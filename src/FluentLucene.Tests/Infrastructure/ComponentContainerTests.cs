using System;
using System.Collections;
using FluentLucene.Infrastructure;
using NUnit.Framework;

namespace FluentLucene.Tests.Infrastructure
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local

    [Category("Infrastructure")]
    [TestFixture]
    public class ComponentContainerTests
    {
        private ComponentContainer Container;

        [SetUp]
        public void SetUp()
        {
            Container = new ComponentContainer();
        }

        #region Component structure
        // Components are defined below
        // A
        // B -> (A)
        // C -> (A, A)
        // D -> (B, C)
        // E -> (E)         Circular dependency
        // F -> (G)
        // G -> (H)         
        // H -> (F)         Circular dependency
        #endregion

        [Test]
        public void Get_SimpleTransient_ReturnsInstance()
        {
            Container.Transient<IA, A>();

            var instance = Container.Get<IA>();

            Assert.That(instance, Is.TypeOf<A>());
        }

        [Test]
        public void Get_SameTransientTwice_ReturnsDifferentInstances()
        {
            Container.Transient<IA, A>();

            var instance1 = Container.Get<IA>();
            var instance2 = Container.Get<IA>();

            Assert.That(instance1, Is.Not.SameAs(instance2));
        }

        [Test]
        public void Get_SimpleSingleton_ReturnsInstance()
        {
            Container.Singleton<IA, A>();

            var instance = Container.Get<IA>();

            Assert.That(instance, Is.TypeOf<A>());
        }

        [Test]
        public void Get_SameSingletonTwice_ReturnsSameInstance()
        {
            Container.Singleton<IA, A>();

            var instance1 = Container.Get<IA>();
            var instance2 = Container.Get<IA>();

            Assert.That(instance1, Is.SameAs(instance2));
        }

        [Test]
        public void Get_TransientWithTransientDependencies_ReturnsInstance()
        {
            Container.Transient<IA, A>();
            Container.Transient<IB, B>();

            var instance = Container.Get<IB>();

            Assert.That(instance, Is.TypeOf<B>());
        }

        [Test]
        public void Get_TransientWithSingletonDependencies_ReturnsSameDependencies()
        {
            Container.Singleton<IA, A>();
            Container.Transient<IB, B>();

            var instance1 = Container.Get<IB>();
            var instance2 = Container.Get<IB>();

            Assert.That(instance1, Is.Not.SameAs(instance2));
            Assert.That(((B)instance1).A, Is.SameAs(((B)instance2).A));
        }

        [Test]
        public void Get_SingletonWithTransientDependencies_ReturnsSameInstanceThusSameDependencies()
        {
            Container.Transient<IA, A>();
            Container.Singleton<IB, B>();

            var instance1 = Container.Get<IB>();
            var instance2 = Container.Get<IB>();

            Assert.That(instance1, Is.SameAs(instance2));
            Assert.That(( (B)instance1 ).A, Is.SameAs(( (B)instance2 ).A));
        }

        [Test]
        public void Get_Unregistered_ThrowException()
        {
            AssertThrowsWithRootCause(() => Container.Get<IF>(), ComponentResolutionError.NotRegistered);
        }

        [Test]
        public void Get_TransientWithCircularDependency_ThrowsException()
        {
            Container.Singleton<IE, E>();

            AssertThrowsWithRootCause(() => Container.Get<IE>(), ComponentResolutionError.CircularDependency);
        }

        [Test]
        public void Get_TransientWithLargeCircularDependency_ThrowsException()
        {
            Container.Singleton<IF, F>();
            Container.Singleton<IG, G>();
            Container.Singleton<IH, H>();

            AssertThrowsWithRootCause(() => Container.Get<IF>(), ComponentResolutionError.CircularDependency);
        }

        [Test]
        public void Get_Instance_ReturnsInstance()
        {
            var expected = new A();

            Container.Instance<IA, A>(expected);

            var actual = Container.Get<IA>();

            Assert.That(actual, Is.SameAs(expected));
        }

        [Test]
        public void Get_TransientWithFactoryMethod_InvokesFactoryMethodEachTime()
        {
            var invocations = 0;
            var expected = new A();
            Container.Transient<IA>(() => { invocations++; return expected; });

            var actual1 = Container.Get<IA>();
            var actual2 = Container.Get<IA>();

            Assert.That(invocations, Is.EqualTo(2));
            Assert.That(actual1, Is.SameAs(expected));
            Assert.That(actual2, Is.SameAs(expected));
        }

        [Test]
        public void Get_TransientWithFactoryMethod_InvokesFactoryMethodLazily()
        {
            var invocations = 0;
            var expected = new A();
            Container.Transient<IA>(() => { invocations++; return expected; });

            Assert.That(invocations, Is.EqualTo(0));
        }

        [Test]
        public void Get_SingletonWithFactoryMethod_InvokesFactoryMethodOnce()
        {
            var invocations = 0;
            var expected = new A();
            Container.Singleton<IA>(() => { invocations++; return expected; });

            var actual1 = Container.Get<IA>();
            var actual2 = Container.Get<IA>();

            Assert.That(invocations, Is.EqualTo(1));
            Assert.That(actual1, Is.SameAs(expected));
            Assert.That(actual2, Is.SameAs(expected));
        }

        [Test]
        public void Get_SingletonWithFactoryMethod_InvokesFactoryMethodLazily()
        {
            var invocations = 0;
            var expected = new A();
            Container.Singleton<IA>(() => { invocations++; return expected; });

            Assert.That(invocations, Is.EqualTo(0));
        }

        [Test]
        public void Get_WithDefaultParametersForUnregisteredComponent_UsesProvidedParameters()
        {
            // Arrange
            Container.Transient<IA, A>();
            var parameters = new Hashtable { { "manual", "Injected!" } };
            Container.Transient<IJ, J>(parameters);

            // Act
            var actual = Container.Get<IJ>();

            // Assert
            Assert.That(actual, Is.TypeOf<J>());
            Assert.That(( (J)actual ).Manual, Is.EqualTo("Injected!"));
        }

        [Test]
        public void Get_WithDefaultParametersForRegisteredComponent_UsesProvidedParameters()
        {
            // Arrange
            Container.Transient<IA, A>();
            var a = new A();
            var parameters = new Hashtable { { "a", a }, { "manual", "Injected!" } };
            Container.Transient<IJ, J>(parameters);

            // Act
            var actual = Container.Get<IJ>();

            // Assert
            Assert.That(actual, Is.TypeOf<J>());
            Assert.That(( (J)actual ).A, Is.SameAs(a));
        }

        [Test]
        public void Get_WithManualParametersForUnregisteredComponent_UsesProvidedParameters()
        {
            // Arrange
            Container.Transient<IA, A>();
            Container.Transient<IJ, J>();

            // Act
            var actual = Container.Get<IJ>(new Hashtable { { "manual", "Injected!" } });

            // Assert
            Assert.That(actual, Is.TypeOf<J>());
            Assert.That(((J)actual).Manual, Is.EqualTo("Injected!"));
        }

        [Test]
        public void Get_WithManualParametersForRegisteredComponent_UsesProvidedParameters()
        {
            // Arrange
            Container.Transient<IA, A>();
            var a = new A();
            Container.Transient<IJ, J>();

            // Act
            var actual = Container.Get<IJ>(new Hashtable { { "a", a }, { "manual", "Injected!" } });

            // Assert
            Assert.That(actual, Is.TypeOf<J>());
            Assert.That(( (J)actual ).A, Is.SameAs(a));
        }

        [Test]
        public void Get_WithManualAndDefaultParameters_ManuelOverridesDefault()
        {
            // Arrange
            Container.Transient<IA, A>();
            var wrongA = new A();
            var a = new A();
            Container.Transient<IJ, J>(new Hashtable { { "a", wrongA }, { "manual", "Injected!" } });

            // Act
            var actual = Container.Get<IJ>(new Hashtable { { "a", a } });

            // Assert
            Assert.That(actual, Is.TypeOf<J>());
            Assert.That(( (J)actual ).A, Is.SameAs(a));
        }

        private static void AssertThrowsWithRootCause(Action action, ComponentResolutionError rootCause)
        {
            Assert.That(() => action(),
                Throws.InstanceOf<ComponentResolutionException>()
                      .With.Property("RootCause").EqualTo(rootCause));
        }
    }

    #region Components
    public class A : IA { }
    public interface IA { }

    public interface IB { }
    public class B : IB
    {
        public B(IA a) { A = a; }
        public IA A { get; private set; }
    }

    public interface IC { }
    public class C : IC
    {
        public C(IA a1, IA a2) { A1 = a1; A2 = a2; }
        public IA A1 { get; private set; }
        public IA A2 { get; private set; }
    }

    public interface ID { }
    public class D : ID
    {
        public D(IB b, IC c) { B = b; C = c; }
        public IB B { get; private set; }
        public IC C { get; private set; }
    }

    public interface IE { }
    public class E : IE { public E(IE e) { } }

    public interface IF { }
    public class F : IF { public F(IG g) { } }

    public interface IG { }
    public class G : IG { public G(IH h) { } }

    public interface IH { }
    public class H : IH { public H(IF f) { } }

    public interface IJ { }
    public class J : IJ
    {
        public J(IA a, string manual) { A = a; Manual = manual; }
        public IA A { get; private set; }
        public string Manual { get; private set; }
    }
    #endregion
}
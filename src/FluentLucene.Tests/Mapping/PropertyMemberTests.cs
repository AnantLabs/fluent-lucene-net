using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentLucene.Mapping;
using NUnit.Framework;

namespace FluentLucene.Tests.Mapping
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local
    // ReSharper disable ClassNeverInstantiated.Local
    // ReSharper disable UnusedAutoPropertyAccessor.Local

    [Category("Reflection")]
    [TestFixture]
    public class PropertyMemberTests
    {
        private static PropertyMember CreateMember(Expression<Func<Target, object>> expression)
        {
            var memberInfo = expression.ToMemberExpression().Member;
            return new PropertyMember((PropertyInfo)memberInfo);
        }

        [Test]
        public void GetValue_PublicProperty_CanGetValue()
        {
            var member = CreateMember(x => x.Property);
            var target = new Target { Property = "value" };

            var value = member.GetValue(target);

            Assert.That(value, Is.EqualTo("value"));
        }

        [Test]
        public void SetValue_PublicProperty_CanSetValue()
        {
            var member = CreateMember(x => x.Property);
            var target = new Target();

            member.SetValue(target, "value");

            Assert.That(target.Property, Is.EqualTo("value"));
        }

        [Test]
        public void MemberType_Always_ReturnsPropertyType()
        {
            var member = CreateMember(x => x.Property);

            var result = member.MemberType;

            Assert.That(result, Is.EqualTo(typeof(string)));
        }

        [Test]
        public void Name_Always_ReturnsPropertyName()
        {
            var member = CreateMember(x => x.Property);

            var result = member.Name;

            Assert.That(result, Is.EqualTo("Property"));
        }

        [Test]
        public void Equals_OtherRepresentsSameProperty_ReturnsTrue()
        {
            var member = CreateMember(x => x.Property);
            var other = CreateMember(x => x.Property);

            var result = member.Equals(other);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_OtherRepresentsDifferentProperty_ReturnsFalse()
        {
            var member = CreateMember(x => x.Property);
            var other = CreateMember(x => x.OtherProperty);

            var result = member.Equals(other);

            Assert.That(result, Is.False);
        }

        #region Utility classes

        private class Target
        {
            public string Property { get; set; }
            public string OtherProperty { get; set; }
        }

        #endregion
    }
}
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
    public class MemberBaseTests
    {
        private static MemberBase CreateMember<TSource>(Expression<Func<TSource, object>> expression)
        {
            var memberInfo = expression.ToMemberExpression().Member;
            return new SampleMember(memberInfo);
        }

        [Test]
        public void Equals_OtherIsNull_ReturnsFalse()
        {
            var member = CreateMember<Target>(x => x.Property);
            MemberBase other = null;

            var result = member.Equals(other);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_OtherIsSame_ReturnsTrue()
        {
            var member = CreateMember<Target>(x => x.Property);
            MemberBase other = member;

            var result = member.Equals(other);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_OtherRepresentsSameMember_ReturnsTrue()
        {
            var member = CreateMember<Target>(x => x.Property);
            MemberBase other = CreateMember<Target>(x => x.Property);

            var result = member.Equals(other);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_OtherRepresentsDifferentMemberInSameType_ReturnsFalse()
        {
            var member = CreateMember<Target>(x => x.Property);
            MemberBase other = CreateMember<Target>(x => x.OtherProperty);

            var result = member.Equals(other);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_OtherRepresentsSimilarMemberInOtherType_ReturnsFalse()
        {
            var member = CreateMember<Target>(x => x.Property);
            MemberBase other = CreateMember<Target2>(x => x.Property);

            var result = member.Equals(other);

            Assert.That(result, Is.False);
        }

        #region Utility classes

        private class SampleMember : MemberBase
        {
            public SampleMember(MemberInfo member) : base(member)
            { }

            public override object GetValue(object target)
            {
                throw new NotSupportedException();
            }

            public override void SetValue(object target, object value)
            {
                throw new NotSupportedException();
            }

            public override Type MemberType
            {
                get { throw new NotSupportedException(); }
            }

            public override string Name
            {
                get { throw new NotSupportedException(); }
            }
        }

        private class Target
        {
            public int Property { get; set; }
            public int OtherProperty { get; set; }
        }

        private class Target2
        {
            public int Property { get; set; }
        }

        #endregion
    }
}
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

    [Category(TestCategories.Reflection)]
    [TestFixture]
    public class FieldMemberTests
    {
        private static FieldMember CreateMember(Expression<Func<Target, object>> expression)
        {
            var memberInfo = expression.ToMemberExpression().Member;
            return new FieldMember((FieldInfo)memberInfo);
        }

        [Test]
        public void GetValue_PublicField_CanGetValue()
        {
            var member = CreateMember(x => x.Field);
            var target = new Target { Field = "value" };

            var value = member.GetValue(target);

            Assert.That(value, Is.EqualTo("value"));
        }

        [Test]
        public void SetValue_PublicField_CanSetValue()
        {
            var member = CreateMember(x => x.Field);
            var target = new Target();

            member.SetValue(target, "value");

            Assert.That(target.Field, Is.EqualTo("value"));
        }

        [Test]
        public void MemberType_Always_ReturnsFieldType()
        {
            var member = CreateMember(x => x.Field);

            var result = member.MemberType;

            Assert.That(result, Is.EqualTo(typeof(string)));
        }

        [Test]
        public void Name_Always_ReturnsFieldName()
        {
            var member = CreateMember(x => x.Field);

            var result = member.Name;

            Assert.That(result, Is.EqualTo("Field"));
        }

        [Test]
        public void Equals_OtherRepresentsSameField_ReturnsTrue()
        {
            var member = CreateMember(x => x.Field);
            var other = CreateMember(x => x.Field);

            var result = member.Equals(other);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_OtherRepresentsDifferentField_ReturnsFalse()
        {
            var member = CreateMember(x => x.Field);
            var other = CreateMember(x => x.OtherField);

            var result = member.Equals(other);

            Assert.That(result, Is.False);
        }

        #region Utility classes

        private class Target
        {
            public string Field;
            public string OtherField;
        }

        #endregion
    }
}
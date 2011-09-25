using FluentLucene.Types;
using NUnit.Framework;

namespace FluentLucene.Tests.Types
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local

    [Category("Conversion")]
    [TestFixture]
    public class ConvertLexTests
    {
        private static readonly ConvertLex Convert = new ConvertLex();

        #region Int32

        [Test]
        public void Int32_PositiveNumbers_StayOrdered()
        {
            var a = Convert.Int32(15);
            var b = Convert.Int32(16);

            Assert.That(a, Is.LessThan(b));
        }

        [Test]
        public void Int32_NegativeNumbers_StayOrdered()
        {
            var a = Convert.Int32(-15);
            var b = Convert.Int32(-16);

            Assert.That(a, Is.GreaterThan(b));
        }

        [Test]
        public void Int32_Zero_IsLesserThanAnyPositiveNumber()
        {
            var a = Convert.Int32(0);
            var b = Convert.Int32(1);

            Assert.That(a, Is.LessThan(b));
        }

        [Test]
        public void Int32_Zero_IsGreaterThanAnyNegativeNumber()
        {
            var a = Convert.Int32(0);
            var b = Convert.Int32(-1);

            Assert.That(a, Is.GreaterThan(b));
        }

        [Test]
        public void Int32_PositiveAndNegativeNumbers_StayOrdered()
        {
            var a = Convert.Int32(-1);
            var b = Convert.Int32(1);

            Assert.That(a, Is.LessThan(b));
        }

        [Test]
        public void Int32_IntMinAndMax_StayOrdered()
        {
            var a = Convert.Int32(int.MinValue);
            var b = Convert.Int32(int.MaxValue);

            Assert.That(a, Is.LessThan(b));
        }

        #endregion
    }
}
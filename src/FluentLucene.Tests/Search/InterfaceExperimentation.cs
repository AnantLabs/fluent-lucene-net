using FluentLucene.Tests.Mapping;
using Moq;
using NUnit.Framework;

namespace FluentLucene.Tests.Search
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local

    [Category("Experimentation")]
    [TestFixture]
    public class InterfaceExperimentation
    {
        private void Nothing()
        {
            var session = new Mock<ILuceneSession>().Object;

            session.SearchOver<SampleDocument>()
                .WhereAny().Matches("hello").Required()
                .And(x => x.StringValue).IsLike("jello")
                .AndAny().Not.Matches("poop")
                .And(x => x.DateValue).IsBetween("20020101").And("20030101")
                .OrderBy(x => x.DateValue)
                .List();
        }
    }
}
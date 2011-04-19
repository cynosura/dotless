namespace dotless.Test.Unit.Response
{
    using Core.Response;
    using Moq;
    using NUnit.Framework;

    public class CachedCssResponseFixture : HttpFixtureBase
    {
        CachedCssResponse CachedCssResponse { get; set; }

        [SetUp]
        public void Setup()
        {
            CachedCssResponse = new CachedCssResponse(Http.Object);
        }

        [Test]
        public void ContentTypeIsSetToTextCss()
        {
            CachedCssResponse.WriteResponse(null);

            HttpResponse.VerifySet(r => r.ContentType = "text/css", Times.Once());
        }

        [Test]
        public void ResponseEndIsCalled()
        {
            CachedCssResponse.WriteResponse(null);

            HttpResponse.Verify(r => r.End(), Times.Once());
        }
    }
}
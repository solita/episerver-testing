using EPiServer;
using EPiServer.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Solita.Testing.EPiServer.Test.Tests
{

    /// <summary>
    /// Tests for <see cref="FakeUrlResolver"/>.
    /// </summary>
    [TestClass]
    public class FakeUrlResolverTests
    {

        private readonly FakeContentRepository contentRepository = new FakeContentRepository();

        /// <summary>
        /// Test routing content to URL and back.
        /// </summary>
        [TestMethod]
        public void Route_WithContentRepository()
        {
            
            var content = contentRepository.GetDefault<PageData>(ContentReference.EmptyReference);
            content.Name = "Test content";
            var contentReference = contentRepository.Save(content);
            var resolver = new FakeUrlResolver(contentRepository);
            var url = resolver.GetUrl(contentReference);

            var routedContent = resolver.Route(new UrlBuilder(url));
            
            Assert.IsNotNull(routedContent, "routedContent");
            Assert.AreEqual(contentReference, routedContent.ContentLink, "Routed content reference");
            Assert.AreEqual("Test content", routedContent.Name, "Routed content name");

        }

    }

}

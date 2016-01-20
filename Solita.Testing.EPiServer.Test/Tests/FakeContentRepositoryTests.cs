using System.Linq;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Solita.Testing.EPiServer.Test.Tests
{

    /// <summary>
    /// Tests for <see cref="FakeContentRepository"/>.
    /// </summary>
    [TestClass]
    public class FakeContentRepositoryTests
    {

        private readonly FakeContentRepository repository = new FakeContentRepository();

        [TestMethod]
        public void GetDefault_SharedBlock()
        {

            var result = repository.GetDefault<BlockData>(ContentReference.EmptyReference);

            Assert.IsNotNull(result, "result");
            Assert.IsInstanceOfType(result, typeof(IContentData), "Shared blocks are content data");
            Assert.IsInstanceOfType(result, typeof(IVersionable), "Sahred blocks are versionable");

        }

        [TestMethod]
        public void GetDefault_Page()
        {

            var result = repository.GetDefault<PageData>(ContentReference.EmptyReference);

            Assert.IsNotNull(result, "result");
            Assert.IsInstanceOfType(result, typeof(IContentData), "Pages are content data");
            Assert.IsInstanceOfType(result, typeof(IVersionable), "Pages are versionable");

        }

        [TestMethod]
        public void Save_NewContent_NoPublish()
        {

            var content = repository.GetDefault<PageData>(ContentReference.EmptyReference);

            var result = repository.Save(content, SaveAction.Save, AccessLevel.NoAccess);

            Assert.IsFalse(ContentReference.IsNullOrEmpty(result), "Content ID was assigned");
            Assert.AreEqual(1, repository.AllContents.Count(), "Number of content in the repository");
            var contentFromRepo = repository.Get<PageData>(result);
            Assert.IsNotNull(contentFromRepo, "Saved content can be loaded from the repository");
            Assert.AreEqual(result, contentFromRepo.ContentLink, "ContentReference matches the returned value");

        }

        [TestMethod]
        public void Save_NewContent_Publish()
        {

            var content = repository.GetDefault<PageData>(ContentReference.EmptyReference);

            var result = repository.Save(content, SaveAction.Publish, AccessLevel.NoAccess);

            var contentFromRepo = repository.Get<PageData>(result);
            Assert.AreEqual(contentFromRepo.Status, VersionStatus.Published, "Status is published");
            Assert.IsTrue(contentFromRepo.CheckPublishedStatus(PagePublishedStatus.Published), "CheckPublishedStatus indicates published");

        }

    }

}

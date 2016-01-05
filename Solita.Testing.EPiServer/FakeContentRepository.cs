using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Security;

namespace Solita.Testing.EPiServer
{

    /// <summary>
    /// Imitates EPiServer content repository by saving and loading content, generating content IDs as necessary.
    /// Runs completely in memory, avoiding the initialization of EPiServer context.
    /// </summary>
    public class FakeContentRepository : IContentRepository
    {

        protected readonly Dictionary<ContentReference, IContent> contents = new Dictionary<ContentReference, IContent>();
        private readonly FakeContentEvents contentEvents = new FakeContentEvents();
        protected int id = 1;

        /// <summary>
        /// List of all contents in the repository.
        /// </summary>
        public IEnumerable<IContent> AllContents
        {
            get
            {
                return contents.Values;
            }
        }

        public FakeContentEvents ContentEvents
        {
            get { return contentEvents; }
        }

        private ContentEventArgs ContentEventArgs(IContent content)
        {
            return new ContentEventArgs(content.ContentLink, content);
        }

        public virtual T Get<T>(Guid contentGuid) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual T Get<T>(Guid contentGuid, LoaderOptions settings) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual T Get<T>(Guid contentGuid, CultureInfo language) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual T Get<T>(ContentReference contentLink) where T : IContentData
        {
            return contents.ContainsKey(contentLink) ? (T)contents[contentLink] : default(T);
        }

        public virtual T Get<T>(ContentReference contentLink, CultureInfo language) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual T Get<T>(ContentReference contentLink, LoaderOptions settings) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetChildren<T>(ContentReference contentLink) where T : IContentData
        {
            return contents.Values.Where(c => c.ParentLink == contentLink).OfType<T>();
        }

        public virtual IEnumerable<T> GetChildren<T>(ContentReference contentLink, CultureInfo language) where T : IContentData
        {
            return GetChildren<T>(contentLink);
        }

        public virtual IEnumerable<T> GetChildren<T>(ContentReference contentLink, LoaderOptions settings) where T : IContentData
        {
            return GetChildren<T>(contentLink);
        }

        public virtual IEnumerable<T> GetChildren<T>(ContentReference contentLink, CultureInfo language, int startIndex, int maxRows) where T : IContentData
        {
            return GetChildren<T>(contentLink);
        }

        public virtual IEnumerable<T> GetChildren<T>(ContentReference contentLink, LoaderOptions settings, int startIndex, int maxRows) where T : IContentData
        {
            return GetChildren<T>(contentLink);
        }

        public virtual IEnumerable<ContentReference> GetDescendents(ContentReference contentLink)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<IContent> GetAncestors(ContentReference contentLink)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<IContent> GetItems(IEnumerable<ContentReference> contentLinks, CultureInfo language)
        {
            // Note: filtering by language parameter is not implemented.
            var items = contents.Values.Where(c => contentLinks.Contains(c.ContentLink));
            return items;
        }

        public virtual IEnumerable<IContent> GetItems(IEnumerable<ContentReference> contentLinks, LoaderOptions settings)
        {            
            return GetItems(contentLinks, CultureInfo.CurrentCulture);
        }

        public virtual IContent GetBySegment(ContentReference parentLink, string urlSegment, CultureInfo language)
        {
            throw new NotImplementedException();
        }

        public virtual IContent GetBySegment(ContentReference parentLink, string urlSegment, LoaderOptions settings)
        {
            throw new NotImplementedException();
        }

        public virtual bool TryGet<T>(ContentReference contentLink, out T content) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual bool TryGet<T>(ContentReference contentLink, CultureInfo language, out T content) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual bool TryGet<T>(ContentReference contentLink, LoaderOptions settings, out T content) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual bool TryGet<T>(Guid contentGuid, out T content) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual bool TryGet<T>(Guid contentGuid, CultureInfo language, out T content) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual bool TryGet<T>(Guid contentGuid, LoaderOptions loaderOptions, out T content) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetLanguageBranches<T>(ContentReference contentLink) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual T GetDefault<T>(ContentReference parentLink) where T : IContentData
        {

            // Create a new block or page, depending on type.
            // Note: need some way to hook to the creation process, to mock ContentAreas for example - maybe ContentEvents is enough?

            if (typeof(BlockData).IsAssignableFrom(typeof(T)))
            {
                return CreateSharedBlock.OfType<T>();
            } else
            {
                return CreatePage.OfType<T>(parentLink != null ? new PropertyPageReference(parentLink.ID) : null);
            }

        }

        public virtual T GetDefault<T>(ContentReference parentLink, CultureInfo language) where T : IContentData
        {
            return GetDefault<T>(parentLink);
        }

        public virtual T GetDefault<T>(ContentReference parentLink, int contentTypeID) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual T GetDefault<T>(ContentReference parentLink, int contentTypeID, CultureInfo language) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual T CreateLanguageBranch<T>(ContentReference contentLink, CultureInfo language) where T : IContentData
        {
            throw new NotImplementedException();
        }

        public virtual ContentReference Copy(ContentReference source, ContentReference destination, AccessLevel requiredSourceAccess,
            AccessLevel requiredDestinationAccess, bool publishOnDestination)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(ContentReference contentLink, bool forceDelete, AccessLevel access)
        {
            if (contents.ContainsKey(contentLink))
                contents.Remove(contentLink);
        }

        public virtual void DeleteChildren(ContentReference contentLink, bool forceDelete, AccessLevel access)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteLanguageBranch(ContentReference contentLink, string languageBranch, AccessLevel access)
        {
            throw new NotImplementedException();
        }

        public virtual ContentReference Move(ContentReference contentLink, ContentReference destination, AccessLevel requiredSourceAccess,
            AccessLevel requiredDestinationAccess)
        {
            throw new NotImplementedException();
        }

        public virtual void MoveToWastebasket(ContentReference contentLink, string deletedBy)
        {
            throw new NotImplementedException();
        }

        public virtual ContentReference Save(IContent content)
        {
            return Save(content, SaveAction.Publish, AccessLevel.NoAccess);
        }

        /// <summary>
        /// Saves content to repository.
        /// </summary>
        /// <remarks>
        /// If content reference is not specified a new ID is generated.
        /// Raises the appropriate content events.
        /// For content that is versionable the publish status is updated.
        /// However, new versions are not created at the moment (that may be implemented if needed).
        /// Access permissions checking is not implemented.
        /// </remarks>
        public virtual ContentReference Save(IContent content, SaveAction action, AccessLevel access)
        {

            bool isNew = false;
            
            if (ContentReference.IsNullOrEmpty(content.ContentLink))
            {
                content.ContentLink = new ContentReference(id++);
                isNew = true;
            }

            if (contents.ContainsKey(content.ContentLink)) {
                // Update content
                contents[content.ContentLink] = content;
            } else
            {
                contents.Add(content.ContentLink, content);
            }

            if (isNew)
                contentEvents.OnCreatedContent(ContentEventArgs(content));
            else if (action == SaveAction.Save)
                contentEvents.OnSavedContent(ContentEventArgs(content));

            if (action == SaveAction.Publish)
            {

                if (content is IVersionable)
                {
                    var versionable = (IVersionable)content;
                    versionable.Status = VersionStatus.Published;
                    versionable.IsPendingPublish = false;                    
                }

                contentEvents.OnPublishedContent(ContentEventArgs(content));

            }

            return content.ContentLink;

        }

        public virtual IEnumerable<ReferenceInformation> GetReferencesToContent(ContentReference contentLink, bool includeDecendents)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<IContent> ListDelayedPublish()
        {
            throw new NotImplementedException();
        }
    }

}

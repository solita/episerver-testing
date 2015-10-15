using System;
using EPiServer;
using EPiServer.Core;

namespace Solita.Testing.EPiServer
{

    /// <summary>
    /// Content events raised by <see cref="FakeContentRepository"/>.
    /// </summary>
    public class FakeContentEvents : IContentEvents
    {
        public event ChildrenEventHandler LoadingChildren;
        public event ChildrenEventHandler LoadedChildren;
        public event ChildrenEventHandler FailedLoadingChildren;
        public event EventHandler<ContentEventArgs> LoadingContent;
        public event EventHandler<ContentEventArgs> LoadedContent;
        public event EventHandler<ContentEventArgs> FailedLoadingContent;
        public event EventHandler<ContentEventArgs> LoadingDefaultContent;
        public event EventHandler<ContentEventArgs> LoadedDefaultContent;
        public event EventHandler<ContentEventArgs> PublishingContent;
        public event EventHandler<ContentEventArgs> PublishedContent;
        public event EventHandler<ContentEventArgs> CheckingInContent;
        public event EventHandler<ContentEventArgs> CheckedInContent;
        public event EventHandler<ContentEventArgs> RejectingContent;
        public event EventHandler<ContentEventArgs> RejectedContent;
        public event EventHandler<DeleteContentEventArgs> DeletingContent;
        public event EventHandler<DeleteContentEventArgs> DeletedContent;
        public event EventHandler<ContentEventArgs> DeletingContentLanguage;
        public event EventHandler<ContentEventArgs> DeletedContentLanguage;
        public event EventHandler<ContentEventArgs> MovingContent;
        public event EventHandler<ContentEventArgs> MovedContent;
        public event EventHandler<ContentEventArgs> CreatingContent;
        public event EventHandler<ContentEventArgs> CreatedContent;
        public event EventHandler<ContentEventArgs> SavingContent;
        public event EventHandler<ContentEventArgs> SavedContent;
        public event EventHandler<ContentEventArgs> DeletingContentVersion;
        public event EventHandler<ContentEventArgs> DeletedContentVersion;

        public virtual void OnCreatedContent(ContentEventArgs e)
        {
            if (CreatedContent != null)
                CreatedContent.Invoke(this, e);
        }

        public virtual void OnSavedContent(ContentEventArgs e)
        {
            if (SavedContent != null)
                SavedContent.Invoke(this, e);
        }

        public virtual void OnPublishedContent(ContentEventArgs e)
        {
            if (PublishedContent != null)
                PublishedContent.Invoke(this, e);
        }

    }

}

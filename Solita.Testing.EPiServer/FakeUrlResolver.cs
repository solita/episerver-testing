using System;
using System.Web.Routing;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Routing;

namespace Solita.Testing.EPiServer
{

    /// <summary>
    /// Fake URL resolver that doesn't require initializing the EPiServer content.
    /// Generates and resolves very simple URLs that are simply the content ID.
    /// Providing content repository is optional - if provided, it will be used for loading content. Otherwise BasicContent is returned.
    /// </summary>
    public class FakeUrlResolver : UrlResolver
    {

        private readonly IContentRepository contentRepository;

        public FakeUrlResolver() { }

        public FakeUrlResolver(IContentRepository contentRepository)
        {
            this.contentRepository = contentRepository;
        }

        public override IContent Route(UrlBuilder urlBuilder, ContextMode contextMode)
        {

            // Attempts to parse content ID from the path.
            // This only works with URLs generated with the GetUrl method, where the URL is the content ID.
            // If contentRepository is provided, load the content from there, otherwise return BasicContent.

            int id;
            if (int.TryParse(urlBuilder.Path, out id))
            {
                var contentLink = new ContentReference(id);
                if (contentRepository != null)
                    return contentRepository.Get<IContent>(contentLink);
                else
                    return new BasicContent { ContentLink = contentLink };                
            }

            return null;

        }

        public override VirtualPathData GetVirtualPathForNonContent(object partialRoutedObject, string language,
            VirtualPathArguments virtualPathArguments)
        {
            throw new NotImplementedException();
        }

        protected override VirtualPathData GetVirtualPathInternal(ContentReference contentLink, string language, VirtualPathArguments arguments)
        {
            throw new NotImplementedException();
        }

        public override string GetUrl(IContent content)
        {
            // Generates URL that is simply the content ID, for example "123"
            return content != null ? content.ContentLink.ID.ToString() : string.Empty;
        }

        public override string GetUrl(UrlBuilder urlBuilderWithInternalUrl, VirtualPathArguments arguments)
        {
            return urlBuilderWithInternalUrl.Path;
        }

        public override string GetUrl(ContentReference contentLink, string language)
        {
            // Generates URL that is simply the content ID, for example "123"
            return contentLink != null ? contentLink.ID.ToString() : string.Empty;
        }

        public override bool TryToPermanent(string url, out string permanentUrl)
        {
            throw new NotImplementedException();
        }

        protected override bool ConvertToPermanent(UrlBuilder url, IContent content)
        {
            throw new NotImplementedException();
        }

    }

}

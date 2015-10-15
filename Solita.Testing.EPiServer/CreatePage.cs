using Castle.DynamicProxy;
using EPiServer.Core;
using EPiServer.SpecializedProperties;

namespace Solita.Testing.EPiServer
{

    // Code taken from EPiAbstractions, https://github.com/MikeHook/EPiAbstractions
    public class CreatePage
    {

        public static T OfType<T>(PropertyPageReference parentLink = null, params object[] constructorArguments) where T : IContentData
        {
            T product = CreateInstanceOfClass<T>(constructorArguments);

            product.Property["PageLink"] = new PropertyPageReference();
            product.Property["PageName"] = new PropertyString();
            product.Property["PageTypeID"] = new PropertyNumber();
            product.Property["PageParentLink"] = parentLink ?? new PropertyPageReference();
            product.Property["PageDeleted"] = new PropertyBoolean(false);
            product.Property["PageMasterLanguageBranch"] = new PropertyLanguage();
            product.Property["PageVisibleInMenu"] = new PropertyBoolean(true);
            product.Property["PageChildOrderRule"] = new PropertyNumber(1);
            product.Property["PagePeerOrder"] = new PropertyNumber(0);
            product.Property["PageTypeName"] = new PropertyString();
            product.Property["PagePendingPublish"] = new PropertyBoolean(true);
            product.Property["PageWorkStatus"] = new PropertyNumber(2);
            product.Property["PageLanguageBranch"] = new PropertyLanguage();
            product.Property["PageStartPublish"] = new PropertyDate();
            product.Property["PageStopPublish"] = new PropertyDate();
            product.Property["PageCreated"] = new PropertyDate();
            product.Property["PageCreatedBy"] = new PropertyString();
            product.Property["PageSaved"] = new PropertyDate();
            product.Property["PageShortcutType"] = new PropertyNumber(0);
            product.Property["PageLinkURL"] = new PropertyString();

            return product;
        }

        private static T CreateInstanceOfClass<T>(params object[] constructorArguments)
        {
            var proxyGenerator = new ProxyGenerator();
            return (T) proxyGenerator.CreateClassProxy(typeof (T), constructorArguments, new IInterceptor[] {});

        }
    }

}

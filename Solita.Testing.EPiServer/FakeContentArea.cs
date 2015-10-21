using System.Collections.Generic;
using EPiServer.Core;

namespace Solita.Testing.EPiServer
{

    /// <summary>
    /// Normal EPiServer content area requires context initialization when the items list is accessed.
    /// This implementation mocks that list so the EPi context is not needed.
    /// </summary>
    public class FakeContentArea : ContentArea
    {

        public IList<ContentAreaItem> FilteredItemsList { get; set; }
        public IList<ContentAreaItem> ItemsList { get; set; }

        public FakeContentArea()
        {
            ItemsList = new List<ContentAreaItem>();
        }

        public override int Count
        {
            get { return ItemsList.Count; }
        }

        // ContentArea's list of items is an observable list, need to mock it.
        public override IEnumerable<ContentAreaItem> FilteredItems
        {
            get { return FilteredItemsList ?? ItemsList; }
        }

        // ContentArea's list of items is an observable list, need to mock it.
        public override IList<ContentAreaItem> Items
        {
            get { return ItemsList; }
        }

    }

}

using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Maintenance;
using Sitecore.Data;
using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using System;

namespace Sitecore.Support
{
    public class FormItemEventHandler
    {
        private const string logFormat = "Sitecore.Support.204746.517881: Explicitly indexing form item: {0}({1})";
        readonly ID formTemplateID = ID.Parse("{6ABEE1F2-4AB4-47F0-AD8B-BDB36F37F64C}");
        public void OnFormItemCreated(object sender, EventArgs args)
        {
            if (args != null)
            {
                ItemCreatedEventArgs args1 = Event.ExtractParameter(args, 0) as ItemCreatedEventArgs;
                Assert.IsNotNull(args1, "Sitecore.Support.204746.517881: createdArgs");
                Item item = args1.Item;
                Assert.IsNotNull(item, "Sitecore.Support.204746.517881: No item in parameters");
                if (item.TemplateID.Equals(formTemplateID))
                {
                    Log.Info(string.Format(logFormat, item.Name, item.ID.ToString()), this);
                    IndexCustodian.RefreshTree(new SitecoreIndexableItem(item));
                }
            }
        }
    }
}

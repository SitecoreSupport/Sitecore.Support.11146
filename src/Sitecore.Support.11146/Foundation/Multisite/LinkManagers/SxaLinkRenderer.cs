using Sitecore.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Sites;
using Sitecore.XA.Foundation.IoC;
using Sitecore.Xml.Xsl;
using Sitecore.XA.Foundation.Multisite;

namespace Sitecore.Support.XA.Foundation.Multisite.LinkManagers
{
  public class SxaLinkRenderer : Sitecore.XA.Foundation.Multisite.LinkManagers.SxaLinkRenderer
  {
    public SxaLinkRenderer(Item item) : base(item) { }
    protected override string GetUrl(XmlField field)
        {
            if (field != null)
            {
                LinkItem linkItem = new LinkItem(field.Value);
                return linkItem.TargetUrl;
            }

            return LinkManager.GetItemUrl(Item, GetUrlOptions(Item));
        }
  }
}
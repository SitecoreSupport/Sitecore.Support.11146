using Sitecore.Data.Items;
//using Sitecore.XA.Foundation.Multisite.LinkManagers;
using Sitecore.Xml.Xsl;
using Sitecore.Support.XA.Foundation.Multisite.LinkManagers;

namespace Sitecore.Support.XA.Foundation.Multisite.Pipelines.RenderField
{
  public class GetLinkFieldValue: Sitecore.Pipelines.RenderField.GetLinkFieldValue
  {
    protected override LinkRenderer CreateRenderer(Item item)
    {
      return new SxaLinkRenderer(item);
    }
  }
}
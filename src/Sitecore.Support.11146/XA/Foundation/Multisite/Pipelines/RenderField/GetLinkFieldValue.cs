namespace Sitecore.Support.XA.Foundation.Multisite.Pipelines.RenderField
{
  using Sitecore.Data.Items;
  using Sitecore.Xml.Xsl;

  public class GetLinkFieldValue: Sitecore.Pipelines.RenderField.GetLinkFieldValue
  {
    protected override LinkRenderer CreateRenderer(Item item)
    {
      return new Sitecore.Support.XA.Foundation.Multisite.LinkManagers.SxaLinkRenderer(item);
    }
  }
}
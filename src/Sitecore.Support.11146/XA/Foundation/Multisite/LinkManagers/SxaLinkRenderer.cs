namespace Sitecore.Support.XA.Foundation.Multisite.LinkManagers
{
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Links;

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
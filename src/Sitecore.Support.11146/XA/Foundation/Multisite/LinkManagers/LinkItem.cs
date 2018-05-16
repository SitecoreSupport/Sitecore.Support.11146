namespace Sitecore.Support.XA.Foundation.Multisite.LinkManagers
{
  using Microsoft.Extensions.DependencyInjection;
  using Sitecore.Data.Items;
  using Sitecore.Links;
  using Sitecore.Sites;
  using Sitecore.XA.Foundation.Multisite;
  using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Xml;
  using System.Xml;

  public class LinkItem : Sitecore.XA.Foundation.Multisite.LinkManagers.LinkItem
  {
    private string Url { get; set; }
    private string QueryString { get; set; }

    public LinkItem(string xml) : base(xml)
    {
      if (string.IsNullOrEmpty(xml))
      {
        return;
      }

      XmlNode node = XmlUtil.GetXmlNode(xml);
      if (node == null)
      {
        return;
      }

      if (node.Name == "link" && node.Attributes != null)
      {
        XmlAttribute urlAttr = node.Attributes["url"];
        if (urlAttr != null)
        {
          Url = urlAttr.Value;
        }

        XmlAttribute typeAttr = node.Attributes["linktype"];
        if (typeAttr != null)
        {
          if (typeAttr.Value == "anchor")
          {
            Url = "#" + Url;
          }
        }

        XmlAttribute queryStrAttr = node.Attributes["querystring"];
        if (queryStrAttr != null)
        {
          QueryString = queryStrAttr.Value;
        }
      }
    }

    public override string TargetUrl
    {
      get
      {
        if (TargetItem != null)
        {
          if (IsMediaLink || TargetItem.Paths.IsMediaItem)
          {
            return ((MediaItem)TargetItem).GetMediaUrl() + (string.IsNullOrEmpty(QueryString) ? "" : "?" + QueryString);
          }

          if (IsInternal)
          {
            var targetSiteInfo = DependencyInjection.ServiceLocator.ServiceProvider.GetService<ISiteInfoResolver>().GetSiteInfo(TargetItem);
            var urlOptions = (UrlOptions)UrlOptions.DefaultOptions.Clone();

            if (targetSiteInfo != null)
            {
              var targetSiteContext = new SiteContext(targetSiteInfo);
              urlOptions.Site = targetSiteContext;
            }

            string link = LinkManager.GetItemUrl(TargetItem, urlOptions);

            if (!string.IsNullOrEmpty(QueryString))
            {
              if (link.Contains("?"))
              {
                return link + "&" + QueryString;
              }
              return link + "?" + QueryString;
            }

            return link;
          }
        }

        return Url;
      }
    }
  }
}
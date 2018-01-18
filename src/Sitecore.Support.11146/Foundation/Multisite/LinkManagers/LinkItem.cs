using System;
using System.Xml;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Sites;
using Sitecore.Web;
using Sitecore.XA.Foundation.IoC;
using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Xml;
using Sitecore.XA.Foundation.Multisite;

namespace Sitecore.Support.XA.Foundation.Multisite.LinkManagers
{
  public class LinkItem: Sitecore.XA.Foundation.Multisite.LinkManagers.LinkItem
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
        XmlAttribute QueryStrAttr = node.Attributes["querystring"];
        if (QueryStrAttr != null)
        {

          QueryString = QueryStrAttr.Value;
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
            var targetSiteInfo = ServiceLocator.Current.Resolve<ISiteInfoResolver>().GetSiteInfo(TargetItem);
            var urlOptions = (UrlOptions)UrlOptions.DefaultOptions.Clone();
            if (targetSiteInfo != null)
            {
              urlOptions.Site = new SiteContext(targetSiteInfo);

              urlOptions.LanguageEmbedding = LanguageEmbedding.Never;
            }

            return LinkManager.GetItemUrl(TargetItem, urlOptions) + (string.IsNullOrEmpty(QueryString) ? "" : "?" + QueryString);
          }
        }

        return Url;
      }
    }

  }
    
}
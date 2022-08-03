using System.Xml.Linq;

namespace RssReader.Domain
{
    public class Reader
    {
        public IList<Feed> Parse(string url)
        {
            try
            {
                XDocument feed = XDocument.Load(url);

                var entries = from item in feed.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")

                              select new Feed
                              {
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                                  Author = item.Elements().First(i => i.Name.LocalName == "author").Value,
                                  Id = item.Elements().First(i => i.Name.LocalName == "guid").Value,
                                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  Image = (item != null && item.Elements().First(i => i.Name.LocalName == "image").FirstAttribute != null) 
                                  ? item.Elements().First(i => i.Name.LocalName == "image").FirstAttribute.Value 
                                  : "",
                                  PublishDate = Convert.ToDateTime(item.Elements().First(i => i.Name.LocalName == "pubDate").Value)
                              };

                return entries.ToList();
            }
            catch (Exception)
            {
                return new List<Feed>();
            }
        }
    }
}

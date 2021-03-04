using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortener.Web.Models
{
    public class LinkShortenerDatabaseSettings: ILinkShortenerDatabaseSettings
    {
        public string LinksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ILinkShortenerDatabaseSettings
    {
        public string LinksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

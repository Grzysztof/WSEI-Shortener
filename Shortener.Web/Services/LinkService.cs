using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Shortener.Web.Models;

namespace Shortener.Web.Services
{
    public class LinkService
    {
        private readonly IMongoCollection<Link> _links;

        public LinkService(ILinkShortenerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _links = database.GetCollection<Link>(settings.LinksCollectionName);
        }

        public List<Link> get() => _links.Find(link => true).ToList();

        public Link Get(string id) => _links.Find<Link>(link => link.Id == id).FirstOrDefault();

        public Link Create(Link link)
        {
            _links.InsertOne(link);
            return link;
        }

        public void Update(string id, Link linkIn)
        {
            _links.ReplaceOne(link => link.Id == id, linkIn);
        }
        public void Remove(Link linkIn) => _links.DeleteOne(link => link.Id == linkIn.Id);

        public void Remove(string id) => _links.DeleteOne(link => link.Id == id);
    }
}

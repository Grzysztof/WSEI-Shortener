using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortener.Web.Entities;
using Shortener.Web.Models;
using Shortener.Web.Database;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;

namespace Shortener.Web.Services
{
    public interface IShortenerService
    {
        UrlModel Create(UrlModel model);
        UrlModel GetLink(string shortPath);
        void Delete(string shortPath, string token);
    }
    public class ShortenerService : IShortenerService
    {
        private readonly ShortenerDbContext _dbContext;

        public ShortenerService(ShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public UrlModel Create(UrlModel model)
        {
            if (!Uri.TryCreate(model.LongUrl, UriKind.Absolute, out Uri result))
            {
              return null;
            }

                var item = new UrlEntity
            {
                LongUrl = model.LongUrl,
                validationString = new Random().Next().ToString(),
                CreationDate = DateTime.Now
            };

            _dbContext.Links.Add(item);
            _dbContext.SaveChanges();

            // Add to returned model "token" for delete link
            model.deleteToken = CreateDeleteToken(item);
            model.ShortPath = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(item.Id));

            return model;
        }

        public UrlModel GetLink(string shortPath)
        {

            int itemId = BitConverter.ToInt32(WebEncoders.Base64UrlDecode(shortPath));
            var dbItem = _dbContext.Links.Find(itemId);
            
            if (dbItem == null)
                return null;

            var model = new UrlModel
            {
                LongUrl = dbItem.LongUrl
            };

            return model; 
        }

        public void Delete(string shortPath, string token)
        {
            int itemId = BitConverter.ToInt32(WebEncoders.Base64UrlDecode(shortPath));
            var dbItem = _dbContext.Links.Find(itemId);

            if (dbItem != null)
            {
                if (ValidateDeleteToken(token, dbItem))
                {
                    _dbContext.Links.Remove(dbItem);
                    _dbContext.SaveChanges();
                }
            }   

        }

        private static string CreateDeleteToken(UrlEntity model)
        {
            var bToken = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model));
            string deleteToken = Convert.ToBase64String(bToken);
            return deleteToken;
        }
        private static bool ValidateDeleteToken(string deleteToken, UrlEntity dbItem)
        {
            var bToken = Convert.FromBase64String(deleteToken);
            UrlEntity item = JsonSerializer.Deserialize<UrlEntity>(System.Text.Encoding.UTF8.GetString(bToken));

            if (dbItem != null)
            {
                if (!String.Equals(dbItem.validationString, item.validationString))
                    return false;
            }
            return true;
        }
    }
}

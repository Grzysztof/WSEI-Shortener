using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Shortener.Web.Models
{
    public class UrlModel
    {
        [Required]
        [DataType(DataType.Url)]
        public string LongUrl { get; set; }
        public string ShortPath { get; set; }
        public string deleteToken { get; set; }
    }
}

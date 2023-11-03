using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleSearchEngine.Models
{
    public class SearchBundle
    {
        public string Query { get; set; }
        public List<SearchResult> Entries { get; set; }

        public SearchBundle(string query)
        {
            Query = query;
            Entries = new List<SearchResult>();
        }
    }
}

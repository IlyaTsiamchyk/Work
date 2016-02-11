using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangDetector.Domain
{
    public class Request
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string QueryResult { get; set; }
        public string QueryString { get; set; }
    }
}

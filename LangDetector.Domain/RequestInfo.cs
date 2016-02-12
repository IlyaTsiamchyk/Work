using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LangDetector.Domain
{
    [Table("RequestsInfo")]
    public class RequestInfo
    {
        [Key]
        public string UserId { get; set; }
        public int AmountOfQueries { get; set; }
        public string RegisterDateTime { get; set; }
        public string LastLoginDateTime { get; set; }
    }
}

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
        public DateTime RegisterDateTime { get; set; }
        public DateTime LastLoginDateTime { get; set; }
    }
}

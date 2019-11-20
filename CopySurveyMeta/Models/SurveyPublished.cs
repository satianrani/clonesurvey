using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CopySurveyMeta.Models
{
    [Table("SurveyPublished")]
    public class SurveyPublished
    {
        [Key]
        public long ID { get; set; }

        public long SurveyID { get; set; }

        public long SurveyMetaID { get; set; }

        public string PublishedKey { get; set; }

        public DateTime PublishedDate { get; set; }

        public bool IsExpired { get; set; }

        public DateTime? ExpiredDateTime { get; set; }

        public int? TotalConduct { get; set; }

        public int? TotalStart { get; set; }

        public int? TotalFinish { get; set; }

        public long? UserID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string PublishedName { get; set; }

    }
}

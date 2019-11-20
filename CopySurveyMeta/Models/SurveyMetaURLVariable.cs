using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CopySurveyMeta.Models
{
    [Table("SurveyMetaURLVariable")]
    public class SurveyMetaURLVariable
    {
        [Key]
        public long ID { get; set; }

        public long SurveyMetaID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

    }
}

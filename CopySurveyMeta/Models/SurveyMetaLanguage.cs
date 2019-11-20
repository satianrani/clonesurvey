using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CopySurveyMeta.Models
{
    [Table("SurveyMetaLanguage")]
    public class SurveyMetaLanguage
    {
        [Key]
        public long ID { get; set; }

        public long SurveyMetaID { get; set; }

        public long LanguageID { get; set; }

        public string LanguageName { get; set; }

    }
}

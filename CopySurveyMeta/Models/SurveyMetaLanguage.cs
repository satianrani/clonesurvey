using System;
using System.Collections.Generic;
using System.Text;

namespace CopySurveyMeta.Models
{
    public class SurveyMetaLanguage
    {
        public long ID { get; set; }

        public long SurveyMetaID { get; set; }

        public long LanguageID { get; set; }

        public string LanguageName { get; set; }

    }
}

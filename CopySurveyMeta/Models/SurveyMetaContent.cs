using System;
using System.Collections.Generic;
using System.Text;

namespace CopySurveyMeta.Models
{
    public class SurveyMetaContent
    {
        public long ID { get; set; }

        public long SurveyMetaID { get; set; }

        public long LanguageID { get; set; }

        public string ButtonStart { get; set; }

        public string ButtonPrevious { get; set; }

        public string ButtonNext { get; set; }

        public string ButtonSubmit { get; set; }

        public string SurveyTitle { get; set; }

        public string WelcomeContent { get; set; }

        public string WelcomeContentType { get; set; }

        public string ThankyouContent { get; set; }

        public string ThankyouContentType { get; set; }

        public string AllowOnlyOneResponsePerUniqueUrlContent { get; set; }

    }

}

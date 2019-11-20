using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CopySurveyMeta.Models
{
    [Table("SurveyMetaElement")]
    public class SurveyMetaElementContent
    {
        [Key]
        public long ID { get; set; }

        public long SurveyMetaID { get; set; }

        public long SurveyMetaElementID { get; set; }

        public long LanguageID { get; set; }

        public string HeaderText { get; set; }

        public string QuestionText { get; set; }

        public string HelpText { get; set; }

        public string DescriptionContent { get; set; }

        public string DescriptionContentType { get; set; }

        public string ValidateMessageText { get; set; }

        public string VerifyMessageText { get; set; }

        public string IncludeOtherText { get; set; }

        public string VerifyOtherMessageText { get; set; }

    }
}

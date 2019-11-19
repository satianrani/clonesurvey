using System;
using System.Collections.Generic;
using System.Text;

namespace CopySurveyMeta.Models
{
    public class SurveyMeta
    {
        public long ID { get; set; }

        public long SurveyID { get; set; }

        public string SurveyName { get; set; }

        public long DefaultLanguageID { get; set; }

        public string LogoUrl { get; set; }

        public string LogoAlignment { get; set; }

        public double? LogoWidth { get; set; }

        public double? LogoHeight { get; set; }

        public bool? SkipWelcomePage { get; set; }

        public bool? ShowPowerBy { get; set; }

        public string FontName { get; set; }

        public string BackgroundImageUrl { get; set; }

        public string BackgroundColor { get; set; }

        public string QuestionTextColor { get; set; }

        public string AnswerTextColor { get; set; }

        public string ButtonBackgroundColor { get; set; }

        public string ValidateTextColor { get; set; }

        public string CustomCss { get; set; }

        public string SurveyFlowJson { get; set; }

        public bool? RedirectThankyouPage { get; set; }

        public string RedirectThankyouPageUrl { get; set; }

        public bool? AllowOnlyOneResponsePerUniqueUrl { get; set; }

        public bool? KeepAnswerState { get; set; }

        public long? UserID { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}

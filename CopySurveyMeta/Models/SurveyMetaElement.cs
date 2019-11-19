using System;
using System.Collections.Generic;
using System.Text;

namespace CopySurveyMeta.Models
{
    public class SurveyMetaElement
    {
        public long ID { get; set; }

        public long SurveyMetaID { get; set; }

        public int Index { get; set; }

        public string ElementKey { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public bool? Mandatory { get; set; }

        public string Alignment { get; set; }

        public string VerifyRegularExpression { get; set; }

        public int? MinimumValue { get; set; }

        public int? MaximumValue { get; set; }

        public bool? IncludeTime { get; set; }

        public string DisplayFormat { get; set; }

        public string DisplayRegion { get; set; }

        public string RatingDisplayOption { get; set; }

        public int? NumOfRatingChoice { get; set; }

        public int? RatingScaleFromNum { get; set; }

        public int? RatingScaleToNum { get; set; }

        public bool? IncludeOther { get; set; }

        public string MultipleChoiceDisplayLayout { get; set; }

        public int? RequiredNumOfChoice { get; set; }

        public bool? OtherUnselectAll { get; set; }

        public int? RequiredNumOfQuestionItemOneAnswer { get; set; }

        public int? RequiredNumOfQuestionItemMultipleAnswer { get; set; }

        public int? RequiredNumOfChoicePerQuestionItem { get; set; }

        public bool? IsRandom { get; set; }

        public bool? UnselectAllChoices { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? RequiredMinimumAnswer { get; set; }

        public string VerifyOtherRegularExpression { get; set; }

    }
}

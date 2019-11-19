using System;
using System.Collections.Generic;
using System.Text;

namespace CopySurveyMeta.Models
{
    public class SurveyMetaElementSubContent
    {
        public long ID { get; set; }

        public long SurveyMetaID { get; set; }

        public long SurveyMetaElementID { get; set; }

        public string SubType { get; set; }

        public long LanguageID { get; set; }

        public string ElementKey { get; set; }

        public string Name { get; set; }

        public string VerifyMinimumValueText { get; set; }

        public string VerifyMaximumValueText { get; set; }

        public string RequiredNumOfQuestionItemMultipleAnswerValidateText { get; set; }

        public string RequiredNumOfChoicePerQuestionItemValidateText { get; set; }

        public string SelectChoiceText { get; set; }

        public string StartLabelText { get; set; }

        public string EndLabelText { get; set; }

        public int? RatingLabelIndex { get; set; }

        public string RatingLabelText { get; set; }

        public string ScaleFromLabelText { get; set; }

        public string ScaleToLabelText { get; set; }

        public string MultipleChoiceText { get; set; }

        public string RankingChoiceText { get; set; }

        public string MatrixQuestionItemText { get; set; }

        public string MatrixQuestionChoiceText { get; set; }

        public bool? MultipleChoiceAppendText { get; set; }

        public bool? MultipleChoiceUnselectAllChoices { get; set; }

        public string MatrixDropdownQuestionItemElementKey { get; set; }

        public string MatrixDropdownChoiceElementKey { get; set; }

        public string MatrixDropdownChoiceText { get; set; }

    }
}

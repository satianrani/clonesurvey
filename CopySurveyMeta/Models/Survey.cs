using System; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace CopySurveyMeta.Model
{
    [Table("Survey")]
    public class Survey
    {
        [Key]
        public long ID { get; set; }

        public string Name { get; set; }

        public string BackgroundImageUrl { get; set; }

        public string BackgroundColor { get; set; }

        public string QuestionTextColor { get; set; }

        public string AnswerTextColor { get; set; }

        public string ValidateTextColor { get; set; }

        public string ButtonBackgroundColor { get; set; }

        public string CustomCss { get; set; }

        public string LogoUrl { get; set; }

        public string LogoAlignment { get; set; }

        public double LogoWidth { get; set; }

        public double LogoHeight { get; set; }

        public bool ShowPowerBy { get; set; }

        public bool SkipWelcomePage { get; set; }

        public string CustomLink { get; set; }

        public string PublishedKey { get; set; }

        public long UserID { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string FontName { get; set; }

        public bool RedirectThankyouPage { get; set; }

        public string RedirectThankyouPageUrl { get; set; }

        public bool AllowOnlyOneResponsePerUniqueUrl { get; set; }

        public bool IsTrash { get; set; }

    }
}

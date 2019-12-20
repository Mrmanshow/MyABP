using System.ComponentModel.DataAnnotations;

namespace MyABP.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
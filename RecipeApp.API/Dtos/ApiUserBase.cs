using System.ComponentModel.DataAnnotations;

namespace RecipeApp.API.Dtos
{
    public abstract class ApiUserBase
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

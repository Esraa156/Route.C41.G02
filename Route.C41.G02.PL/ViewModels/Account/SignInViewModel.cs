using System.ComponentModel.DataAnnotations;

namespace Route.C41.G02.PL.ViewModels.Account
{
    public class SignInViewModel
    {
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]

		[DataType(DataType.Password)]
        public string Passoword { get; set; }

        public  bool RememberMe {  get; set; }

    
    }
}

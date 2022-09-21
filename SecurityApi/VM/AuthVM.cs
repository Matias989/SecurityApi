using System.ComponentModel.DataAnnotations;

namespace SecurityApi.VM
{
    public class AuthVM
    {
        [StringLength(128),Required(ErrorMessage = "EmailUser is Requiered"), EmailAddress]
        public string EmailUser { get; set; }
        [StringLength(255),Required(ErrorMessage = "PasswordUser is Requiered")]
        public string PasswordUser { get; set; }
    }
}

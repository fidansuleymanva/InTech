namespace InTech_MVC.Areas.Admin.ViewModel
{
    public class MemberRegisterViewModel
    {
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "FullName size incorrent")]
        public string FullName { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "FullName size incorrent")]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "FullName size incorrent")]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "FullName size incorrent"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "FullName size incorrent"), DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}

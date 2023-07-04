namespace InTech_MVC.Areas.Admin.ViewModel
{
    public class AdminLoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "FullName size incorrent")]
        public string FullName { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "UserName size incorrent")]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

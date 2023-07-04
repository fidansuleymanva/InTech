namespace InTech_MVC.Models
{
    public class ClientsMembers1cs
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "Image size is too much!")]
        public string? Img { get; set; }
        [StringLength(maximumLength: 600, ErrorMessage = "Fullname size is too much!")]
        public string Fullname { get; set; }
        [StringLength(maximumLength: 1000, ErrorMessage = " Description size is too much!")]
        public string Description { get; set; }
        [NotMapped]
        public IFormFile? FromFile { get; set; }
    }
}

namespace InTech_MVC.Models
{
    public class OurTeamTitleMember
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "Image size is too much!")]
        public string? Img { get; set; }
        [StringLength(maximumLength: 600, ErrorMessage = "Fullname size is too much!")]
        public string Fullname { get; set; }
        [StringLength(maximumLength: 600, ErrorMessage = "Profession size is too much!")]
        public string Profession{ get; set; }
        [StringLength(maximumLength: 600, ErrorMessage = "About size is too much!")]
        public string About { get; set; }

        [NotMapped]
        public IFormFile? FromFile { get; set; }
    }
}

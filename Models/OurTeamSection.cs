namespace InTech_MVC.Models
{
	public class OurTeamSection
	{
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "Image size is too much!")]
        public string? Img { get; set; }
        [StringLength(maximumLength: 600, ErrorMessage = "Title size is too much!")]
        public string Title1 { get; set; }
        [StringLength(maximumLength: 600, ErrorMessage = "Title size is too much!")]
        public string Title2 { get; set; }
        //[StringLength(maximumLength: 600, ErrorMessage = "URL size is too much!")]
        //public string URL { get; set; }

        [NotMapped]
        public IFormFile? FromFile { get; set; }
    }
}

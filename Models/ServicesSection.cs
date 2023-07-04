namespace InTech_MVC.Models
{
	public class ServicesSection
	{
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "Image size is too much!")]
        public string? Img { get; set; }

        [NotMapped]
        public IFormFile? FromFile { get; set; }
    }
}

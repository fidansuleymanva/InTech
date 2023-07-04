namespace InTech_MVC.Models
{
	public class CardSection
	{

        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "Image size is too much!")]
        public string? Img { get; set; }
        [StringLength(maximumLength: 600, ErrorMessage = "Title size is too much!")]
        public string Title1 { get; set; }
        [StringLength(maximumLength: 600, ErrorMessage = "Description size is too much!")]
        public string Description { get; set; }
        
        [NotMapped]
        public IFormFile? FromFile { get; set; }
    }
}

namespace InTech_MVC.Models
{
    public class Photos2Title2
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 1000, ErrorMessage = "Title size is too much!")]
        public string Title { get; set; }
        [StringLength(maximumLength: 1000, ErrorMessage = " Description size is too much!")]
        public string Description { get; set; }
    }
}

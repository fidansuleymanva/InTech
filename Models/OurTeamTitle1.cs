namespace InTech_MVC.Models
{
    public class OurTeamTitle1
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 600, ErrorMessage = "Title size is too much!")]
        public string Title { get; set; }
    }
}

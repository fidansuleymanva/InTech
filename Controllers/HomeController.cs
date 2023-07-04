
namespace InTech_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                CardSections = _dataContext.CardSection.ToList(),
                HeaderSections = _dataContext.HeaderSection.ToList(),
                PhotosSections = _dataContext.PhotosSections.ToList(),
                Photos2Sections = _dataContext.Photo2Sections.ToList(),
                OurTeamsSections = _dataContext.OurTeamSections.ToList(),
                ServicesSections = _dataContext.ServicesSections.ToList(),
                SosialMediaIcons = _dataContext.SosialMediaIcons.ToList(),
                OurTeamTitle1s = _dataContext.OurTeamTitle1s.ToList(),
                OurTeamTitle2s = _dataContext.OurTeamTitle2s.ToList(),
                OurTeamTitleMember1s = _dataContext.OurTeamTitleMember1s.ToList(),
                OurTeamMembers = _dataContext.OurTeamMember.ToList(),
                ServicesSectionTitles = _dataContext.ServicesSectionTitle.ToList(),
                SeeMoreLinks = _dataContext.SeeMoreLinks.ToList(),
                Photos2Title1s = _dataContext.Photos2Title1s.ToList(),
                Photos2Title2s = _dataContext.Photos2Title2s.ToList(),
                ClientsTitle1s = _dataContext.clientsTitle1s.ToList(),
                ClientsTitle2s = _dataContext.clientsTitle2s.ToList(),
                ClientsMembers = _dataContext.clientsMember1s.ToList(),
                ClientsMembers1cs = _dataContext.ClientsMembers1Cs.ToList(), 
                ProjectTitle1s = _dataContext.ProjectTitle1s.ToList(),
                
            };
            return View(homeViewModel);

        }


    }
}
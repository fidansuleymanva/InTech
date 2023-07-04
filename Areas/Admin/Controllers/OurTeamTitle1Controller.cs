
namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class OurTeamTitle1Controller : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public OurTeamTitle1Controller(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.OurTeamTitle1s.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<OurTeamTitle1>.Create(query, 3, page);
            return View(PaginationList);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OurTeamTitle1 ourTeamTitle1)
        {
            if (!ModelState.IsValid) return View();
            _dataContext.Add(ourTeamTitle1);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            OurTeamTitle1 ourTeamTitle1 = _dataContext.OurTeamTitle1s.FirstOrDefault(x => x.Id == id);
            if (ourTeamTitle1 == null) return View();
            return View(ourTeamTitle1);
        }
        [HttpPost]
        public IActionResult Update(OurTeamTitle1 ourTeamTitle1)
        {
            if (ourTeamTitle1 is null) return View();

            OurTeamTitle1  exsistourteamtitle1 = _dataContext.OurTeamTitle1s.FirstOrDefault(x => x.Id == ourTeamTitle1.Id);
            if (exsistourteamtitle1 == null) return View();
            if (!ModelState.IsValid) return View(exsistourteamtitle1);

            exsistourteamtitle1.Title = ourTeamTitle1.Title;
            _dataContext.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            OurTeamTitle1 ourTeamTitle1 = _dataContext.OurTeamTitle1s.FirstOrDefault(x => x.Id == id);
            _dataContext.OurTeamTitle1s.Remove(ourTeamTitle1);
            _dataContext.SaveChanges();
            return View(ourTeamTitle1);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            OurTeamTitle1 ourTeamTitle1 = _dataContext.OurTeamTitle1s.Find(id);
            if (ourTeamTitle1 == null) return View();

            _dataContext.OurTeamTitle1s.Remove(ourTeamTitle1);
            _dataContext.SaveChanges();
            return Ok();


        }

    }
}

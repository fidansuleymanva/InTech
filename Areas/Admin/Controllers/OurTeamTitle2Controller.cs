
namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class OurTeamTitle2Controller : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public OurTeamTitle2Controller(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.OurTeamTitle2s.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<OurTeamTitle2>.Create(query, 3, page);
            return View(PaginationList);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OurTeamTitle2 ourTeamTitle2)
        {
            if (!ModelState.IsValid) return View();
            _dataContext.Add(ourTeamTitle2);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            OurTeamTitle2 ourTeamTitle2 = _dataContext.OurTeamTitle2s.FirstOrDefault(x => x.Id == id);
            if (ourTeamTitle2 == null) return View();
            return View(ourTeamTitle2);
        }
        [HttpPost]
        public IActionResult Update(OurTeamTitle2 ourTeamTitle2)
        {
            if (ourTeamTitle2 is null) return View();

            OurTeamTitle2 exsistourteamtitle2 = _dataContext.OurTeamTitle2s.FirstOrDefault(x => x.Id == ourTeamTitle2.Id);
            if (exsistourteamtitle2 == null) return View();
            if (!ModelState.IsValid) return View(exsistourteamtitle2);

            exsistourteamtitle2.Title = ourTeamTitle2.Title;
            exsistourteamtitle2.Description = ourTeamTitle2.Description;
            _dataContext.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            OurTeamTitle2 ourTeamTitle2 = _dataContext.OurTeamTitle2s.FirstOrDefault(x => x.Id == id);
            _dataContext.OurTeamTitle2s.Remove(ourTeamTitle2);
            _dataContext.SaveChanges();
            return View(ourTeamTitle2);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            OurTeamTitle2 ourTeamTitle2 = _dataContext.OurTeamTitle2s.Find(id);
            if (ourTeamTitle2 == null) return View();

            _dataContext.OurTeamTitle2s.Remove(ourTeamTitle2);
            _dataContext.SaveChanges();
            return Ok();


        }
    }
}

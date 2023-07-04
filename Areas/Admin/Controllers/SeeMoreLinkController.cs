

namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SeeMoreLinkController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public SeeMoreLinkController(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.SeeMoreLinks.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<SeeMoreLink>.Create(query, 3, page);
            return View(PaginationList);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SeeMoreLink seeMoreLink)
        {
            if (!ModelState.IsValid) return View();
            _dataContext.Add(seeMoreLink);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            SeeMoreLink seeMoreLink = _dataContext.SeeMoreLinks.FirstOrDefault(x => x.Id == id);
            if (seeMoreLink == null) return View();
            return View(seeMoreLink);
        }
        [HttpPost]
        public IActionResult Update(SeeMoreLink seeMoreLink)
        {
            if (seeMoreLink is null) return View();

            SeeMoreLink exsistSeeMoreLink = _dataContext.SeeMoreLinks.FirstOrDefault(x => x.Id == seeMoreLink.Id);
            if (exsistSeeMoreLink == null) return View();
            if (!ModelState.IsValid) return View(exsistSeeMoreLink);
            exsistSeeMoreLink.URL = seeMoreLink.URL;
            _dataContext.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            SeeMoreLink seeMoreLink = _dataContext.SeeMoreLinks.FirstOrDefault(x => x.Id == id);
            _dataContext.SeeMoreLinks.Remove(seeMoreLink);
            _dataContext.SaveChanges();
            return View(seeMoreLink);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            SeeMoreLink seeMoreLink = _dataContext.SeeMoreLinks.Find(id);
            if (seeMoreLink == null) return View();

            _dataContext.SeeMoreLinks.Remove(seeMoreLink);
            _dataContext.SaveChanges();
            return Ok();


        }

    }
}

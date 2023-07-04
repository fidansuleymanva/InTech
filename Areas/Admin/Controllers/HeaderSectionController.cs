
namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class HeaderSectionController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public HeaderSectionController(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.HeaderSection.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<HeaderSection>.Create(query, 3, page);
            return View(PaginationList);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(HeaderSection headerSection)
        {
            if (!ModelState.IsValid) return View();
            _dataContext.Add(headerSection);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            HeaderSection headerSection = _dataContext.HeaderSection.FirstOrDefault(x => x.Id == id);
            if (headerSection == null) return View();
            return View(headerSection);
        }
        [HttpPost]
        public IActionResult Update(HeaderSection headerSection)
        {
            if (headerSection is null) return View();

            HeaderSection exsistheadersection = _dataContext.HeaderSection.FirstOrDefault(x => x.Id == headerSection.Id);
            if (exsistheadersection == null) return View();
            if (!ModelState.IsValid) return View(exsistheadersection);

            exsistheadersection.Title = headerSection.Title;
            exsistheadersection.Description = headerSection.Description;
            exsistheadersection.URL = headerSection.URL;
            _dataContext.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            HeaderSection headerSection = _dataContext.HeaderSection.FirstOrDefault(x => x.Id == id);
            _dataContext.HeaderSection.Remove(headerSection);
            _dataContext.SaveChanges();
            return View(headerSection);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            HeaderSection headerSection = _dataContext.HeaderSection.Find(id);
            if (headerSection == null) return View();

            _dataContext.HeaderSection.Remove(headerSection);
            _dataContext.SaveChanges();
            return Ok();


        }

    }
}


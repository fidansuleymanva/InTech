

namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class Photos2Title2Controller : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public Photos2Title2Controller(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.Photos2Title2s.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<Photos2Title2>.Create(query, 3, page);
            return View(PaginationList);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Photos2Title2 photos2Title2)
        {
            if (!ModelState.IsValid) return View();
            _dataContext.Add(photos2Title2);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Photos2Title2 photos2Title2 = _dataContext.Photos2Title2s.FirstOrDefault(x => x.Id == id);
            if (photos2Title2 == null) return View();
            return View(photos2Title2);
        }
        [HttpPost]
        public IActionResult Update(Photos2Title2 photos2Title2)
        {
            if (photos2Title2 is null) return View();

            Photos2Title2 exsistphotos2Title2 = _dataContext.Photos2Title2s.FirstOrDefault(x => x.Id == photos2Title2.Id);
            if (exsistphotos2Title2 == null) return View();
            if (!ModelState.IsValid) return View(exsistphotos2Title2);

            exsistphotos2Title2.Title = photos2Title2.Title;
            exsistphotos2Title2.Description = photos2Title2.Description;
            _dataContext.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Photos2Title2 photos2Title2 = _dataContext.Photos2Title2s.FirstOrDefault(x => x.Id == id);
            _dataContext.Photos2Title2s.Remove(photos2Title2);
            _dataContext.SaveChanges();
            return View(photos2Title2);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Photos2Title2 photos2Title2 = _dataContext.Photos2Title2s.Find(id);
            if (photos2Title2 == null) return View();

            _dataContext.Photos2Title2s.Remove(photos2Title2);
            _dataContext.SaveChanges();
            return Ok();


        }
    }
}

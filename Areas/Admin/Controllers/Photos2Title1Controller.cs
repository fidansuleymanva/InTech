using Microsoft.AspNetCore.Mvc;

namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class Photos2Title1Controller : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public Photos2Title1Controller(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.Photos2Title1s.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<Photos2Title1>.Create(query, 3, page);
            return View(PaginationList);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Photos2Title1 photos2Title1)
        {
            if (!ModelState.IsValid) return View();
            _dataContext.Add(photos2Title1);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Photos2Title1 photos2Title1 = _dataContext.Photos2Title1s.FirstOrDefault(x => x.Id == id);
            if (photos2Title1 == null) return View();
            return View(photos2Title1);
        }
        [HttpPost]
        public IActionResult Update(Photos2Title1 photos2Title1)
        {
            if (photos2Title1 is null) return View();

            OurTeamTitle1 exsistphotos2Title1 = _dataContext.OurTeamTitle1s.FirstOrDefault(x => x.Id == photos2Title1.Id);
            if (exsistphotos2Title1 == null) return View();
            if (!ModelState.IsValid) return View(exsistphotos2Title1);

            exsistphotos2Title1.Title = photos2Title1.Title;
            _dataContext.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Photos2Title1 photos2Title1 = _dataContext.Photos2Title1s.FirstOrDefault(x => x.Id == id);
            _dataContext.Photos2Title1s.Remove(photos2Title1);
            _dataContext.SaveChanges();
            return View(photos2Title1);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Photos2Title1 photos2Title1 = _dataContext.Photos2Title1s.Find(id);
            if (photos2Title1 == null) return View();

            _dataContext.Photos2Title1s.Remove(photos2Title1);
            _dataContext.SaveChanges();
            return Ok();


        }

    }
}

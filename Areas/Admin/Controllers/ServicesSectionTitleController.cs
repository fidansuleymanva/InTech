using Microsoft.AspNetCore.Mvc;

namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class ServicesSectionTitleController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;
        public ServicesSectionTitleController(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.ServicesSectionTitle.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<ServicesSectionTitle>.Create(query, 3, page);
            return View(PaginationList);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ServicesSectionTitle servicesSectionTitle)
        {
            if (!ModelState.IsValid) return View();
            _dataContext.Add(servicesSectionTitle);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
           ServicesSectionTitle servicesSectionTitle = _dataContext.ServicesSectionTitle.FirstOrDefault(x => x.Id == id);
            if (servicesSectionTitle == null) return View();
            return View(servicesSectionTitle);
        }
        [HttpPost]
        public IActionResult Update(ServicesSectionTitle servicesSectionTitle)
        {
            if (servicesSectionTitle is null) return View();

            OurTeamTitle2 exsistservicesSectionTitle = _dataContext.OurTeamTitle2s.FirstOrDefault(x => x.Id == servicesSectionTitle.Id);
            if (exsistservicesSectionTitle == null) return View();
            if (!ModelState.IsValid) return View(exsistservicesSectionTitle);

            exsistservicesSectionTitle.Title = servicesSectionTitle.Title;
            exsistservicesSectionTitle.Description = servicesSectionTitle.Description;
            _dataContext.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
           ServicesSectionTitle servicesSectionTitle= _dataContext.ServicesSectionTitle.FirstOrDefault(x => x.Id == id);
            _dataContext.ServicesSectionTitle.Remove(servicesSectionTitle);
            _dataContext.SaveChanges();
            return View(servicesSectionTitle);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            ServicesSectionTitle servicesSectionTitle = _dataContext.ServicesSectionTitle.Find(id);
            if (servicesSectionTitle == null) return View();

            _dataContext.ServicesSectionTitle.Remove(servicesSectionTitle);
            _dataContext.SaveChanges();
            return Ok();


        }
    }
}

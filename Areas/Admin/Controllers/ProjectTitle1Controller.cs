using Microsoft.AspNetCore.Mvc;

namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProjectTitle1Controller : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public ProjectTitle1Controller(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.ProjectTitle1s.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<ProjectTitle1>.Create(query, 3, page);
            return View(PaginationList);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProjectTitle1 projectTitle1)
        {
            if (!ModelState.IsValid) return View();
            _dataContext.Add(projectTitle1);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ProjectTitle1 projectTitle1 = _dataContext.ProjectTitle1s.FirstOrDefault(x => x.Id == id);
            if (projectTitle1 == null) return View();
            return View(projectTitle1);
        }
        [HttpPost]
        public IActionResult Update(ProjectTitle1 projectTitle1)
        {
            if (projectTitle1 is null) return View();

            ProjectTitle1 exsistprojectTitle1 = _dataContext.ProjectTitle1s.FirstOrDefault(x => x.Id == projectTitle1.Id);
            if (exsistprojectTitle1 == null) return View();
            if (!ModelState.IsValid) return View(exsistprojectTitle1);

            exsistprojectTitle1.Title = projectTitle1.Title;
            _dataContext.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            ProjectTitle1 projectTitle1 = _dataContext.ProjectTitle1s.FirstOrDefault(x => x.Id == id);
            _dataContext.ProjectTitle1s.Remove(projectTitle1);
            _dataContext.SaveChanges();
            return View(projectTitle1);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            ProjectTitle1 projectTitle1 = _dataContext.ProjectTitle1s.Find(id);
            if (projectTitle1 == null) return View();

            _dataContext.ProjectTitle1s.Remove(projectTitle1);
            _dataContext.SaveChanges();
            return Ok();


        }

    }
}

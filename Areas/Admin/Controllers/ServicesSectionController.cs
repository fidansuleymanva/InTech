
namespace InTech_MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ServicesSectionController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly IWebHostEnvironment _env;

		public ServicesSectionController(DataContext dataContext, IWebHostEnvironment env)
		{
			_dataContext = dataContext;
			_env = env;
		}
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.ServicesSections.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<ServicesSection>.Create(query, 3, page);
            return View(PaginationList);


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ServicesSection servicesSection)
        {
            if (!ModelState.IsValid)
            {
                return View(servicesSection);
            }

            if (servicesSection.FromFile == null || servicesSection.FromFile.Length == 0)
            {
                ModelState.AddModelError("FromFile", "Please select an image file.");
                return View(servicesSection);
            }

            if (servicesSection.FromFile.ContentType != "image/png" && servicesSection.FromFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("FromFile", "Only PNG and JPEG image files are allowed.");
                return View(servicesSection);
            }

            if (servicesSection.FromFile.Length > 4145728)
            {
                ModelState.AddModelError("FromFile", "The image file size must be less than 4 MB.");
                return View(servicesSection);
            }

            try
            {
                servicesSection.Img = FileManager.SaveFile(_env.WebRootPath, "upload/servicessection", servicesSection.FromFile);
                _dataContext.ServicesSections.Add(servicesSection);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file upload or database operations.
                ModelState.AddModelError(string.Empty, "An error occurred while creating the slider.");
                return View(servicesSection);
            }



        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ServicesSection servicesSection = _dataContext.ServicesSections.Find(id);
            if (servicesSection == null) return NotFound();
            return View(servicesSection);
        }
        [HttpPost]
        public IActionResult Update(ServicesSection servicesSection)
        {
            ServicesSection existservicessection = _dataContext.ServicesSections.Find(servicesSection.Id);
            if (existservicessection == null) return View(existservicessection);
            if (servicesSection.FromFile != null)
            {

                if (servicesSection.FromFile.ContentType != "image/png" && servicesSection.FromFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
                    return View();
                }
                if (servicesSection.FromFile.Length > 4145728)
                {
                    ModelState.AddModelError("ImageFile", "It can be 4 Mb!");
                    return View();
                }

                string name = FileManager.SaveFile(_env.WebRootPath, "upload/servicessection", servicesSection.FromFile);
                FileManager.DeleteFile(_env.WebRootPath, "upload/servicessection", existservicessection.Img);
                existservicessection.Img = name;
            }
            _dataContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            ServicesSection servicesSection = _dataContext.ServicesSections.Find(id);
            if (servicesSection == null) return NotFound();
            return View(servicesSection);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            ServicesSection servicesSection = _dataContext.ServicesSections.Find(id);
            if (servicesSection == null) return NotFound();
            if (servicesSection.Img != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "upload/servicessection", servicesSection.Img);
            }

            _dataContext.ServicesSections.Remove(servicesSection);
            _dataContext.SaveChanges();

            // Redirect to the Index action to refresh the page
            return RedirectToAction("Index");
        }

    }
}


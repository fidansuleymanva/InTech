
namespace InTech_MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class OurTeamSectionController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly IWebHostEnvironment _env;

		public OurTeamSectionController(DataContext dataContext, IWebHostEnvironment env)
		{
			_dataContext = dataContext;
			_env = env;
		}
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.OurTeamSections.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<OurTeamSection>.Create(query, 3, page);
            return View(PaginationList);


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OurTeamSection ourTeamSection)
        {
            if (!ModelState.IsValid)
            {
                return View(ourTeamSection);
            }

            if (ourTeamSection.FromFile == null || ourTeamSection.FromFile.Length == 0)
            {
                ModelState.AddModelError("FromFile", "Please select an image file.");
                return View(ourTeamSection);
            }

            if (ourTeamSection.FromFile.ContentType != "image/png" && ourTeamSection.FromFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("FromFile", "Only PNG and JPEG image files are allowed.");
                return View(ourTeamSection);
            }

            if (ourTeamSection.FromFile.Length > 4145728)
            {
                ModelState.AddModelError("FromFile", "The image file size must be less than 4 MB.");
                return View(ourTeamSection);
            }

            try
            {
                ourTeamSection.Img = FileManager.SaveFile(_env.WebRootPath, "upload/ourteamsection", ourTeamSection.FromFile);
                _dataContext.OurTeamSections.Add(ourTeamSection);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file upload or database operations.
                ModelState.AddModelError(string.Empty, "An error occurred while creating the slider.");
                return View(ourTeamSection);
            }



        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            OurTeamSection ourTeamSection = _dataContext.OurTeamSections.Find(id);
            if (ourTeamSection == null) return NotFound();
            return View(ourTeamSection);
        }
        [HttpPost]
        public IActionResult Update(OurTeamSection ourTeamSection)
        {
            OurTeamSection existourteamsection = _dataContext.OurTeamSections.Find(ourTeamSection.Id);
            if (existourteamsection == null) return View(existourteamsection);
            if (ourTeamSection.FromFile != null)
            {

                if (ourTeamSection.FromFile.ContentType != "image/png" && ourTeamSection.FromFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
                    return View();
                }
                if (ourTeamSection.FromFile.Length > 4145728)
                {
                    ModelState.AddModelError("ImageFile", "It can be 4 Mb!");
                    return View();
                }

                string name = FileManager.SaveFile(_env.WebRootPath, "upload/ourteamsection", ourTeamSection.FromFile);
                FileManager.DeleteFile(_env.WebRootPath, "upload/ourteamsection", existourteamsection.Img);
                existourteamsection.Img = name;
            }
            existourteamsection.Title1 = ourTeamSection.Title1;
            existourteamsection.Title2 = ourTeamSection.Title2;
            _dataContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            OurTeamSection ourTeamSection = _dataContext.OurTeamSections.Find(id);
            if (ourTeamSection == null) return NotFound();
            return View(ourTeamSection);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            OurTeamSection ourTeamSection = _dataContext.OurTeamSections.Find(id);
            if (ourTeamSection == null) return NotFound();
            if (ourTeamSection.Img != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "upload/ourteamsection", ourTeamSection.Img);
            }

            _dataContext.OurTeamSections.Remove(ourTeamSection);
            _dataContext.SaveChanges();

            // Redirect to the Index action to refresh the page
            return RedirectToAction("Index");
        }

    }
}


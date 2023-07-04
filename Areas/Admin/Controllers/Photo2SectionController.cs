
namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class Photo2SectionController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly IWebHostEnvironment _env;

		public Photo2SectionController(DataContext dataContext, IWebHostEnvironment env)
		{
			_dataContext = dataContext;
			_env = env;
		}
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.Photo2Sections.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<Photo2Section>.Create(query, 3, page);
            return View(PaginationList);


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Photo2Section photo2Section)
        {
            if (!ModelState.IsValid)
            {
                return View(photo2Section);
            }

            if (photo2Section.FromFile == null || photo2Section.FromFile.Length == 0)
            {
                ModelState.AddModelError("FromFile", "Please select an image file.");
                return View(photo2Section);
            }

            if (photo2Section.FromFile.ContentType != "image/png" && photo2Section.FromFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("FromFile", "Only PNG and JPEG image files are allowed.");
                return View(photo2Section);
            }

            if (photo2Section.FromFile.Length > 4145728)
            {
                ModelState.AddModelError("FromFile", "The image file size must be less than 4 MB.");
                return View(photo2Section);
            }

            try
            {
                photo2Section.Img = FileManager.SaveFile(_env.WebRootPath, "upload/photo2section", photo2Section.FromFile);
                _dataContext.Photo2Sections.Add(photo2Section);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file upload or database operations.
                ModelState.AddModelError(string.Empty, "An error occurred while creating the slider.");
                return View(photo2Section);
            }



        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Photo2Section photo2Section = _dataContext.Photo2Sections.Find(id);
            if (photo2Section == null) return NotFound();
            return View(photo2Section);
        }
        [HttpPost]
        public IActionResult Update(Photo2Section photo2Section)
        {
            Photo2Section existphoto2Section = _dataContext.Photo2Sections.Find(photo2Section.Id);
            if (existphoto2Section == null) return View(existphoto2Section);
            if (photo2Section.FromFile != null)
            {

                if (photo2Section.FromFile.ContentType != "image/png" && photo2Section.FromFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
                    return View();
                }
                if (photo2Section.FromFile.Length > 4145728)
                {
                    ModelState.AddModelError("ImageFile", "It can be 4 Mb!");
                    return View();
                }

                string name = FileManager.SaveFile(_env.WebRootPath, "upload/photo2section", photo2Section.FromFile);
                FileManager.DeleteFile(_env.WebRootPath, "upload/photo2section", existphoto2Section.Img);
                existphoto2Section.Img = name;
            }
            _dataContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Photo2Section photo2Section = _dataContext.Photo2Sections.Find(id);
            if (photo2Section == null) return NotFound();
            return View(photo2Section);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Photo2Section photo2Section = _dataContext.Photo2Sections.Find(id);
            if (photo2Section == null) return NotFound();
            if (photo2Section.Img != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "upload/photo2section", photo2Section.Img);
            }

            _dataContext.Photo2Sections.Remove(photo2Section);
            _dataContext.SaveChanges();

            // Redirect to the Index action to refresh the page
            return RedirectToAction("Index");
        }

    }
}


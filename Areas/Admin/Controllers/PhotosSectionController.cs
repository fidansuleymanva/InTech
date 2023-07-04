
namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class PhotosSectionController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public PhotosSectionController(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.PhotosSections.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<PhotosSection>.Create(query, 3, page);
            return View(PaginationList);


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PhotosSection photosSection)
        {
            if (!ModelState.IsValid)
            {
                return View(photosSection);
            }

            if (photosSection.FromFile == null || photosSection.FromFile.Length == 0)
            {
                ModelState.AddModelError("FromFile", "Please select an image file.");
                return View(photosSection);
            }

            if (photosSection.FromFile.ContentType != "image/png" && photosSection.FromFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("FromFile", "Only PNG and JPEG image files are allowed.");
                return View(photosSection);
            }

            if (photosSection.FromFile.Length > 4145728)
            {
                ModelState.AddModelError("FromFile", "The image file size must be less than 4 MB.");
                return View(photosSection);
            }

            try
            {
                photosSection.Img = FileManager.SaveFile(_env.WebRootPath, "upload/photossection", photosSection.FromFile);
                _dataContext.PhotosSections.Add(photosSection);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file upload or database operations.
                ModelState.AddModelError(string.Empty, "An error occurred while creating the slider.");
                return View(photosSection);
            }



        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            PhotosSection photosSection = _dataContext.PhotosSections.Find(id);
            if (photosSection == null) return NotFound();
            return View(photosSection);
        }
        [HttpPost]
        public IActionResult Update(PhotosSection photosSection)
        {
            PhotosSection existphotossection = _dataContext.PhotosSections.Find(photosSection.Id);
            if (existphotossection == null) return View(existphotossection);
            if (photosSection.FromFile != null)
            {

                if (photosSection.FromFile.ContentType != "image/png" && photosSection.FromFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
                    return View();
                }
                if (photosSection.FromFile.Length > 4145728)
                {
                    ModelState.AddModelError("ImageFile", "It can be 4 Mb!");
                    return View();
                }

                string name = FileManager.SaveFile(_env.WebRootPath, "upload/cardsections", photosSection.FromFile);
                FileManager.DeleteFile(_env.WebRootPath, "upload/cardsections", existphotossection.Img);
                existphotossection.Img = name;
            }
            existphotossection.Title = photosSection.Title;
            existphotossection.Description = photosSection.Description;
            _dataContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
           PhotosSection photosSection = _dataContext.PhotosSections.Find(id);
            if (photosSection == null) return NotFound();
            return View(photosSection);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            PhotosSection photosSection = _dataContext.PhotosSections.Find(id);
            if (photosSection == null) return NotFound();
            if (photosSection.Img != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "upload/cardsections", photosSection.Img);
            }

            _dataContext.PhotosSections.Remove(photosSection);
            _dataContext.SaveChanges();

            // Redirect to the Index action to refresh the page
            return RedirectToAction("Index");
        }

    }

}

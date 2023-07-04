
namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class OurTeamMemberController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public OurTeamMemberController(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.OurTeamMember.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<OurTeamMember>.Create(query, 3, page);
            return View(PaginationList);


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OurTeamMember ourTeamMember)
        {
            if (!ModelState.IsValid)
            {
                return View(ourTeamMember);
            }

            if (ourTeamMember.FromFile == null || ourTeamMember.FromFile.Length == 0)
            {
                ModelState.AddModelError("FromFile", "Please select an image file.");
                return View(ourTeamMember);
            }

            if (ourTeamMember.FromFile.ContentType != "image/png" && ourTeamMember.FromFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("FromFile", "Only PNG and JPEG image files are allowed.");
                return View(ourTeamMember);
            }

            if (ourTeamMember.FromFile.Length > 4145728)
            {
                ModelState.AddModelError("FromFile", "The image file size must be less than 4 MB.");
                return View(ourTeamMember);
            }

            try
            {
                ourTeamMember.Img = FileManager.SaveFile(_env.WebRootPath, "upload/ourteammember", ourTeamMember.FromFile);
                _dataContext.OurTeamMember.Add(ourTeamMember);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file upload or database operations.
                ModelState.AddModelError(string.Empty, "An error occurred while creating the slider.");
                return View(ourTeamMember);
            }



        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            OurTeamMember ourTeamMember = _dataContext.OurTeamMember.Find(id);
            if (ourTeamMember == null) return NotFound();
            return View(ourTeamMember);
        }
        [HttpPost]
        public IActionResult Update(OurTeamMember ourTeamMember)
        {
           OurTeamMember existourTeamMember = _dataContext.OurTeamMember.Find(ourTeamMember.Id);
            if (existourTeamMember == null) return View(existourTeamMember);
            if (existourTeamMember.FromFile != null)
            {

                if (ourTeamMember.FromFile.ContentType != "image/png" && ourTeamMember.FromFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
                    return View();
                }
                if (ourTeamMember.FromFile.Length > 4145728)
                {
                    ModelState.AddModelError("ImageFile", "It can be 4 Mb!");
                    return View();
                }

                string name = FileManager.SaveFile(_env.WebRootPath, "upload/ourteammember", ourTeamMember.FromFile);
                FileManager.DeleteFile(_env.WebRootPath, "upload/ourteammember", existourTeamMember.Img);
                existourTeamMember.Img = name;
            }
            existourTeamMember.Fullname = ourTeamMember.Fullname;
            existourTeamMember.Profession = ourTeamMember.Profession;
            existourTeamMember.About = ourTeamMember.About;
            _dataContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            OurTeamMember ourTeamMember = _dataContext.OurTeamMember.Find(id);
            if (ourTeamMember == null) return NotFound();
            return View(ourTeamMember);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            OurTeamMember ourTeamMember = _dataContext.OurTeamMember.Find(id);
            if (ourTeamMember == null) return NotFound();
            if (ourTeamMember.Img != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "upload/ourteammember", ourTeamMember.Img);
            }

            _dataContext.OurTeamMember.Remove(ourTeamMember);
            _dataContext.SaveChanges();

            // Redirect to the Index action to refresh the page
            return RedirectToAction("Index");
        }

    }
}

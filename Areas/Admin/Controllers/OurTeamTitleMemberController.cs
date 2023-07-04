
namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class OurTeamTitleMemberController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public OurTeamTitleMemberController(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.OurTeamTitleMember1s.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<OurTeamTitleMember>.Create(query, 3, page);
            return View(PaginationList);


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OurTeamTitleMember ourTeamTitleMember)
        {
            if (!ModelState.IsValid)
            {
                return View(ourTeamTitleMember);
            }

            if (ourTeamTitleMember.FromFile == null || ourTeamTitleMember.FromFile.Length == 0)
            {
                ModelState.AddModelError("FromFile", "Please select an image file.");
                return View(ourTeamTitleMember);
            }

            if (ourTeamTitleMember.FromFile.ContentType != "image/png" && ourTeamTitleMember.FromFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("FromFile", "Only PNG and JPEG image files are allowed.");
                return View(ourTeamTitleMember);
            }

            if (ourTeamTitleMember.FromFile.Length > 4145728)
            {
                ModelState.AddModelError("FromFile", "The image file size must be less than 4 MB.");
                return View(ourTeamTitleMember);
            }

            try
            {
                ourTeamTitleMember.Img = FileManager.SaveFile(_env.WebRootPath, "upload/ourteamtitlemember", ourTeamTitleMember.FromFile);
                _dataContext.OurTeamTitleMember1s.Add(ourTeamTitleMember);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file upload or database operations.
                ModelState.AddModelError(string.Empty, "An error occurred while creating the slider.");
                return View(ourTeamTitleMember);
            }



        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            OurTeamTitleMember teamTitleMember = _dataContext.OurTeamTitleMember1s.Find(id);
            if (teamTitleMember == null) return NotFound();
            return View(teamTitleMember);
        }
        [HttpPost]
        public IActionResult Update(OurTeamTitleMember ourTeamTitleMember)
        {
            OurTeamTitleMember existourTeamTitleMember = _dataContext.OurTeamTitleMember1s.Find(ourTeamTitleMember.Id);
            if (existourTeamTitleMember == null) return View(existourTeamTitleMember);
            if (ourTeamTitleMember.FromFile != null)
            {

                if (ourTeamTitleMember.FromFile.ContentType != "image/png" && ourTeamTitleMember.FromFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
                    return View();
                }
                if (ourTeamTitleMember.FromFile.Length > 4145728)
                {
                    ModelState.AddModelError("ImageFile", "It can be 4 Mb!");
                    return View();
                }

                string name = FileManager.SaveFile(_env.WebRootPath, "upload/ourteamtitlemember", ourTeamTitleMember.FromFile);
                FileManager.DeleteFile(_env.WebRootPath, "upload/ourteamtitlemember", existourTeamTitleMember.Img);
                existourTeamTitleMember.Img = name;
            }
            existourTeamTitleMember.Fullname = ourTeamTitleMember.Fullname;
            existourTeamTitleMember.Profession = ourTeamTitleMember.Profession;
            existourTeamTitleMember.About = ourTeamTitleMember.About;
            _dataContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            OurTeamTitleMember ourTeamTitleMember = _dataContext.OurTeamTitleMember1s.Find(id);
            if (ourTeamTitleMember == null) return NotFound();
            return View(ourTeamTitleMember);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            OurTeamTitleMember ourTeamTitleMember = _dataContext.OurTeamTitleMember1s.Find(id);
            if (ourTeamTitleMember == null) return NotFound();
            if (ourTeamTitleMember.Img != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "upload/ourteamtitlemember", ourTeamTitleMember.Img);
            }

            _dataContext.OurTeamTitleMember1s.Remove(ourTeamTitleMember);
            _dataContext.SaveChanges();

            // Redirect to the Index action to refresh the page
            return RedirectToAction("Index");
        }
    }
}

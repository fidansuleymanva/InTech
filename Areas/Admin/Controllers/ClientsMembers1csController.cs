
namespace InTech_MVC.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ClientsMembers1csController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public ClientsMembers1csController(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.ClientsMembers1Cs.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<ClientsMembers1cs>.Create(query, 3, page);
            return View(PaginationList);


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClientsMembers1cs clientsMembers1Cs)
        {
            if (!ModelState.IsValid)
            {
                return View(clientsMembers1Cs);
            }

            if (clientsMembers1Cs.FromFile == null || clientsMembers1Cs.FromFile.Length == 0)
            {
                ModelState.AddModelError("FromFile", "Please select an image file.");
                return View(clientsMembers1Cs);
            }

            if (clientsMembers1Cs.FromFile.ContentType != "image/png" && clientsMembers1Cs.FromFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("FromFile", "Only PNG and JPEG image files are allowed.");
                return View(clientsMembers1Cs);
            }

            if (clientsMembers1Cs.FromFile.Length > 4145728)
            {
                ModelState.AddModelError("FromFile", "The image file size must be less than 4 MB.");
                return View(clientsMembers1Cs);
            }

            try
            {
                clientsMembers1Cs.Img = FileManager.SaveFile(_env.WebRootPath, "upload/clientsmember", clientsMembers1Cs.FromFile);
                _dataContext.ClientsMembers1Cs.Add(clientsMembers1Cs);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file upload or database operations.
                ModelState.AddModelError(string.Empty, "An error occurred while creating the slider.");
                return View(clientsMembers1Cs);
            }



        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ClientsMembers1cs clientsMembers1Cs = _dataContext.ClientsMembers1Cs.Find(id);
            if (clientsMembers1Cs == null) return NotFound();
            return View(clientsMembers1Cs);
        }
        [HttpPost]
        public IActionResult Update(ClientsMembers1cs clientsMembers1Cs)
        {
            ClientsMembers1cs existclientsMembers1Cs = _dataContext.ClientsMembers1Cs.Find(clientsMembers1Cs.Id);
            if (existclientsMembers1Cs == null) return View(existclientsMembers1Cs);
            if (existclientsMembers1Cs.FromFile != null)
            {

                if (clientsMembers1Cs.FromFile.ContentType != "image/png" && clientsMembers1Cs.FromFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
                    return View();
                }
                if (clientsMembers1Cs.FromFile.Length > 4145728)
                {
                    ModelState.AddModelError("ImageFile", "It can be 4 Mb!");
                    return View();
                }

                string name = FileManager.SaveFile(_env.WebRootPath, "upload/clientsmember", clientsMembers1Cs.FromFile);
                FileManager.DeleteFile(_env.WebRootPath, "upload/clientsmember", existclientsMembers1Cs.Img);
                existclientsMembers1Cs.Img = name;
            }
            existclientsMembers1Cs.Fullname = clientsMembers1Cs.Fullname;
            existclientsMembers1Cs.Description = clientsMembers1Cs.Description;
            _dataContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            ClientsMembers1cs clientsMembers1Cs= _dataContext.ClientsMembers1Cs.Find(id);
            if (clientsMembers1Cs == null) return NotFound();
            return View(clientsMembers1Cs);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            ClientsMembers1cs clientsMembers1Cs = _dataContext.ClientsMembers1Cs.Find(id);
            if (clientsMembers1Cs == null) return NotFound();
            if (clientsMembers1Cs.Img != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "upload/clientsmember", clientsMembers1Cs.Img);
            }

            _dataContext.ClientsMembers1Cs.Remove(clientsMembers1Cs);
            _dataContext.SaveChanges();

            // Redirect to the Index action to refresh the page
            return RedirectToAction("Index");
        }

    }
}

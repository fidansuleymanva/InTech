

namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class ClientsTitle2Controller : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public ClientsTitle2Controller(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.clientsTitle2s.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<ClientsTitle2>.Create(query, 3, page);
            return View(PaginationList);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ClientsTitle2 clientsTitle2)
        {
            if (!ModelState.IsValid) return View();
            _dataContext.Add(clientsTitle2);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ClientsTitle2 clientsTitle2 = _dataContext.clientsTitle2s.FirstOrDefault(x => x.Id == id);
            if (clientsTitle2 == null) return View();
            return View(clientsTitle2);
        }
        [HttpPost]
        public IActionResult Update(ClientsTitle2 clientsTitle2)
        {
            if (clientsTitle2 is null) return View();

           ClientsTitle2 exsistclientsTitle2 = _dataContext.clientsTitle2s.FirstOrDefault(x => x.Id == clientsTitle2.Id);
            if (exsistclientsTitle2 == null) return View();
            if (!ModelState.IsValid) return View(exsistclientsTitle2);

            exsistclientsTitle2.Title = clientsTitle2.Title;
            exsistclientsTitle2.Description = clientsTitle2.Description;
            _dataContext.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            ClientsTitle2 clientsTitle2 = _dataContext.clientsTitle2s.FirstOrDefault(x => x.Id == id);
            _dataContext.clientsTitle2s.Remove(clientsTitle2);
            _dataContext.SaveChanges();
            return View(clientsTitle2);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            ClientsTitle2 clientsTitle2 = _dataContext.clientsTitle2s.Find(id);
            if (clientsTitle2 == null) return View();

            _dataContext.clientsTitle2s.Remove(clientsTitle2);
            _dataContext.SaveChanges();
            return Ok();


        }
    }
}

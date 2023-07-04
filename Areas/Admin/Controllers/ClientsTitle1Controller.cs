using Microsoft.AspNetCore.Mvc;

namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ClientsTitle1Controller : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public ClientsTitle1Controller(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.clientsTitle1s.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<ClientsTitle1>.Create(query, 3, page);
            return View(PaginationList);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ClientsTitle1 clientsTitle1)
        {
            if (!ModelState.IsValid) return View();
            _dataContext.Add(clientsTitle1);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ClientsTitle1 clientsTitle1 = _dataContext.clientsTitle1s.FirstOrDefault(x => x.Id == id);
            if (clientsTitle1 == null) return View();
            return View(clientsTitle1);
        }
        [HttpPost]
        public IActionResult Update(ClientsTitle1 clientsTitle1)
        {
            if (clientsTitle1 is null) return View();

           ClientsTitle1 exsistclientsTitle1 = _dataContext.clientsTitle1s.FirstOrDefault(x => x.Id == clientsTitle1.Id);
            if (exsistclientsTitle1 == null) return View();
            if (!ModelState.IsValid) return View(exsistclientsTitle1);

            exsistclientsTitle1.Title = clientsTitle1.Title;
            _dataContext.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            ClientsTitle1 clientsTitle1 = _dataContext.clientsTitle1s.FirstOrDefault(x => x.Id == id);
            _dataContext.clientsTitle1s.Remove(clientsTitle1);
            _dataContext.SaveChanges();
            return View(clientsTitle1);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            ClientsTitle1 clientsTitle1 = _dataContext.clientsTitle1s.Find(id);
            if (clientsTitle1 == null) return View();

            _dataContext.clientsTitle1s.Remove(clientsTitle1);
            _dataContext.SaveChanges();
            return Ok();


        }

    }
}

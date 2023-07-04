
namespace InTech_MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SosialMediaIconController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly IWebHostEnvironment _env;

		public SosialMediaIconController(DataContext dataContext, IWebHostEnvironment env)
		{
			_dataContext = dataContext;
			_env = env;
		}
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.SosialMediaIcons.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<SosialMediaIcon>.Create(query, 3, page);
            return View(PaginationList);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SosialMediaIcon sosialMediaIcon)
        {
            if (!ModelState.IsValid) return View();
            _dataContext.Add(sosialMediaIcon);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            SosialMediaIcon sosialMediaIcon = _dataContext.SosialMediaIcons.FirstOrDefault(x => x.Id == id);
            if (sosialMediaIcon == null) return View();
            return View(sosialMediaIcon);
        }
        [HttpPost]
        public IActionResult Update(SosialMediaIcon sosialMediaIcon)
        {
            if (sosialMediaIcon is null) return View();

            SosialMediaIcon exsistsosialmediaicin = _dataContext.SosialMediaIcons.FirstOrDefault(x => x.Id == sosialMediaIcon.Id);
            if (exsistsosialmediaicin == null) return View();
            if (!ModelState.IsValid) return View(exsistsosialmediaicin);

            exsistsosialmediaicin.Icon = sosialMediaIcon.Icon;
            exsistsosialmediaicin.URL = sosialMediaIcon.URL;
            _dataContext.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            SosialMediaIcon sosialMediaIcon = _dataContext.SosialMediaIcons.FirstOrDefault(x => x.Id == id);
            _dataContext.SosialMediaIcons.Remove(sosialMediaIcon);
            _dataContext.SaveChanges();
            return View(sosialMediaIcon);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            SosialMediaIcon sosialMediaIcon = _dataContext.SosialMediaIcons.Find(id);
            if (sosialMediaIcon == null) return View();

            _dataContext.SosialMediaIcons.Remove(sosialMediaIcon);
            _dataContext.SaveChanges();
            return Ok();


        }

    }
}


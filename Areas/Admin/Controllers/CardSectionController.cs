namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CardSectionController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public CardSectionController(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _dataContext.CardSection.AsQueryable();
            //List<Slider> sliderList = _dataContext.Slider.ToList();
            var PaginationList = PaginationList<CardSection>.Create(query, 3, page);
            return View(PaginationList);


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CardSection cardSection)
        {
            if (!ModelState.IsValid)
            {
                return View(cardSection);
            }

            if (cardSection.FromFile == null || cardSection.FromFile.Length == 0)
            {
                ModelState.AddModelError("FromFile", "Please select an image file.");
                return View(cardSection);
            }

            if (cardSection.FromFile.ContentType != "image/png" && cardSection.FromFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("FromFile", "Only PNG and JPEG image files are allowed.");
                return View(cardSection);
            }

            if (cardSection.FromFile.Length > 3145728)
            {
                ModelState.AddModelError("FromFile", "The image file size must be less than 3 MB.");
                return View(cardSection);
            }

            try
            {
                cardSection.Img = FileManager.SaveFile(_env.WebRootPath, "upload/cardsections", cardSection.FromFile);
                _dataContext.CardSection.Add(cardSection);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file upload or database operations.
                ModelState.AddModelError(string.Empty, "An error occurred while creating the slider.");
                return View(cardSection);
            }



        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            CardSection cardSection = _dataContext.CardSection.Find(id);
            if (cardSection == null) return NotFound();
            return View(cardSection);
        }
        [HttpPost]
        public IActionResult Update(CardSection cardSection)
        {
            CardSection existcardsection = _dataContext.CardSection.Find(cardSection.Id);
            if (existcardsection == null) return View(existcardsection);
            if (cardSection.FromFile != null)
            {

                if (cardSection.FromFile.ContentType != "image/png" && cardSection.FromFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
                    return View();
                }
                if (cardSection.FromFile.Length > 3145728)
                {
                    ModelState.AddModelError("ImageFile", "It can be 3 Mb!");
                    return View();
                }

                string name = FileManager.SaveFile(_env.WebRootPath, "upload/cardsections", cardSection.FromFile);
                FileManager.DeleteFile(_env.WebRootPath, "upload/cardsections", existcardsection.Img);
                existcardsection.Img = name;
            }
            existcardsection.Title1 = cardSection.Title1;
            existcardsection.Description = cardSection.Description;
            _dataContext.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            CardSection cardSection = _dataContext.CardSection.Find(id);
            if (cardSection == null) return NotFound();
            return View(cardSection);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            CardSection cardSection = _dataContext.CardSection.Find(id);
            if (cardSection == null) return NotFound();
            if (cardSection.Img != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "upload/cardsections", cardSection.Img);
            }

            _dataContext.CardSection.Remove(cardSection);
            _dataContext.SaveChanges();

            // Redirect to the Index action to refresh the page
            return RedirectToAction("Index");
        }

    }  
}   






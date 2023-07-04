
namespace InTech_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(DataContext dataContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //public async Task<IActionResult> AdminCreate()
        //{
        //    AppUser appUser = new AppUser
        //    {
        //        FullName = "User User",
        //        UserName = "Admin"
        //  };
        //  var result = await _userManager.CreateAsync(appUser, "User09012002");
        //    return Ok(result);
        //}
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginViewModel)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(adminLoginViewModel.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrent!");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, adminLoginViewModel.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorret!");
                return View();
            }


            return RedirectToAction("Index", "dashboard");

        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterViewModel)
        //{
        //    if (!ModelState.IsValid) return View();
        //    AppUser member = await _userManager.FindByNameAsync(memberRegisterViewModel.UserName);
        //    if (member == null)
        //    {
        //        ModelState.AddModelError("", "Username or password is incorrent!");
        //        return View();
        //    }
        //    //member = await _userManager.FindByEmailAsync(member.Email);
        //    //if (member == null)
        //    //{
        //    //    ModelState.AddModelError("Email", "Email has taken!");
        //    //    return View();
        //    //}
        //    member = new AppUser
        //    {
        //        FullName = memberRegisterViewModel.FullName,
        //        UserName = memberRegisterViewModel.UserName,
        //        //Email = memberRegisterViewModel.Email,
        //    };
        //    var result = await _userManager.CreateAsync(member,memberRegisterViewModel.Password);
        //    if (!result.Succeeded)
        //    {
        //        foreach(var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //            return View();
        //        }

        //    }
        //    var roleresult = await _userManager.AddToRoleAsync(member, "Member");
        //    if(!roleresult.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //            return View();
        //        }
        //    }
        //    await _signInManager.SignInAsync(member, isPersistent: false);
        //    return RedirectToAction("Index","Home");

        //}
        //[HttpPost]
        //public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(memberRegisterViewModel);
        //    }

        //    var existingUser = await _userManager.FindByNameAsync(memberRegisterViewModel.UserName);
        //    if (existingUser != null)
        //    {
        //        ModelState.AddModelError("", "Username or password is incorrect!");
        //        return View(memberRegisterViewModel);
        //    }

        //    var newMember = new AppUser
        //    {
        //        FullName = memberRegisterViewModel.FullName,
        //        UserName = memberRegisterViewModel.UserName,
        //    };

        //    var result = await _userManager.CreateAsync(newMember, memberRegisterViewModel.Password);
        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //        return View(memberRegisterViewModel);
        //    }

        //    var roleResult = await _userManager.AddToRoleAsync(newMember, "Member");
        //    if (!roleResult.Succeeded)
        //    {
        //        foreach (var error in roleResult.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //        return View(memberRegisterViewModel);
        //    }

        //    await _signInManager.SignInAsync(newMember, isPersistent: false);
        //    return RedirectToAction("Index", "Home");
        //}

        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(memberRegisterViewModel);
            }

            var existingUser = await _userManager.FindByNameAsync(memberRegisterViewModel.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Username already exists!");
                return View(memberRegisterViewModel);
            }

            var newMember = new AppUser
            {
                FullName = memberRegisterViewModel.FullName,
                UserName = memberRegisterViewModel.UserName,
            };

            var result = await _userManager.CreateAsync(newMember, memberRegisterViewModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(memberRegisterViewModel);
            }

            var roleResult = await _userManager.AddToRoleAsync(newMember, "Member");
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(memberRegisterViewModel);
            }

            await _signInManager.SignInAsync(newMember, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("login", "account");

        }



    }
}

using Lesson09_Session.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lesson09_Session.Controllers
{


    public class LoginController : Controller
    {
        private readonly Lesson09DbContext _context;

        public LoginController(Lesson09DbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Lấy dữ liệu từ session -> chuyển lên giao diện và hiển thị
            var jsonCustomer = HttpContext.Session.GetString("CustomerLogin");
            if (jsonCustomer != null)
            {
                // Chuyển dữ liệu từ trong session ở dạng json sang đối tượng customer
                var CustomerModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(jsonCustomer);
                return View(CustomerModel);
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            var modelLogin = new ModelLogin();
            return View(modelLogin);
        }

        [HttpPost]
        public IActionResult Login(ModelLogin modelLogin)
        {
            // Kiểm tra trong db xem có tài khoản, mật khẩu như trên form không?
            // Nếu có thì lưu thông tin đăng nhập vào session

            var dataLogin = _context.Customers.FirstOrDefault(x => x.Username.Equals(modelLogin.Username)
                && x.Password.Equals(modelLogin.Password));

            if (dataLogin != null)
            {
                ViewBag.Login = "Đăng nhập thành công";
                // Lưu session khi đăng nhập thành công
                var customerLogin = Newtonsoft.Json.JsonConvert.SerializeObject(dataLogin);
                HttpContext.Session.SetString("CustomerLogin", customerLogin);
                return RedirectToAction("Index");
            }

            ViewBag.Login = "Sai thông tin đăng nhập";
            return View(modelLogin);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CustomerLogin");
            return RedirectToAction("Index");
        }
    }
}

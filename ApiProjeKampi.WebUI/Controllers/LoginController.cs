using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace ApiProjeKampi.WebUI.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult LoginGir()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginGir(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!model.IsHuman)
            {
                ModelState.AddModelError("", "Robot doğrulaması gerekli");
                return View(model);
            }

            string role;

            if (model.Password == "123")
                role = "User";
            else if (model.Password == "456")
                role = "Admin";
            else
            {
                ModelState.AddModelError("", "Şifre yanlış");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Name),
                new Claim(ClaimTypes.Role, role)
            };                                              

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);


            await HttpContext.SignInAsync("Cookies", principal);

            if (role == "User")
            {
                return RedirectToAction("Index", "Default");
            }
            else if (role == "Admin")
            {
                return RedirectToAction("Chef", "ChefList");
            }

            return View(model);

        }
        [HttpGet]
        public IActionResult PassvordAl()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PassvordAl(string email)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("talhadurmaz175@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Yummy Restorant – Şifre Bilgilendirme";
                mail.Body = "Sayın Kullanıcımız,\r\n\r\nYummy Restorant hesabınız için şifre talebiniz tarafımıza ulaşmıştır.\r\n\r\nSistemde kayıtlı kullanıcı bilgileriniz aşağıdaki gibidir:\r\n\r\nKullanıcı Adı: User\r\nŞifre: 123\r\n\r\nHesabınıza giriş yaptıktan sonra işlemlerinizi gerçekleştirebilirsiniz.\r\n\r\nHerhangi bir sorun yaşamanız durumunda bizimle iletişime geçebilirsiniz.\r\n\r\nİyi günler dileriz.\r\nYummy Restorant Destek Ekibi";
                mail.IsBodyHtml = false; 

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("talhadurmaz175@gmail.com", "");
                smtp.EnableSsl = true;

                smtp.Send(mail);

                Console.WriteLine("Mail başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
            return RedirectToAction("LoginGir", "Login");
        }
    }


    public class User
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }


    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "Robot olmadığını doğrula")]
        public bool IsHuman { get; set; }
    }

}

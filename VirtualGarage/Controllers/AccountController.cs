using System;
using System.Web.Mvc;
using System.Web.Security;
using BarterMarket.Logic;
using BarterMarket.Logic.DataModel;
using BarterMarket.Logic.Repository;
using BarterMarket.Models;
using System.Text;
using System.Net.Mail;
using System.Linq;
using BarterMarket.Logic.Enums;
using System.Net;
using System.IO;
using BarterMarket.HtmlHelpers;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using BarterMarket.Helpers;

namespace BarterMarket.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]        
        public ActionResult LogOn(LoginModel model, String token)
        {
            if (ModelState.IsValid || token != null)
            {
                using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
                {
                    var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();

                    if (token != null)
                    {
                        String link = String.Format("http://ulogin.ru/token.php?token={0}&host={1}", Request.Form["token"], Request.ServerVariables["SERVER_NAME"]);

                        WebRequest reqGET = WebRequest.Create(link);
                        String answer = "";

                        using (WebResponse resp = reqGET.GetResponse())
                        {
                            using (Stream stream = resp.GetResponseStream())
                            {
                                if (stream != null)
                                {
                                    using (StreamReader sr = new StreamReader(stream))
                                    {
                                        answer = sr.ReadToEnd();
                                    }
                                }
                            }
                        }

                        try
                        {
                            var json_serializer = new JavaScriptSerializer();
                            var routes_list = (IDictionary<string, object>)json_serializer.DeserializeObject(answer);

                            var network = (String)routes_list["network"];
                            var userName = String.Format("{0} {1}", routes_list["first_name"], routes_list["last_name"]);
                            var uid = (String)routes_list["uid"];

                            var user = userRepo.SingleOrDefault(us => us.Network == network && us.Uid == uid);
                            if (user != null)
                            {
                                FormsAuthentication.SetAuthCookie(user.UserEmail, false);
                                return RedirectToAction("Index", "Default");
                            }
                            else
                            {
                                return RedirectToAction("LoginFailed", "Account");      
                            }
                        }
                        catch (Exception)
                        {
                            return RedirectToAction("Index", "Default");
                        }
                    }

                    if (userRepo.CheckEmailAndPass(model.UserEmail, model.Password) == LoginResult.Success)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserEmail, model.IsRememberMe);
                        return RedirectToAction("Index", "Default");                        
                    }
                }
            }

            return RedirectToAction("LoginFailed", "Account");         
        }

        /// <summary>
        /// Выход из системы
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Default");
        }

        /// <summary>
        /// Регистрация пользователя (get)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Регистрация пользователя (post)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[CaptchaValidator]
        public ActionResult Register(RegisterModel model, String token)
        {
            if (ModelState.IsValid || token != null)
            {     
                using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
                {                
                    var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();

                    // Регистрация через социальную сеть
                    if (token != null)
                    {
                        String link = String.Format("http://ulogin.ru/token.php?token={0}&host={1}", Request.Form["token"], Request.ServerVariables["SERVER_NAME"]);

                        WebRequest reqGET = WebRequest.Create(link);
                        String answer = "";

                        using (WebResponse resp = reqGET.GetResponse())
                        {
                            using (Stream stream = resp.GetResponseStream())
                            {
                                if (stream != null) 
                                {
                                    using (StreamReader sr = new StreamReader(stream))
                                    {
                                        answer = sr.ReadToEnd();
                                    }
                                }                                
                            }
                        }

                        try
                        {
                            var json_serializer = new JavaScriptSerializer();
                            var routes_list = (IDictionary<string, object>)json_serializer.DeserializeObject(answer);

                            var network = (String)routes_list["network"];
                            var userName = String.Format("{0} {1}", routes_list["first_name"], routes_list["last_name"]);
                            var uid = (String)routes_list["uid"];

                            var user = userRepo.SingleOrDefault(us => us.Network == network && us.Uid == uid);
                            if (user != null)
                            {
                                if (user.UserEmail != null)
                                {
                                    // пользователь уже зарегистрирован, переправить его а страницу логина
                                }
                                else
                                {
                                    // пользователю необходимо дозаполнить необходимые поля перед входом в систему
                                }
                            }


                            userRepo.Add(new User()
                            {                                
                                UserRoleID = _roleIDOfUser,
                                UserPiarsCount = 0,                               
                                UserRegisrationDate = DateTime.Now,
                                Uid = uid,
                                Network = network
                            });

                            String error;
                            if ((error = unitOfWork.SaveAndGetError()) != null)
                            {
                                ModelState.AddModelError("", error);
                                return View(model);  
                            }
                            else
                            {
                                var registerNetworkModel = new RegisterNetworkModel() 
                                {
                                    Network = network,
                                    Uid = uid,
                                    UserName = userName
                                };

                                //MachineKey.Protect(Encoding.UTF8.GetBytes(cookieValue), "an authentication token").FromBytesToBase64()

                                //MachineKeyProtection option = new MachineKeyProtection();

                                //MachineKey.Encode(Encoding.UTF8.GetBytes(network), MachineKeyProtection.All);

                                Response.Cookies["Network"].Value = MachineKey.Encode(Encoding.UTF8.GetBytes(network), MachineKeyProtection.All);
                                Response.Cookies["Uid"].Value = MachineKey.Encode(Encoding.UTF8.GetBytes(uid), MachineKeyProtection.All);
                                registerNetworkModel.IsFirstLoad = true; // Если первая загрузка страницы, не показывывть ошибки проверки значений
                                return View("RegisterNetwork", registerNetworkModel);
                            }              
                        }
                        catch (Exception)
                        {
                            return RedirectToAction("Index", "Default");
                        }
                    }

                    // Проверить, существует ли пользователь с таким e-mail'ом
                    if (userRepo.IsEmailExist(model.UserEmail))
                    {
                        ModelState.AddModelError("", "Пользователь с таким E-mail'ом уже существует. Выберите другой E-mail");
                    }                        
                    else
                    {
                        userRepo.Add(new User()
                        {
                            UserNick = model.UserName,
                            UserEmail = model.UserEmail,
                            UserPassword = model.Password,
                            UserRoleID = _roleIDOfUser,
                            UserPiarsCount = 0,
                            UserCompanyName = model.CompanyName,
                            UserCompanyDetails = model.CompanyDetails,
                            UserRegisrationDate = DateTime.Now
                        });

                        String error;
                        if ((error = unitOfWork.SaveAndGetError()) != null)
                        {
                            ModelState.AddModelError("", error);
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(model.UserEmail, false);
                            return RedirectToAction("Index", "Default");
                        }                           
                    }                        
                }                                   
            }

            model.Password = String.Empty;
            model.ConfirmPassword = String.Empty;                
            return View(model);             
        }

        /// <summary>
        /// Регистрация пользователя через соцсеть (post)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[CaptchaValidator]
        public ActionResult RegisterNetwork(RegisterNetworkModel model)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
                {
                    var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();

                    // Проверить, существует ли пользователь с таким e-mail'ом
                    if (userRepo.IsEmailExist(model.UserEmail))
                    {
                        ModelState.AddModelError("", "Пользователь с таким E-mail'ом уже существует. Выберите другой E-mail");
                    }
                    else
                    {
                        var network = Encoding.UTF8.GetString(MachineKey.Decode(Request.Cookies["Network"].Value, MachineKeyProtection.All));
                        var uid = Encoding.UTF8.GetString(MachineKey.Decode(Request.Cookies["Uid"].Value, MachineKeyProtection.All));

                        var user = userRepo.SingleOrDefault(us => us.Network == network && us.Uid == uid);

                        if (user != null)
	                    {
                            user.UserCompanyDetails = model.CompanyDetails;
                            user.UserCompanyName = model.CompanyName;
                            user.UserEmail = model.UserEmail;
                            user.UserNick = model.UserName;                            
	                    }

                        String error;
                        if ((error = unitOfWork.SaveAndGetError()) != null)
                        {
                            ModelState.AddModelError("", error);
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(model.UserEmail, false);
                            return RedirectToAction("Index", "Default");
                        }
                    }
                }
            }

            return View(model);
        }

        /// <summary>
        /// Страница восстановления пароля (get)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Страница восстановления пароля (post)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
                {
                    var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();

                    User user = userRepo.FirstOrDefault(it => it.UserEmail == model.Email);

                    if (user != null)
                    {
                        var message = new StringBuilder();
                        message.AppendFormat("Здравствуйте, {0}!\n\n", user.UserNick);

                        message.AppendFormat("Это письмо было сгенерированно по Вашему запросу на восстановление пароля ");
                        message.AppendFormat("от учетной записи на сайте pr-money.com ");
                        message.AppendFormat("Если Вы не делали этот запрос, проигнорируйте письмо.\n");
                        message.AppendFormat("E-mail: {0}\n", user.UserEmail);
                        message.AppendFormat("Пароль: {0}\n\n", user.UserPassword);

                        message.AppendFormat("С уважением, Служба поддержки сайта pr-money.com");

                        SmtpClient smtpClient = new SmtpClient();

                        try
                        {
                            smtpClient.Send(new MailMessage(
                                "Служба поддержки pr-money.com <info@pr-money.com>",   // From
                                user.UserEmail,   // To
                                "Восстановление пароля",    // Subject
                                message.ToString()  // Body
                            ));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError("", "Ошибка при отправке письма на указанный E-mail");
                            return View(model);
                        }

                        return RedirectToAction("", "");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователя с таким E-mail'ом не существует");
                    }
                }
            }

            return View(model);
        }
                
        /// <summary>
        /// Страница при неудачном входе в систему(get)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LoginFailed()
        {
            return View();
        }

        /// <summary>
        /// Страница входа
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// Страница при неудачном входе в систему(post)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginFailed(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
                {
                    var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();

                    LoginResult loginResult = userRepo.CheckEmailAndPass(model.UserEmail, model.Password);

                    if (loginResult == LoginResult.UserNotExist)
                    {
                        ModelState.AddModelError("", "Пользователя с таким именем не существует");
                    }
                    else if (loginResult == LoginResult.UncorrectPass)
                    {
                        ModelState.AddModelError("", "Неправильный пароль");
                    }
                    else if (loginResult == LoginResult.Success)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserEmail, model.IsRememberMe);
                        return RedirectToAction("Index", "Default");
                    }
                }
            }

            model.Password = String.Empty;
            return View(model);   
        }

        private const Int32 _roleIDOfUser = 1;
    }
}

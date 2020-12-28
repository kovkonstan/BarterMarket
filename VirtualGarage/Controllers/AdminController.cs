using BarterMarket.Helpers;
using BarterMarket.Logic;
using BarterMarket.Logic.DataModel;
using BarterMarket.Logic.Repository;
using BarterMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarterMarket.Controllers
{
    [Authorize(Users = "admin@pr-money.com")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Пользователи
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Users()
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();
                var model = userRepo.Select(us => new UserModel()
                        {
                            UserName = us.UserNick,
                            UserEmail = us.UserEmail,
                            CompanyName = us.UserCompanyName,
                            CompanyDetails = us.UserCompanyDetails,
                            RegistrationDate = us.UserRegisrationDate,
                            UserPiarsCount = us.UserPiarsCount,
                            UserID = us.UserID,
                            Password = us.UserPassword
                        }).ToList();


                    return View(model);               
            }
        }

        /// <summary>
        /// Предложения
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public ActionResult Offers()
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                try
                {
                    var offerModels = DataHelper.GetAllOffers(unitOfWork);

                    var model = new ListUserOffersModel()
                    {
                        OfferModels = offerModels
                    };

                    return View(model);
                }
                catch (Exception)
                {
                    return View("Message", new MessageWithRedirectModel("Невозможно отобразить страницу", "", "", null));
                }
            }
        }

        /// <summary>
        /// Запросы на регистрацию
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OfferRequests()
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var requestRepo = unitOfWork.CreateRepo<OfferRequest>();
                var model = requestRepo.Select(r => new OfferRequestModel()
                {
                    AdvertisingTarget = r.AdvertisingTarget,
                    CompanyAddress = r.CompanyAddress,
                    CompanyDetails = r.CompanyDetails,
                    CompanyName = r.CompanyName,
                    CompanyRegions = r.CompanyRegions,
                    CompanyRegistrationRegion = r.CompanyRegistrationRegion,
                    CompanySite = r.CompanySite,
                    OfferRequestID = r.OfferRequestID,
                    SourceAboutUs = r.SourceAboutUs,
                    TelephoneNumber = r.TelephoneNumber,
                    UserName = r.UserName
                }).ToList();
                
                return View(model);
            }
        }

        /// <summary>
        /// Добавить пиары
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddPiars(Int32 userID)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var user = unitOfWork.CreateInterfacedRepo<IUserRepo>().SingleOrDefault(us => us.UserID == userID);

                if (user != null)
                {
                    var model = new AddPiarsModel()
                    {
                        UserID = user.UserID,
                        UserName = user.UserNick,
                        UserPiarsCounts = user.UserPiarsCount
                    }; 
                    
                    return View(model);
                }

                
               
            }
            
            return View("Message",
                            new MessageWithRedirectModel("Невозможно отобразить страницу",
                                "Users",
                                "Admin",
                                null));
        }


        /// <summary>
        /// Добавить пиары (post)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddPiars(AddPiarsModel model)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var user = unitOfWork.CreateInterfacedRepo<IUserRepo>().SingleOrDefault(us => us.UserID == model.UserID);

                try
                {
                    user.UserPiarsCount += model.PiarsCountsToIncrement;
                }
                catch (Exception)
                {
                    return View("Message",
                            new MessageWithRedirectModel("При выполнениии операции произошла ошибка",
                                "Users",
                                "Admin",
                                null));
                }

                String error = unitOfWork.SaveAndGetError();

                if (error == null)
                {
                    return RedirectToAction("Users", "Admin");
                }
                else
                    
                return View("Message",
                            new MessageWithRedirectModel("При выполнениии операции произошла ошибка",
                                "Users",
                                "Admin",
                                null));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarterMarket.Logic.DataModel;
using KK.Data;
using BarterMarket.Logic;
using BarterMarket.Models;
using BarterMarket.Logic.Repository;
using System.Web.Security;
using BarterMarket.Helpers;
using System.Web.Routing;
using BarterMarket.Logic.Enums;

namespace BarterMarket.Controllers
{
    public class DefaultController : Controller
    {

        /// <summary>
        /// Главная страница сайта
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            var model = new IndexModel();
			using(var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
			{				  
				if (User.Identity.IsAuthenticated)
				{				
                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());		 
				}

				return View(model);
			}
        }

        /// <summary>
        /// Вопросы
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Questions()
        {
            var model = new QuestionsModel();
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                if (User.Identity.IsAuthenticated)
                {
                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                }

                return View(model);
            }
        }

        /// <summary>
        /// Правила сайта
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Rules()
        {
            var model = new RulesModel();
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                if (User.Identity.IsAuthenticated)
                {
                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                }
                                
                return View(model);
            }
        }

        /// <summary>
        /// Новости
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult News()
        {
            var model = new NewsModel();
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                if (User.Identity.IsAuthenticated)
                {
                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                }

                return View(model);
            }
        }

        /// <summary>
        /// О нас
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult About()
        {
            var model = new AboutModel();
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                if (User.Identity.IsAuthenticated)
                {
                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                }
                                
                return View(model);
            }
        }
               
        /// <summary>
        /// Настройки
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Settings()
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();

                User user = userRepo.GetByEmail(GetAuthenticatedEmail());

                if (user != null)
                {
                    var model = new SettingsModel()
                    {
                        UserModel = new UserModel()
                        {
                            UserName = user.UserNick,
                            UserEmail = user.UserEmail,
                            CompanyName = user.UserCompanyName,
                            CompanyDetails = user.UserCompanyDetails,
                            RegistrationDate = user.UserRegisrationDate,
                            UserPiarsCount = user.UserPiarsCount
                        }
                    };

                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                    return View(model);
                }
				else
				{
					return View("Message",
									new MessageWithRedirectModel("Невозможно отобразить страницу",
										"",
										"",
										null));
				}
            }					   			
        }

        /// <summary>
        /// Информация о пользователе
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserInfo(Int32 userID)
        {	
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                User user = unitOfWork.CreateInterfacedRepo<IUserRepo>()
                                .SingleOrDefault(it => it.UserID == userID);
                if (user != null)
                {
                    UserInfoModel model = new UserInfoModel()
                    {
                        UserID = user.UserID,
                        UserName = user.UserNick,						
                    };

                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());			                    
                    return View(model);
                }
            }

            return View("Message",
							new MessageWithRedirectModel("Невозможно отобразить страницу",
								"",
								"",
								null));	
		}

        /// <summary>
        /// Информация о предложении
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OfferInfo(Int32 offerID)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var offer = unitOfWork.CreateRepo<Offer>().SingleOrDefault(of => of.OfferID == offerID);

                var offerPackets = new List<PacketModel>();

                foreach (var it in offer.Packets)
                {
                    var packet = new PacketModel()
                    {
                        OfferID = it.OfferID,
                        PacketCost = it.PacketCost,
                        PacketDate = it.PacketDate,
                        PacketID = it.PacketID,
                        PacketName = it.PacketName,
                        PacketsCount = it.PacketsCount,
                        PacketStatusID = it.PacketStatusID,
                        PacketStatusName = it.PacketStatus.PacketStatusName
                    };

                    packet.AvailAblePacketsCount = it.PacketsCount - unitOfWork.CreateRepo<Certificate>().Count(cer => cer.PacketID == it.PacketID);

                    offerPackets.Add(packet);                                        
                }

                if (offer != null)
                {
                    OfferInfoModel model = new OfferInfoModel()
                    {
                        CategoryName = offer.OfferCategory.OfferCategoryName,
                        UserNick = offer.User.UserNick,
                        OfferModel = new OfferModel()
                        {
                            OfferCompanyAddress = offer.OfferCompanyAddress,
                            OfferDate = offer.OfferDate,
                            OfferDetails = offer.OfferDetails,
                            OfferFacebookLink = offer.OfferFacebookLink,
                            OfferID = offer.OfferID,
                            OfferInstagramLink = offer.OfferInstagramLink,
                            OfferManagerEmail = offer.OfferManagerEmail,
                            OfferManagerName = offer.OfferManagerName,
                            OfferManagerPhone = offer.OfferManagerPhone,
                            OfferName = offer.OfferName,
                            OfferOdnoklassnikiLink = offer.OfferOdnoklassnikiLink,
                            OfferTelegramLink = offer.OfferTelegramLink,
                            OfferTwitterLink = offer.OfferTwitterLink,
                            OfferViberLink = offer.OfferViberLink,
                            OfferVKLink = offer.OfferVKLink,
                            OfferWhatsappLink = offer.OfferWhatsappLink,
                            UserID = offer.UserID,
                            OfferCategoryName = offer.OfferCategory.OfferCategoryName,
                            OfferStatusName = offer.OfferStatus.OfferStatusName,
                            ImageType = offer.ImageType,
                            Packets = offerPackets,
                            OfferPacketsCount = offerPackets.Where(it => it.PacketStatusID == 2).Count()
                            //Packets = offer.Packets.Select(it => new PacketModel() 
                            //                                           {
                            //                                               OfferID = it.OfferID,
                            //                                               PacketCost = it.PacketCost,
                            //                                               PacketDate = it.PacketDate,
                            //                                               PacketID = it.PacketID,
                            //                                               PacketName = it.PacketName,
                            //                                               PacketsCount = it.PacketsCount,
                            //                                               AvailAblePacketsCount = it.PacketsCount - offerCertificates.Count(cer => cer.PacketID == it.PacketID)
                            //                                           })
                        }
                    };

                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                    return View(model);
                }
            }

            return View("Message",
                            new MessageWithRedirectModel("Невозможно отобразить страницу",
                                "",
                                "",
                                null));
        }

        /// <summary>
        /// Изменить e-mail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult ChangeEmail()
        {
			using(var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
			{
                var model = new ChangeEmailModel();

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
				return View(model);
			}
        }

        /// <summary>
        /// Изменить e-mail (post)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult ChangeEmail(ChangeEmailModel model)
        {
			using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
			{
				if (ModelState.IsValid)
				{                
                    var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();

                    // Проверить, существует ли пользователь с таким e-mail'ом
                    if (userRepo.IsEmailExist(model.NewEmail))
                    {
                        ModelState.AddModelError("", "Пользователь с таким E-mail'ом уже существует. Выберите другой E-mail");
                    }
                    else
                    {
                        userRepo.GetByEmail(GetAuthenticatedEmail()).UserEmail = model.NewEmail;

                        String error = unitOfWork.SaveAndGetError();

                        if (error == null)
                        {
                            return RedirectToAction("Settings", "Default");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Проверьте еще раз Ваш E-mail");
                        }
                    }
                }

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
				return View(model);
            }


        }

        /// <summary>
        /// Изменрить пароль
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
			using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
			{
                var model = new ChangePasswordModel();

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
				return View(model);
			}
        }
         
        /// <summary>
        /// Изменрить пароль (post)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
			using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
			{
				if (ModelState.IsValid)
				{                
                    var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();

                    if (userRepo.GetByEmail(GetAuthenticatedEmail()).UserPassword == model.OldPassword)
                    {
                        userRepo.GetByEmail(GetAuthenticatedEmail())
                            .UserPassword = model.NewPassword;

                        String error = unitOfWork.SaveAndGetError();
                        if (error != null)
                        {
                            ModelState.AddModelError("", "Попробуйте еще раз. При повторном " +
                                "появлении ошибки обратитесь к администратору");
                        }
                        else
                        {
                            return RedirectToAction("Settings", "Default");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильно введен старый пароль");
                    }
                }

				model.NewPassword = String.Empty;
				model.ConfirmPassword = String.Empty;

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
				return View(model);
            }
        }
        

        /// <summary>
        /// Изменить e-mail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult ChangeCompanyDetails()
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();

                User user = userRepo.GetByEmail(GetAuthenticatedEmail());

                if (user != null)
                {
                    var model = new SettingsModel()
                    {
                        UserModel = new UserModel()
                        {
                            UserName = user.UserNick,
                            UserEmail = user.UserEmail,
                                
                            CompanyName = user.UserCompanyName,
                            CompanyDetails = user.UserCompanyDetails,
                            RegistrationDate = user.UserRegisrationDate,
                            UserPiarsCount = user.UserPiarsCount
                        }
                    };

                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                    return View(model);
                }
                else
                {
                    return View("Message",
                                    new MessageWithRedirectModel("Невозможно отобразить страницу",
                                        "",
                                        "",
                                        null));
                }
            }
        }

        /// <summary>
        /// Изменить e-mail (post)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult ChangeCompanyDetails(SettingsModel model)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {

                var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();

                userRepo.GetByEmail(GetAuthenticatedEmail()).UserCompanyName = model.UserModel.CompanyName;
                userRepo.GetByEmail(GetAuthenticatedEmail()).UserCompanyDetails = model.UserModel.CompanyDetails;

                String error = unitOfWork.SaveAndGetError();

                if (error == null)
                {
                    return RedirectToAction("Settings", "Default");
                }
                else
                {
                    ModelState.AddModelError("", "Проверьте введенные данные");
                }

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }


        /// <summary>
        /// Предложения пользователя
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserOffers(Int32? userID)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var me = unitOfWork.CreateInterfacedRepo<IUserRepo>().GetByEmail(GetAuthenticatedEmail());
                Int32 usID = (userID ?? me.UserID);

                Boolean isMyOffers = (usID == me.UserID);

                try
                {
                    var offerModels = DataHelper.GetUserOffers(unitOfWork, usID);
                    var model = new ListUserOffersModel()
                    {
                        OfferModels = offerModels,
                        IsMyOffers = isMyOffers
                    };

                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                    return View(model);
                }
                catch (Exception)
                {
                    return View("Message", new MessageWithRedirectModel("Невозможно отобразить страницу", "", "", null));
                }
            }
        }

        /// <summary>
        /// Личный кабинет пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Cabinet()
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var me = unitOfWork.CreateInterfacedRepo<IUserRepo>().GetByEmail(GetAuthenticatedEmail());
                
                try
                {
                    var offerModels = DataHelper.GetUserOffers(unitOfWork, me.UserID);
                    var model = new CabinetModel()
                    {
                        OfferModels = offerModels
                    };

                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                    return View(model);
                }
                catch (Exception)
                {
                    return View("Message", new MessageWithRedirectModel("Невозможно отобразить страницу", "", "", null));
                }
            }
        }

        ///// <summary>
        ///// Все предложения
        ///// </summary>
        ///// <param name="userID"></param>
        ///// <param name="page"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult Offers()
        //{
        //    using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
        //    {
        //        //var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();
        //        //var me = userRepo.GetByLogin(GetAuthenticatedEmail());
        //        //Int32 usID = (userID ?? me.UserID);

        //        //Boolean isMyOffers = (usID == me.UserID);

        //        var offerRepo = unitOfWork.CreateRepo<Offer>();

        //        try
        //        {
        //            // Получить все предложения пользователя
        //            //CarMapper mapper = new CarMapper();

        //            List<OfferModel> offerModels = (from offer
        //                                                in offerRepo.All()
        //                                            select new OfferModel()
        //                                            {
        //                                                ImageType = offer.ImageType,
        //                                                OfferCompanyAddress = offer.OfferCompanyAddress,
        //                                                OfferDate = offer.OfferDate,
        //                                                OfferDetails = offer.OfferDetails,
        //                                                OfferFacebookLink = offer.OfferFacebookLink,
        //                                                OfferID = offer.OfferID,
        //                                                OfferInstagramLink = offer.OfferInstagramLink,
        //                                                OfferManagerEmail = offer.OfferManagerEmail,
        //                                                OfferManagerName = offer.OfferManagerName,
        //                                                OfferManagerPhone = offer.OfferManagerPhone,
        //                                                OfferName = offer.OfferName,
        //                                                OfferOdnoklassnikiLink = offer.OfferOdnoklassnikiLink,
        //                                                OfferPhoto = offer.OfferPhoto,
        //                                                OfferTelegramLink = offer.OfferTelegramLink,
        //                                                OfferTwitterLink = offer.OfferTwitterLink,
        //                                                OfferViberLink = offer.OfferViberLink,
        //                                                OfferVKLink = offer.OfferVKLink,
        //                                                OfferWhatsappLink = offer.OfferWhatsappLink,
        //                                                UserID = offer.UserID

        //                                            }).ToList();
        //            var model = new ListUserOffersModel()
        //            {
        //                OfferModels = offerModels,
        //                IsMyOffers = false
        //            };

        //            model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());

        //            return View(model);
        //        }
        //        catch (Exception)
        //        {
        //            return View("Message", new MessageWithRedirectModel("Невозможно отобразить страницу", "", "", null));
        //        }
        //    }
        //}

        /// <summary>
        /// Все предложения
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Offers(String searchRequest)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                //var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();
                //var me = userRepo.GetByLogin(GetAuthenticatedEmail());
                //Int32 usID = (userID ?? me.UserID);

                //Boolean isMyOffers = (usID == me.UserID);
                               
                try
                {
                    var offerModels = (searchRequest != null ?
                                        DataHelper.GetAllPublicOffers(unitOfWork).Where(of => of.OfferName.ToLower().Contains(searchRequest.ToLower()) || of.OfferDetails.ToLower().Contains(searchRequest.ToLower())).ToList() :
                                        DataHelper.GetAllPublicOffers(unitOfWork));

                    var model = new ListUserOffersModel()
                    {
                        OfferModels = offerModels,
                        IsMyOffers = false
                    };

                    if (model.OfferModels.Count == 0 && searchRequest != null)
                    {
                        ModelState.AddModelError("", "К сожалению, по Вашему запросу не найдено результатов");
                    }                    

                    model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());

                    return View(model);
                }
                catch (Exception)
                {                    
                    return View("Message", new MessageWithRedirectModel("Невозможно отобразить страницу", "", "", null));
                }
            }
        }

        /// <summary>
        /// Добавить предложение
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult AddOffer()
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                AddOfferModel model = DataHelper.GetAddOfferModel(unitOfWork, GetAuthenticatedEmail());

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }
        
        /// <summary>
        /// Добавить предложение (post)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult AddOffer(AddOfferModel model, HttpPostedFileBase photo)
        {
            Offer offer = new Offer();

            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                if (photo != null && photo.ContentLength > 1024000)
                {
                    ModelState.AddModelError("", "Размер изображения должен быть меньше 1 мегабайта");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var offerRepo = unitOfWork.CreateRepo<Offer>();

                        try
                        {
                            offer.OfferName = model.OfferModel.OfferName;
                            offer.OfferCategoryID = model.OfferModel.OfferCategoryID;
                            offer.OfferCompanyAddress = model.OfferModel.OfferCompanyAddress;
                            offer.OfferManagerName = model.OfferModel.OfferManagerName;
                            offer.OfferManagerPhone = model.OfferModel.OfferManagerPhone;
                            offer.OfferManagerEmail = model.OfferModel.OfferManagerEmail;
                            offer.OfferDetails = model.OfferModel.OfferDetails;
                            offer.OfferFacebookLink = model.OfferModel.OfferFacebookLink;
                            offer.OfferInstagramLink = model.OfferModel.OfferInstagramLink;
                            offer.OfferOdnoklassnikiLink = model.OfferModel.OfferOdnoklassnikiLink;
                            offer.OfferTelegramLink = model.OfferModel.OfferTelegramLink;
                            offer.OfferTwitterLink = model.OfferModel.OfferTwitterLink;
                            offer.OfferViberLink = model.OfferModel.OfferViberLink;
                            offer.OfferVKLink = model.OfferModel.OfferVKLink;
                            offer.OfferWhatsappLink = model.OfferModel.OfferWhatsappLink;
                            offer.UserID = (unitOfWork.CreateInterfacedRepo<IUserRepo>()).GetByEmail(GetAuthenticatedEmail()).UserID;
                            offer.OfferDate = DateTime.Now;
                            offer.OfferStatusID = _offerInitialStatusID;
                            
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError("", "Введены некорректные данные. Проверьте введенные данные еще раз");
                            model = DataHelper.GetAddOfferModel(model, unitOfWork, GetAuthenticatedEmail());
                            
                            return View(model);
                        }

                        if (photo != null)
                        {
                            offer.OfferPhoto = new Byte[photo.ContentLength];
                            photo.InputStream.Read(offer.OfferPhoto, 0, photo.ContentLength);
                            offer.ImageType = photo.ContentType;
                        }

                        offerRepo.Add(offer);

                        String error;
                        if ((error = unitOfWork.SaveAndGetError()) != null)
                        {
                            ModelState.AddModelError("", error);
                        }
                        else
                        {
                            return View("Message",
                                        new MessageWithRedirectModel("Предложение добавлено",
                                                                    "UserOffers",
                                                                    "Default",
                                                                    null));
                        }
                    }
                }

                model = DataHelper.GetAddOfferModel(model, unitOfWork, GetAuthenticatedEmail());
                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }

        /// <summary>
        /// Заявка на предложение
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public ActionResult OfferRequest()
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                OfferRequestModel model = new OfferRequestModel();

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }

        /// <summary>
        /// Заявка на предложение (post)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost]        
        public ActionResult OfferRequest(OfferRequestModel model)
        {
            OfferRequest request = new OfferRequest();

            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {

                if (ModelState.IsValid)
                {
                    var offerRequestRepo = unitOfWork.CreateRepo<OfferRequest>();

                    try
                    {
                        request.AdvertisingTarget = model.AdvertisingTarget;
                        request.CompanyAddress = model.CompanyAddress;
                        request.CompanyDetails = model.CompanyDetails;
                        request.CompanyName = model.CompanyName;
                        request.CompanyRegions = model.CompanyRegions;
                        request.CompanyRegistrationRegion = model.CompanyRegistrationRegion;
                        request.CompanySite = model.CompanySite;
                        request.SourceAboutUs = model.SourceAboutUs;
                        request.TelephoneNumber = model.TelephoneNumber;
                        request.UserName = model.UserName;                        
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Введены некорректные данные. Проверьте введенные данные еще раз");
                        model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                        return View(model);
                    }

                    offerRequestRepo.Add(request);
                    String error;
                    if ((error = unitOfWork.SaveAndGetError()) != null)
                    {
                        ModelState.AddModelError("", error);
                    }
                    else
                    {
                        return View("Message",
                                    new MessageWithRedirectModel("Ваша заявка оформлена",
                                                                "Index",
                                                                "Default",
                                                                null));
                    }
                }
                                                
                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }

        /// <summary>
        /// Задать вопрос (post)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Contacts(ContactsModel model)
        {
            UserQuestion question = new UserQuestion();

            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                if (ModelState.IsValid)
                {
                    var userQuestionRepo = unitOfWork.CreateRepo<UserQuestion>();

                    try
                    {
                        question.UserEmail = model.UserQuestionModel.UserEmail;
                        question.UserMessage = model.UserQuestionModel.UserMessage;
                        question.UserName = model.UserQuestionModel.UserName;
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Введены некорректные данные. Проверьте введенные данные еще раз");
                        model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                        return View(model);
                    }

                    userQuestionRepo.Add(question);
                    String error;
                    if ((error = unitOfWork.SaveAndGetError()) != null)
                    {
                        ModelState.AddModelError("", error);
                    }
                    else
                    {
                        return View("Message",
                                    new MessageWithRedirectModel("Мы уже рассматриваем Ваш вопрос!",
                                                                "Index",
                                                                "Default",
                                                                null));
                    }
                }

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }

        /// <summary>
        /// Редактировать предложение
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult EditOffer(Int32 offerID)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                AddOfferModel model = DataHelper.GetEditOfferModel(unitOfWork, GetAuthenticatedEmail(), offerID);

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }

        /// <summary>
        /// Редактировать предложение (post)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult EditOffer(EditOfferModel model, HttpPostedFileBase photo)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                if (photo != null && photo.ContentLength > 1024000)
                {
                    ModelState.AddModelError("", "Размер изображения должен быть меньше 1 мегабайта");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var offerRepo = unitOfWork.CreateRepo<Offer>();
                        var offer = offerRepo.SingleOrDefault(it => it.OfferID == model.OfferModel.OfferID);

                        try
                        {
                            offer.OfferName = model.OfferModel.OfferName;
                            offer.OfferCategoryID = model.OfferModel.OfferCategoryID;
                            offer.OfferCompanyAddress = model.OfferModel.OfferCompanyAddress;
                            offer.OfferManagerName = model.OfferModel.OfferManagerName;
                            offer.OfferManagerPhone = model.OfferModel.OfferManagerPhone;
                            offer.OfferManagerEmail = model.OfferModel.OfferManagerEmail;
                            offer.OfferDetails = model.OfferModel.OfferDetails;
                            offer.OfferFacebookLink = model.OfferModel.OfferFacebookLink;
                            offer.OfferInstagramLink = model.OfferModel.OfferInstagramLink;
                            offer.OfferOdnoklassnikiLink = model.OfferModel.OfferOdnoklassnikiLink;
                            offer.OfferTelegramLink = model.OfferModel.OfferTelegramLink;
                            offer.OfferTwitterLink = model.OfferModel.OfferTwitterLink;
                            offer.OfferViberLink = model.OfferModel.OfferViberLink;
                            offer.OfferVKLink = model.OfferModel.OfferVKLink;
                            offer.OfferWhatsappLink = model.OfferModel.OfferWhatsappLink;
                            offer.OfferStatusID = (model.OfferModel.OfferStatusID != 0 ? model.OfferModel.OfferStatusID : _offerInitialStatusID);
                            
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError("", "Введены некорректные данные. Проверьте введенные данные еще раз");
                            model = DataHelper.GetEditOfferModel(unitOfWork, GetAuthenticatedEmail(), model.OfferModel.OfferID);
                            model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                            return View(model);
                        }

                        if (photo != null)
                        {
                            offer.OfferPhoto = new Byte[photo.ContentLength];
                            photo.InputStream.Read(offer.OfferPhoto, 0, photo.ContentLength);
                            offer.ImageType = photo.ContentType;
                        }

                        String error;
                        if ((error = unitOfWork.SaveAndGetError()) != null)
                        {
                            ModelState.AddModelError("", error);
                        }
                        else                        
                        {
                            return View("Message",
                                        new MessageWithRedirectModel("Изменения сохранены",
                                                                    "UserOffers",
                                                                    "Default",
                                                                    null));
                        }
                    }
                }

                model.OfferModel.OfferCategories = DataHelper.GetOfferCategoriesListItems(unitOfWork);
                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }

        /// <summary>
        /// Загрузка фото предложения (get)
        /// </summary>
        /// <param name="carID"></param>
        /// <returns></returns>
        [HttpGet]
        public FileContentResult GetImage(Int32 offerID)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                Offer offer = (unitOfWork.CreateRepo<Offer>()).FirstOrDefault(it => it.OfferID == offerID);

                if (offer.OfferPhoto != null)
                {
                    return File(offer.OfferPhoto, offer.ImageType);
                }
            }
            return null;
        }
        
        /// <summary>
        /// Удалить автомобиль
        /// </summary>
        /// <param name="carID"></param>
        /// <param name="pageStr"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult DeleteOffer(Int32 offerID)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var offerRepo = unitOfWork.CreateRepo<Offer>();
                Offer offer = offerRepo.SingleOrDefault(it => it.OfferID == offerID);

                if (offer == null)
                {
                    return PartialView("MessageWithRedirect", new MessageWithRedirectModel("Предложение не найдено", "", "", null));

                }

                offerRepo.Delete(offer);  

                String error;
                if ((error = unitOfWork.SaveAndGetError()) != null)
                {
                    ModelState.AddModelError("", "Не удалось удалить предложение");
                }
                else
                {
                    return PartialView("MessageWithRedirect",
                                new MessageWithRedirectModel("Предложение успешно удалено",
                                                                "UserOffers",
                                                                "Default", 
                                                                null));
                } 
            }
            return PartialView("MessageWithRedirect",
                                new MessageWithRedirectModel("Не удалось удалить предложение",
                                                                "UserOffers",
                                                                "Default",
                                                                null));
        }
        
        /// <summary>
        /// Возвращает имя пользователя из куков
        /// </summary>
        /// <returns></returns>
        private String GetAuthenticatedEmail()
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                return User.Identity.Name;
            }

            return null;
        }
        
        /// <summary>
        /// Добавить пакет
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult AddPacket(Int32 offerID)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                AddPacketModel model = DataHelper.GetAddPacketModel(unitOfWork, GetAuthenticatedEmail(), offerID);

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());                
                return View(model);
            }
        }

        /// <summary>
        /// Добавить пакет (post)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult AddPacket(AddPacketModel model)
        {
            Packet packet = new Packet();

            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {               
                if (ModelState.IsValid)
                {
                    var offerRepo = unitOfWork.CreateRepo<Offer>();
                    

                    try
                    {
                        offerRepo.SingleOrDefault(it => it.OfferID == model.PacketModel.OfferID).Packets
                                                        .Add(new Packet()
                                                        {
                                                            PacketCost = model.PacketModel.PacketCost,
                                                            PacketDate = DateTime.Now,
                                                            PacketName = model.PacketModel.PacketName,
                                                            PacketsCount = model.PacketModel.PacketsCount,
                                                            PacketStatusID = _packetInitialStatusID
                                                        });                        

                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Введены некорректные данные. Проверьте введенные данные еще раз");
                        
                        model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                        return View(model);
                    }
                    
                    String error;
                    if ((error = unitOfWork.SaveAndGetError()) != null)
                    {
                        ModelState.AddModelError("", error);
                    }
                    else
                    {
                        return View("Message",
                                    new MessageWithRedirectModel("Пакет добавлен",
                                                                "OfferInfo",
                                                                "Default",
                                                                new RouteValueDictionary() { {"offerID", model.PacketModel.OfferID } }));
                    }
                    
                }

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }

        /// <summary>
        /// Редактировать пакет
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult EditPacket(Int32 packetID)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                EditPacketModel model = DataHelper.GetEditPacketModel(unitOfWork, GetAuthenticatedEmail(), packetID);

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }

        /// <summary>
        /// Редактировать пакет (post)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult EditPacket(EditPacketModel model)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                if (ModelState.IsValid)
                {
                    var packetRepo = unitOfWork.CreateRepo<Packet>();
                    var packet = packetRepo.SingleOrDefault(it => it.PacketID == model.PacketModel.PacketID);

                    try
                    {
                        packet.PacketCost = model.PacketModel.PacketCost;
                        packet.PacketName = model.PacketModel.PacketName;
                        packet.PacketsCount = model.PacketModel.PacketsCount;
                        packet.PacketStatusID = (model.PacketModel.PacketStatusID != 0 ? model.PacketModel.PacketStatusID : _packetInitialStatusID);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Введены некорректные данные. Проверьте введенные данные еще раз");

                        model = DataHelper.GetEditPacketModel(unitOfWork, GetAuthenticatedEmail(), model.PacketModel.PacketID);

                        model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                        return View(model);
                    }
                    
                    String error;
                    if ((error = unitOfWork.SaveAndGetError()) != null)
                    {
                        ModelState.AddModelError("", error);
                    }
                    else
                    {
                        return View("Message",
                                    new MessageWithRedirectModel("Изменения сохранены",
                                                                "OfferInfo",
                                                                "Default",
                                                                new RouteValueDictionary() { { "offerID", model.PacketModel.OfferID } }));
                    }
                }


                model = DataHelper.GetEditPacketModel(unitOfWork, GetAuthenticatedEmail(), model.PacketModel.PacketID);

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }
        
        /// <summary>
        /// Удалить пакет
        /// </summary>
        /// <param name="carID"></param>
        /// <param name="pageStr"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult DeletePacket(Int32 packetID)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var packetRepo = unitOfWork.CreateRepo<Packet>();
                Packet packet = packetRepo.SingleOrDefault(it => it.PacketID == packetID);

                if (packet == null)
                {
                    return PartialView("MessageWithRedirect", new MessageWithRedirectModel("Пакет не найден", "", "", null));

                }

                Int32 offerID = packet.OfferID;
                packetRepo.Delete(packet);

                String error;
                if ((error = unitOfWork.SaveAndGetError()) != null)
                {
                    ModelState.AddModelError("", "Не удалось удалить пакет");
                }
                else
                {
                    return PartialView("MessageWithRedirect",
                                new MessageWithRedirectModel("Пакет успешно удален",
                                                                "OfferInfo",
                                                                "Default",
                                                                new RouteValueDictionary() { { "offerID", offerID } }));
                }
            }
            return PartialView("MessageWithRedirect",
                                new MessageWithRedirectModel("Не удалось удалить пакет",
                                                                "UserOffers",
                                                                "Default",
                                                                null));
        }

        /// <summary>
        /// Получить сертификат (post)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult GetCertificate(Int32 packetID)
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();
                var user = userRepo.GetByEmail(GetAuthenticatedEmail());
                var packet = unitOfWork.CreateRepo<Packet>().SingleOrDefault(it => it.PacketID == packetID);

                //if (packet != null && user.UserPiarsCount >= packet.PacketCost)
                //{
                //    packet.Certificates.Add(new Certificate() 
                //    {
                //        CustomerID = user.UserID,
                //        CertificateCreateDate = DateTime.Now,
                //        CertificateIsImplement = false,
                //        CertificateCodeValue = DataHelper.GetRandomCertificateCodeValue()
                //    });

                //    user.UserPiarsCount -= packet.PacketCost;
                //}

                try
                {
                    if (packet != null && user.UserPiarsCount >= packet.PacketCost)
                    {
                        var certificate = new Certificate()
                        {
                            CashOperations = new List<CashOperation>()
                        };

                        certificate.CashOperations.Add(new CashOperation()
                            {
                                CashAmount = packet.PacketCost,
                                CashOperationTypeID = 3, // ТАК НЕЛЬЗЯ ДЕЛАТЬ, ПЕРЕДЕЛАТЬ ПОЗЖЕ!!!
                                CertificateCount = 1, //пока так
                                OperationDate = DateTime.Now,
                                UserID = user.UserID
                            });

                        certificate.CustomerID = user.UserID;
                        certificate.CertificateCreateDate = DateTime.Now;
                        certificate.CertificateIsImplement = false;
                        certificate.CertificateCodeValue = DataHelper.GetRandomCertificateCodeValue();

                        packet.Certificates.Add(certificate);
                        user.UserPiarsCount -= packet.PacketCost;
                    }
                    else
                    {
                        return View("Message",
                                        new MessageWithRedirectModel("Не удалось приобрести сертификат",
                                                                    "Index",
                                                                    "Default", null));
                    }

                }
                catch (Exception)
                {
                    return View("Message",
                                        new MessageWithRedirectModel("Не удалось приобрести сертификат",
                                                                    "Index",
                                                                    "Default", null));
                }
                

               
                String error;
                if ((error = unitOfWork.SaveAndGetError()) != null)
                {
                    ModelState.AddModelError("", error);
                }
                else
                {
                    return PartialView("MessageWithRedirect",
                                new MessageWithRedirectModel("Сертификат успешно приобретен",
                                                                "UserCertificates",
                                                                "Default",
                                                                null));
                }

                var model = new UserCertificatesModel();
                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View("Message",
                                    new MessageWithRedirectModel("Сертификат успешно приобретен",
                                                                "Index",
                                                                "Default", null));
            }
        }

        /// <summary>
        /// Сертификаты пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult UserCertificates()
        {
            var userEmail = GetAuthenticatedEmail();

            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {                
                var model = new UserCertificatesModel()
                {
                    GottenCertificates = unitOfWork.CreateRepo<Certificate>().Where(cer => cer.User.UserEmail == userEmail).Select(cer => new CertificateModel()
                    {
                        CertificateCodeValue = cer.CertificateCodeValue,
                        CertificateCreateDate = cer.CertificateCreateDate,
                        CertificateID = cer.CertificateID,
                        CertificateImplementDate = cer.CertificateImplementDate,
                        CertificateIsImplement = cer.CertificateIsImplement,
                        OfferID = cer.Packet.OfferID,
                        PacketCost = cer.Packet.PacketCost,
                        PacketID = cer.PacketID,
                        PacketName = cer.Packet.PacketName,

                        SellerID = cer.Packet.Offer.UserID,
                        SellerName = cer.Packet.Offer.User.UserNick
                    }).ToList(),
                    SoldCertificates = unitOfWork.CreateRepo<Certificate>().Where(cer => cer.Packet.Offer.User.UserEmail == userEmail).Select(cer => new CertificateModel()
                    {
                        CertificateCreateDate = cer.CertificateCreateDate,
                        CertificateID = cer.CertificateID,
                        CertificateImplementDate = cer.CertificateImplementDate,
                        CertificateIsImplement = cer.CertificateIsImplement,
                        OfferID = cer.Packet.OfferID,
                        PacketCost = cer.Packet.PacketCost,
                        PacketID = cer.PacketID,
                        PacketName = cer.Packet.PacketName,

                        CustomerID = cer.CustomerID,
                        CustomerName = cer.User.UserNick
                    }).ToList(),
                };            

                //var user = unitOfWork.CreateInterfacedRepo<IUserRepo>().GetByLogin(GetAuthenticatedEmail());
                //var soldCertificates = unitOfWork.CreateRepo<Certificate>().Where(cer => cer.Packet.Offer.UserID == user.UserID);
                //var gottenCertificates = unitOfWork.CreateRepo<Certificate>().Where(cer => cer.CustomerID == user.UserID);
                
                //foreach (var cer in gottenCertificates)
                //{
                //    model.GottenCertificates.Add(new CertificateModel() 
                //    {
                //        CertificateCodeValue = cer.CertificateCodeValue,
                //        CertificateCreateDate = cer.CertificateCreateDate,
                //        CertificateID = cer.CertificateID,
                //        CertificateImplementDate = cer.CertificateImplementDate,
                //        CertificateIsImplement = cer.CertificateIsImplement,
                //        OfferID = cer.Packet.OfferID,
                //        PacketCost = cer.Packet.PacketCost,
                //        PacketID = cer.PacketID,
                //        PacketName = cer.Packet.PacketName,
                        
                //        SellerID = cer.Packet.Offer.UserID,
                //        SellerName = cer.Packet.Offer.User.UserNick
                //    });
                //};

                //foreach (var cer in soldCertificates)
                //{
                //    model.SoldCertificates.Add(new CertificateModel()
                //    {
                        //CertificateCodeValue = cer.CertificateCodeValue,
                        //CertificateCreateDate = cer.CertificateCreateDate,
                        //CertificateID = cer.CertificateID,
                        //CertificateImplementDate = cer.CertificateImplementDate,
                        //CertificateIsImplement = cer.CertificateIsImplement,
                        //OfferID = cer.Packet.OfferID,
                        //PacketCost = cer.Packet.PacketCost,
                        //PacketID = cer.PacketID,
                        //PacketName = cer.Packet.PacketName,

                        //CustomerID = cer.CustomerID,
                        //CustomerName = cer.User.UserNick
                //    });
                //};

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());     
                return View(model);           
            }            
        }

        /// <summary>
        /// Кошелек пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult UserPurse()
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                //var userID = unitOfWork.CreateInterfacedRepo<IUserRepo>().GetByLogin(GetAuthenticatedEmail());
                //var operations = unitOfWork.CreateRepo<CashOperation>().Where(op => op.UserID == user.UserID);
                var userEmail = GetAuthenticatedEmail();

                var model = new PurseModel()
                {
                    Operations = unitOfWork.CreateRepo<CashOperation>()
                                .Where(op => op.User.UserEmail == userEmail)
                                .Select(op => new CashOperationModel()
                    {
                        CashAmount = op.CashAmount,
                        CashOperationID = op.CashOperationID,
                        CashOperationTypeID = op.CashOperationTypeID,
                        CashOperationTypeName = op.CashOperationType.CashOperationTypeName,
                        CertificateCount = op.CertificateCount,
                        CertificateID = op.CertificateID,
                        OperationDate = op.OperationDate,
                        PacketName = (op.Certificate != null ? op.Certificate.Packet.PacketName : null),
                        IsIncrementOperation = op.CashOperationType.IsIncrementOperation,
                        OfferID = op.Certificate.Packet.OfferID
                    }).ToList()
                };

                //foreach (var op in operations)
                //{
                //    model.Operations.Add(new CashOperationModel()
                //    {
                //        CashAmount = op.CashAmount,
                //        CashOperationID = op.CashOperationID,
                //        CashOperationTypeID = op.CashOperationTypeID,
                //        CashOperationTypeName = op.CashOperationType.CashOperationTypeName,
                //        CertificateCount = op.CertificateCount,
                //        CertificateID = op.CertificateID,
                //        OperationDate = op.OperationDate,
                //        PacketName = (op.Certificate != null ? op.Certificate.Packet.PacketName : null),
                //        IsIncrementOperation = op.CashOperationType.IsIncrementOperation,
                //        OfferID = op.Certificate.Packet.OfferID
                //    });
                //};
                
                
                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }
        }

        /// <summary>
        /// Активация
        /// </summary>
        /// <param name="model"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Activation()
        {
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var model = new ActivationModel();

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail());
                return View(model);
            }            
        }

        /// <summary>
        /// Активация (post)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Activation(ActivationModel model)
        {            
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var certificate = unitOfWork.CreateRepo<Certificate>().SingleOrDefault(cer => cer.CertificateCodeValue == model.ActivationCode);
                var user = unitOfWork.CreateInterfacedRepo<IUserRepo>().GetByEmail(GetAuthenticatedEmail());

                if (certificate != null && certificate.Packet.Offer.User.UserEmail == user.UserEmail)
                {
                    try
                    {
                        certificate.CertificateIsImplement = true;
                        certificate.CertificateImplementDate = DateTime.Now;
                        user.UserPiarsCount += certificate.Packet.PacketCost;
                    }
                    catch (Exception)
                    {
                        return View("Message",
                                    new MessageWithRedirectModel("Указанный код не найден",
                                                                "Activation",
                                                                "Default",
                                                                null));
                    }

                    String error;
                    if ((error = unitOfWork.SaveAndGetError()) != null)
                    {
                        return View("Message",
                                    new MessageWithRedirectModel("Указанный код не найден",
                                                                "Activation",
                                                                "Default",
                                                                null));
                    }
                    else
                    {
                        return View("Message",
                                    new MessageWithRedirectModel("Код успешно принят системой",
                                                                "UserCertificates",
                                                                "Default",
                                                                null));
                    }
                }

                return View("Message",
                                    new MessageWithRedirectModel("Указанный код не найден",
                                                                "Activation",
                                                                "Default",
                                                                null));
            }
        }

        /// <summary>
        /// Все предложения
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Contacts()
        {            
            using (var unitOfWork = UnitOfWorkProvider.CreateUnitOfWork())
            {
                var model = new ContactsModel();

                model.FillBaseDefaultModel(unitOfWork, GetAuthenticatedEmail(), "Contacts");
                return View(model);
            } 
        }

        private static Int32 _offerInitialStatusID = 1; // первоначальный статус предложенния - на рассмотрении
        private static Int32 _packetInitialStatusID = 1; // первоначальный статус пакета - на рассмотрении
    }    
}

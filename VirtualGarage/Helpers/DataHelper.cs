using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KK.Data;
using BarterMarket.Models;
using BarterMarket.Logic.Repository;
using BarterMarket.Logic.Enums;
using BarterMarket.Logic;
using AutoMapper;
using BarterMarket.Logic.DataModel;
using System.Web.Mvc;

namespace BarterMarket.Helpers
{
    public static class DataHelper
    {


        public static BaseDefaultModel FillBaseDefaultModel(IUnitOfWork unitOfWork, String userEmail)
        {
            if (userEmail != null)
            {

                var us = unitOfWork.CreateInterfacedRepo<IUserRepo>().GetByEmail(userEmail);

                if (us != null)
                {
                    var model = new BaseDefaultModel()
                    {
                        UserID = us.UserID,
                        UserNick = us.UserNick,
                        UserPiarsCount = us.UserPiarsCount,
                        UserOffers = us.Offers.Select(it =>
                                            new SimpleUserOffer()
                                            {
                                                OfferID = it.OfferID,
                                                OfferName = it.OfferName
                                            }).ToList()
                    };

                    return model;
                }
            }
            return null;
        }

        public static AddOfferModel GetAddOfferModel(IUnitOfWork unitOfWork, String userEmail)
        {
            return new AddOfferModel()
            {
                //BaseModel = new BaseDefaultModel(),
                OfferModel = new OfferModel() 
                {
                    OfferCategories = GetOfferCategoriesListItems(unitOfWork)
                }
            };                        
        }

        public static EditOfferModel GetEditOfferModel(IUnitOfWork unitOfWork, String userEmail, Int32 offerID)
        {
            //EditOfferModel result = (EditOfferModel)GetAddOfferModel(unitOfWork, userName);
            var offer = unitOfWork.CreateRepo<Offer>().SingleOrDefault(it => it.OfferID == offerID);

            if (offer != null)
            {
                return new EditOfferModel()
                {
                    OfferModel = new OfferModel()
                    {
                        ImageType = offer.ImageType,
                        OfferCategoryID = offer.OfferCategoryID,
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
                        OfferCategories = GetOfferCategoriesListItems(unitOfWork),
                        OfferStatusID = offer.OfferStatusID,
                        OfferStatusName = offer.OfferStatus.OfferStatusName
                    }
                };                    
            }
            
            return null;
        }

        public static AddPacketModel GetAddPacketModel(IUnitOfWork unitOfWork, String userEmail, Int32 offerID)
        {
            return new AddPacketModel()
            {
                OfferName = unitOfWork.CreateRepo<Offer>().SingleOrDefault(it => it.OfferID == offerID).OfferName,
                PacketModel = new PacketModel() { OfferID = offerID }
            };
        }

        public static EditPacketModel GetEditPacketModel(IUnitOfWork unitOfWork, String userEmail, Int32 packetID)
        {
            var packet = unitOfWork.CreateRepo<Packet>().SingleOrDefault(it => it.PacketID == packetID);

            if (packet != null)
            {
                return new EditPacketModel()
                {
                    PacketModel = new PacketModel() 
                    {
                        OfferID = packet.OfferID,
                        PacketCost = packet.PacketCost,
                        PacketDate = packet.PacketDate,
                        PacketID = packet.PacketID,
                        PacketName = packet.PacketName,
                        PacketsCount = packet.PacketsCount,
                        PacketStatusID = packet.PacketStatusID
                    },
                    OfferName = packet.Offer.OfferName
                };
            }
            return null;
        }

        public static List<SelectListItem> GetOfferCategoriesListItems(IUnitOfWork unitOfWork)
        {
            return unitOfWork.CreateRepo<OfferCategory>().Select(offerCategory => new
                    {
                        Text = offerCategory.OfferCategoryName,
                        Value = offerCategory.OfferCategoryID
                    }).ToList().Select(t => new SelectListItem()
                    {
                        Text = t.Text,
                        Value = t.Value.ToString()
                    }).ToList();
        }

        public static AddOfferModel GetAddOfferModel(AddOfferModel model, IUnitOfWork unitOfWork, String userEmail)
        {

            model.BaseModel = new BaseDefaultModel();
            model.OfferModel.OfferCategories = unitOfWork.CreateRepo<OfferCategory>().Select(offerCategory => new
            {
                Text = offerCategory.OfferCategoryName,
                Value = offerCategory.OfferCategoryID
            }).ToList().Select(t => new SelectListItem()
            {
                Text = t.Text,
                Value = t.Value.ToString()
            }).ToList();

            return model;
        }

        public static List<OfferModel> GetAllOffers(IUnitOfWork unitOfWork)
        {
            var offerRepo = unitOfWork.CreateRepo<Offer>();
            return offerRepo.Select(offer => new OfferModel()                       
                                                    {
                                                        ImageType = offer.ImageType,
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
                                                        OfferPhoto = offer.OfferPhoto,
                                                        OfferTelegramLink = offer.OfferTelegramLink,
                                                        OfferTwitterLink = offer.OfferTwitterLink,
                                                        OfferViberLink = offer.OfferViberLink,
                                                        OfferVKLink = offer.OfferVKLink,
                                                        OfferWhatsappLink = offer.OfferWhatsappLink,
                                                        UserID = offer.UserID,
                                                        OfferStatusID = offer.OfferStatusID,
                                                        OfferCategoryName = offer.OfferCategory.OfferCategoryName,
                                                        UserName = offer.User.UserNick                                                        
                                                    }).ToList();
        }

        public static List<OfferModel> GetAllPublicOffers(IUnitOfWork unitOfWork)
        {
            return GetAllOffers(unitOfWork).Where(of => of.OfferStatusID == 2).ToList();    
        }

        public static String GetRandomCertificateCodeValue()
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[9];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

        public static List<OfferModel> GetUserOffers(IUnitOfWork unitOfWork, Int32 userID)
        {
            var userRepo = unitOfWork.CreateInterfacedRepo<IUserRepo>();

            List<OfferModel> offerModels = (from offer
                                                        in userRepo.GetUserOffers(userID)
                                            select new OfferModel()
                                            {
                                                ImageType = offer.ImageType,
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
                                                OfferPhoto = offer.OfferPhoto,
                                                OfferTelegramLink = offer.OfferTelegramLink,
                                                OfferTwitterLink = offer.OfferTwitterLink,
                                                OfferViberLink = offer.OfferViberLink,
                                                OfferVKLink = offer.OfferVKLink,
                                                OfferWhatsappLink = offer.OfferWhatsappLink,
                                                OfferCategoryName = offer.OfferCategory.OfferCategoryName,
                                                OfferPacketsCount = offer.Packets.Count,
                                                OfferStatusID = offer.OfferStatusID,
                                                OfferStatusName = offer.OfferStatus.OfferStatusName
                                            }).ToList();

            return offerModels;
        }

	}

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KK.Data;
using BarterMarket.Logic.DataModel;
using BarterMarket.Logic.Enums;

namespace BarterMarket.Logic.Repository
{
    public interface IUserRepo : IRepository<User>
    {
        /// <summary>
        /// get user by login or return null, if it no
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        User GetByEmail(String userEmail);

        /// <summary>
        /// get user's offers by login
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        IEnumerable<Offer> GetUserOffers(String userEmail);

        /// <summary>
        /// Get User's Offers
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        IEnumerable<Offer> GetUserOffers(Int32 userID);

        /// <summary>
        /// Проверяет данные пользователя при входе
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        LoginResult CheckEmailAndPass(String userEmail, String pass);

        /// <summary>
        /// Проверяет, существует ли пользователь с указанным именем
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Boolean IsEmailExist(String userEmail);        
    }
}

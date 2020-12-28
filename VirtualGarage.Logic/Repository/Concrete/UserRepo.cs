using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KK.Data;
using BarterMarket.Logic.DataModel;
using BarterMarket.Logic.Exceptions;
using BarterMarket.Logic.Enums;

namespace BarterMarket.Logic.Repository
{
    class UserRepo : RepositoryEF<User>, IUserRepo
    {
        public UserRepo(IUnitOfWorkEF unitOfWork)
            : base(unitOfWork)
        {
        }

        public User GetByEmail(string userEmail)
        {
            return this.SingleOrDefault(it => it.UserEmail == userEmail);
        }

        public Boolean IsEmailExist(String userEmail)
        {
            return this.Any(it => it.UserEmail == userEmail);
        }

        public IEnumerable<Offer> GetUserOffers(String userEmail)
        {
            var user = this.SingleOrDefault(it => it.UserEmail == userEmail);
            if (user != null)
            {
                return user.Offers.AsEnumerable();
            }
            else
            {
                throw new UserNotExistException();
            }
        }

        public IEnumerable<Offer> GetUserOffers(Int32 userID)
        {
            var user = this.SingleOrDefault(it => it.UserID == userID);
            if (user != null)
            {
                return user.Offers.AsEnumerable();
            }
            else
            {
                throw new UserNotExistException();
            }
        }

        public LoginResult CheckEmailAndPass(string userEmail, string pass)
        {
            User user;
            if ((user = this.GetByEmail(userEmail)) == null)
            {
                return LoginResult.UserNotExist;
            }
            else if (user.UserPassword != pass)
            {
                return LoginResult.UncorrectPass;
            }
            else
            {
                return LoginResult.Success;
            }
        }        
    }
}

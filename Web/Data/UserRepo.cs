using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortUrl.Models;
using Biggy;
using Biggy.JSON;

namespace ShortUrl.Data
{
    public class UserRepo
    {

        BiggyList<User> db;
        public UserRepo()
        {
            db = new BiggyList<User>(dbPath: HttpRuntime.AppDomainAppPath);
        }

        //public User Get(string providerToken)
        //{
        //    return db.SingleOrDefault(x => x.Token == providerToken.Trim());
        //}
        public void Save(User user)
        {
            user.SubmittedUrls = null;
            db.Add(user);
        }

        public void Update(User user)
        {
            user.SubmittedUrls = null;
            db.Update(user);
        }

        public void Remove(User user)
        {
            db.Remove(user);
        }

        public User GetLogin(string loginProvider, string providerKey)
        {
            return db.SingleOrDefault(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);
        }

        public User GetById(string id)
        {
            return db.SingleOrDefault(x => x.UserId.ToString() == id);
        }

        public User GetByName(string userName)
        {
            return db.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
        }
    }
}
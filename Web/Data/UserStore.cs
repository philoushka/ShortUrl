using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortUrl.Models;
using System.Threading.Tasks;

namespace ShortUrl.Data
{
    public class UserStore : IUserStore<User>, IUserLoginStore<User>, IUserSecurityStampStore<User> 
    {
        UserRepo repo;

        public UserStore()
        {
            repo = new UserRepo();
        }

        public void Dispose() { }

        #region IUserStore
        public virtual Task CreateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                repo.Save(user);
            });
        }

        public virtual Task DeleteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                repo.Remove(user);
            });
        }

        public virtual Task<User> FindByIdAsync(string id)
        {
            if (id.IsNullOrWhiteSpace())
                throw new ArgumentNullException("id");

            return Task.Factory.StartNew(() =>
            {
                return repo.GetById(id);
            });
        }


        public virtual Task<User> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            return Task.Factory.StartNew(() =>
            {
                return repo.GetByName(userName);
            });
        }

        public virtual Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                repo.Update(user);
            });
        }
        #endregion

        #region IUserLoginStore
        public virtual Task AddLoginAsync(User user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (login == null)
                throw new ArgumentNullException("login");

            return Task.Factory.StartNew(() =>
            {
                user.LoginProvider = login.LoginProvider;
                user.ProviderKey = login.ProviderKey;
                repo.Update(user);
            });
        }

        public virtual Task<User> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            return Task.Factory.StartNew(() =>
            {
                return repo.GetLogin(login.LoginProvider, login.ProviderKey);
            });
        }

        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                var found = repo.GetById(user.UserId.ToString());
                if (found == null)
                    return (IList<UserLoginInfo>)(new List<UserLoginInfo>());
                else
                    return (IList<UserLoginInfo>)(new List<UserLoginInfo>() { new UserLoginInfo(found.LoginProvider, found.ProviderKey) });
            });
        }

        public virtual Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (login == null)
                throw new ArgumentNullException("login");

            return Task.Factory.StartNew(() =>
            {
                user.ProviderKey = user.LoginProvider = string.Empty;
                repo.Update(user);
            });
        }
        #endregion

      

        #region IUserSecurityStampStore
        public virtual Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        #endregion


    }
}
using ShortUrl.Data;
using ShortUrl.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace ShortUrl
{
    public class UsersController : ApiController
    {
        UserRepo repo;
        public UsersController()
        {
            repo = new UserRepo();
        }
        
        //[HttpGet]
        //public User Get(string token)
        //{            
        //    return repo.Get(token);
        //}
      
        [HttpPost]
        public void CreateNewUser(User user)
        {
            repo.Save(user);
        }

        [HttpPost]
        public void Update(User user)
        {
            repo.Update(user);
        }

        [HttpPost]
        public void Delete(User user)
        {
            repo.Remove(user);
        }

       

    }
}
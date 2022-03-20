using System;
using System.Collections.Generic;
using System.Linq;
using CrawfordTask.Common.Entities;
using CrawfordTask.Common.Helpers;

namespace CrawfordTask.Common.Services
{
    public class CrawfordService : ICrawfordService
    {
        private InterviewContext _context;

        public CrawfordService(InterviewContext context)
        {
            _context = context;
        }

        #region Users

        public Users Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(u => u.UserName == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct and isActive - authentication successful
            if (user.UserName == username && user.Password == password && user.Active == true)
                return user;
            else
                return null;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _context.Users;
        }

        public Users GetUserById(int id)
        {
            return _context.Users.Find(id);
        }
        #endregion

        #region LossTypes
        public IEnumerable<LossTypes> GetAllLossTypes()
        {
            return _context.LossTypes;
        }

        public LossTypes GetLossTypeById(int id)
        {
            return _context.LossTypes.Find(id);
        }

        #endregion
    }
}
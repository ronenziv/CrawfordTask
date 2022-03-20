using System;
using System.Collections.Generic;
using System.Linq;
using CrawfordTask.Common.Entities;
using CrawfordTask.Common.Helpers;

namespace CrawfordTask.Common.Services
{
    public interface ICrawfordService
    {
        Users Authenticate(string username, string password);
        IEnumerable<Users> GetAllUsers();
        Users GetUserById(int id);

        IEnumerable<LossTypes> GetAllLossTypes();
        LossTypes GetLossTypeById(int id);
    }
}
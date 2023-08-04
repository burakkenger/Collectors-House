﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfUserRepository : GenericRepository<User>, IUserDal
    {
        Context context = new Context();
        public List<User> efRep_GetAllIncludeOthers()
        {
            return context.Users
                .Include(l => l.Followings).ThenInclude(l => l.Follower)
                .Include(l => l.Followers).ThenInclude(l => l.Followed)
                .Include(l => l.FavProducts)
                .Include(l => l.Products).ThenInclude(l => l.Category)
                .Include(l => l.Products).ThenInclude(l => l.Images)
                .Include(l => l.Chats)
                .Include(l => l.SentMessages)
                .Include(l => l.ReceivingMessages)
                .ToList();
        }



        public User efRep_GetIncludeChats(int ID)
        {
            return context.Users
                .Include(Usr => Usr.Chats).ThenInclude(Cht => Cht.Messages)
                .FirstOrDefault(Usr => Usr.ID == ID);
        }
    }
}

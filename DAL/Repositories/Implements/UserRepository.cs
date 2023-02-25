﻿using System;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Infacstucture;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
    public class UserRepository : FRMDbContextBase<User>, IUserRepository
    {
        private readonly FRMDbContext _dbContext;
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }
       
    }
}

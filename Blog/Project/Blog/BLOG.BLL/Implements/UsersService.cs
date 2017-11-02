using BLOG.DAL;
using BLOG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG.BLL
{
    public class UsersService : BaseService<Users>,IUsersService
    {
        private readonly IBaseDAL<Users> BaseDal;
        public UsersService(IBaseDAL<Users> _baseDal) : base(_baseDal)
        {
            BaseDal = _baseDal;
        }


    }
}

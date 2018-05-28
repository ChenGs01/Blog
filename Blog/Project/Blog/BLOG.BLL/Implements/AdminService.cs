using BLOG.DAL;
using BLOG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG.BLL
{
    public class AdminService : BaseService<Admin>, IAdminService
    {
        private IBaseDAL<Admin> BaseDal { get; set; }
        public AdminService(IBaseDAL<Admin> _baseDal) : base(_baseDal)
        {
            BaseDal = _baseDal;
        }
    }
}

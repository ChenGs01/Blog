using BLOG.DAL;
using BLOG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG.BLL
{
    public class CategorysService : BaseService<Categorys>, ICategorysService
    {
        private readonly IBaseDAL<Categorys> BaseDal;
        public CategorysService(IBaseDAL<Categorys> _baseDal) : base(_baseDal)
        {
            BaseDal = _baseDal;
        }
    }
}

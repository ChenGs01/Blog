using BLOG.DAL;
using BLOG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG.BLL
{
    public class ArticlesService: BaseService<Articles>, IArticlesService
    {
        private  IBaseDAL<Articles> BaseDal { get; set; }
        public ArticlesService(IBaseDAL<Articles> _baseDal) : base(_baseDal)
        {
            BaseDal = _baseDal;
        }




    }
}

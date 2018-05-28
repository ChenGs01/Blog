using BLOG.DAL;
using BLOG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG.BLL
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        private readonly IBaseDAL<Message> BaseDal;
        public MessageService(IBaseDAL<Message> _baseDal) : base(_baseDal)
        {
            BaseDal = _baseDal;
        }
    }
}

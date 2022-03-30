using Custom.IBLL;
using Custom.IDAL;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Custom.BLL
{
    public class UserBLL : IUserBLL
    {
        private IUserDAL iuserDAL;
        public UserBLL(IUserDAL userDAL)
        {
            iuserDAL = userDAL;
        }

        public UserModel Find(int id)
        {
            return iuserDAL.Find(id);
        }

        public IEnumerable<UserModel> Find(Expression<Func<UserModel, bool>> exp)
        {
            return iuserDAL.Find(exp);
        }

        public bool Insert(UserModel model)
        {
            return iuserDAL.Insert(model);
        }

        public bool Update(UserModel model)
        {
            return iuserDAL.Update(model);
        }

        public bool Delete(UserModel model)
        {
            return iuserDAL.Delete(model);
        }
    }
}

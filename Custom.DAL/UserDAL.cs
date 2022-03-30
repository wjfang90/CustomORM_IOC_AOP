using Custom.IDAL;
using Custom.Interface;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Custom.DAL
{
    public class UserDAL : IUserDAL
    {

        UserModel IQuery<UserModel>.Find(int id)
        {
            return SqlHelper.Find<UserModel>(id);
        }

        public IEnumerable<UserModel> Find(Expression<Func<UserModel, bool>> exp)
        {
            return SqlHelper.Find(exp);
        }

        public bool Insert(UserModel model)
        {
            return SqlHelper.Insert(model);
        }

        public bool Update(UserModel model)
        {
            return SqlHelper.Update(model);
        }

        public bool Delete(UserModel model)
        {
            return SqlHelper.Delete(model);
        }
    }
}

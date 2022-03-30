using Custom.IDAL;
using Custom.Interface;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Custom.DAL
{
    public class CompanyDAL : ICompanyDAL
    {

        CompanyModel IQuery<CompanyModel>.Find(int id)
        {
            return SqlHelper.Find<CompanyModel>(id);
        }

        public IEnumerable<CompanyModel> Find(Expression<Func<CompanyModel, bool>> exp)
        {
            return SqlHelper.Find(exp);
        }

        public bool Insert(CompanyModel model)
        {
            return SqlHelper.Insert(model);
        }

        public bool Update(CompanyModel model)
        {
            return SqlHelper.Update(model);
        }

        public bool Delete(CompanyModel model)
        {
            return SqlHelper.Delete(model);
        }
    }
}

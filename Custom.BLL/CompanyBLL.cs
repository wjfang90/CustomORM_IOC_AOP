using Custom.IBLL;
using Custom.IDAL;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Custom.BLL
{
    public class CompanyBLL : ICompanyBLL
    {
        private ICompanyDAL iCompanyDAL;
        public CompanyBLL(ICompanyDAL CompanyDAL)
        {
            iCompanyDAL = CompanyDAL;
        }

        public CompanyModel Find(int id)
        {
            return iCompanyDAL.Find(id);
        }

        public IEnumerable<CompanyModel> Find(Expression<Func<CompanyModel, bool>> exp)
        {
            return iCompanyDAL.Find(exp);
        }

        public bool Insert(CompanyModel model)
        {
            return iCompanyDAL.Insert(model);
        }

        public bool Update(CompanyModel model)
        {
            return iCompanyDAL.Update(model);
        }

        public bool Delete(CompanyModel model)
        {
            return iCompanyDAL.Delete(model);
        }
    }
}

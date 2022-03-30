using Custom.Interface;
using Custom.Model;
using System;

namespace Custom.IDAL
{
    public interface ICompanyDAL : IQuery<CompanyModel>, IInsert<CompanyModel>, IUpdate<CompanyModel>, IDelete<CompanyModel>
    {

    }
}

using Custom.Interface;
using Custom.Model;
using System;

namespace Custom.IBLL
{
    public interface ICompanyBLL : IQuery<CompanyModel>, IInsert<CompanyModel>, IUpdate<CompanyModel>, IDelete<CompanyModel>
    {
    }
}

using Custom.Interface;
using Custom.Model;
using System;

namespace Custom.IDAL
{
    public interface IUserDAL : IQuery<UserModel>, IInsert<UserModel>, IUpdate<UserModel>, IDelete<UserModel>
    {

    }
}

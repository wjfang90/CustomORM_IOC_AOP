using Custom.Interface;
using Custom.Model;
using System;

namespace Custom.IBLL
{
    public interface IUserBLL : IQuery<UserModel>, IInsert<UserModel>, IUpdate<UserModel>, IDelete<UserModel>
    {
    }
}

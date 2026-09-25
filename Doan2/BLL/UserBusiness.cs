using BLL.Interfaces;
using DAL.Interfaces;
using Model;

namespace BLL
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _res;

        public UserBusiness(IUserRepository res)
        {
            _res = res;
        }

        public bool Register(UserModel model)
        {
            return _res.Register(model);
        }

        public UserModel Login(string email, string password)
        {
            return _res.Login(email, password);
        }

        public UserModel GetById(int userId)
        {
            return _res.GetById(userId);
        }
    }
}
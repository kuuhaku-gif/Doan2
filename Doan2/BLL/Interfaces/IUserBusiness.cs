using Model;

namespace BLL.Interfaces
{
    public interface IUserBusiness
    {
        bool Register(UserModel model);
        UserModel Login(string email, string password);
        UserModel GetById(int userId);
    }
}
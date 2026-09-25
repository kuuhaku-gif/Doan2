using Model;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        bool Register(UserModel model);
        UserModel Login(string email, string password);
        UserModel GetById(int userId);
    }
}
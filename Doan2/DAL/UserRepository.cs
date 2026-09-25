using System;
using System.Collections.Generic;
using DAL.Helper;
using DAL.Interfaces;
using Model;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public UserRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Register(UserModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteNonQuery(out msgError, "sp_user_register",
                    "@FullName", model.FullName,
                    "@Email", model.Email,
                    "@Phone", model.Phone,
                    "@Password", model.Password);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserModel Login(string email, string password)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_user_login",
                    "@Email", email,
                    "@Password", password);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                var list = dt.ConvertTo<UserModel>();
                return list.Count > 0 ? list[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserModel GetById(int userId)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_user_get_by_id",
                    "@UserId", userId);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                var list = dt.ConvertTo<UserModel>();
                return list.Count > 0 ? list[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
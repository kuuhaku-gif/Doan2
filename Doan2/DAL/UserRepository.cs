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

                if (dt != null && dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    return new UserModel
                    {
                        UserId = Convert.ToInt32(row["UserId"]),
                        Email = row["Email"]?.ToString(),
                        FullName = row["FullName"]?.ToString(),
                        // BẮT BUỘC: Đảm bảo có dòng map Role này
                        Role = row["Role"]?.ToString() ?? "User"
                    };
                }

                return null; // Sai email hoặc mật khẩu
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
        public bool SaveOtp(string email, string otpCode)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteNonQuery(out msgError, "sp_user_save_otp",
                    "@Email", email,
                    "@OtpCode", otpCode);

                // Nếu SQL báo lỗi (ví dụ email không tồn tại), ném exception để Controller bắt được
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return true; // Không bị phụ thuộc vào số dòng trả về
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool VerifyOtpAndResetPassword(string email, string otpCode, string newPassword)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteNonQuery(out msgError, "sp_user_verify_otp_and_reset_password",
                    "@Email", email,
                    "@OtpCode", otpCode,
                    "@NewPassword", newPassword);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
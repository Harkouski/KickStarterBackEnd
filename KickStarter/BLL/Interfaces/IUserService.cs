using BLL.Models;
using DAL.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService<T>  
    {
        List<string> DisplayFull();

        Task<T> DisplaySingle(T T);

        Task<User> SignIn(User user, string password);

        Task<T> Add(T T);

        Task Delete(T T);

        Task<T> UpdateUser(T T);

        Task<User> FindUserByEmail(string s);

        Task<string> ReturnResetCode(User user);

        Task<ResetPasswordModel> ResetPassword(ResetPasswordModel T);

        Task<string> ReturnConfirmEmailCode(User user);

        Task<IdentityResult> ConfirmEmail(User user, string code);

        Task<bool> TwoFactorEnabledAsync(User user, bool result);

        Task<string> GenerateTwoFactorTokenAsync(User user);

        Task<bool> VerifyTwoFactorTokenAsync(User user, string code);

        Task<bool> ReturnTwoFactorEnabledValue(string Email);

        Task<IdentityResult> ChengePassword(User user, string oldPassword, string newPassword);

        Task<bool> TwoFactorEnabledAsync(string email);

        Task<bool> ReturnConfirmEmailValue(string Email);

        Task<string> GetUserId(string email);

      
    }
}

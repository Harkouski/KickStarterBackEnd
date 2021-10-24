using BLL.DTO;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace BLL.Services
{
    public class UserService : IUserService<UserDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public SampleContext sampleContext;


        public UserService(UserManager<User> userManager, SampleContext sample, SignInManager<User> signInManager)
        {

            sampleContext = sample;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserDTO> Add(UserDTO userDTO)
        {
            var userAfterDTO = ConvertModel.ConvertModel.ConvertUserDtoToUser(userDTO, sampleContext);

            userAfterDTO.UserName = userDTO.Email;

            var path = Path.Combine("Users", "defaultPicture.png");

            userAfterDTO.Avatar = path;

            try
            {
                await _userManager.CreateAsync(userAfterDTO, userDTO.Password);
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }

            return userDTO;
        }

        public async Task<User> SignIn(User user, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, password, false, false);

            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                throw (new Exception("Write correct Email or password"));
            }
        }

        public async Task<UserDTO> UpdateUser(UserDTO userDTO)
        {

            var user = await _userManager.FindByNameAsync(userDTO.Email);
            if (user != null)
            {
                user.Age = userDTO.Age;
                user.FirstName = userDTO.FirstName;
                user.LastName = userDTO.LastName;
                user.Age = userDTO.Age;
                user.Gender = userDTO.Gender;
                user.Avatar = userDTO.Avatar;
                await _userManager.UpdateAsync(user);
                return userDTO;
            }
            else
                throw (new Exception("Write valid data"));
        }

        public async Task Delete(UserDTO userDTO)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userDTO.Email);
                var qwe = await _userManager.DeleteAsync(user);
                Console.WriteLine();
            }
            catch(Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }



        public async Task<User> FindUserByEmail(string Email)
        {
            User user = await _userManager.FindByNameAsync(Email);

            return user;
        }

        public async Task<string> ReturnResetCode(User user)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            return code;
        }

        public async Task<ResetPasswordModel> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            resetPasswordModel.Code = resetPasswordModel.Code.Replace(' ', '+');
            var user = await FindUserByEmail(resetPasswordModel.Email);
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Code, resetPasswordModel.Password);
            if (result.Succeeded)
            {
                return resetPasswordModel;          
            }
            throw (new Exception("ResetPasswordConfirmation"));
        }

        public async Task<string> ReturnConfirmEmailCode(User user)
        {
            try
            {               
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return code;
            }
            catch(Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        public async Task<string> GenerateTwoFactorTokenAsync(User user)
        {
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
            return code;
        }

        public async Task<bool> VerifyTwoFactorTokenAsync(User user,string code)
        {
            var result = await _userManager.VerifyTwoFactorTokenAsync(user,TokenOptions.DefaultPhoneProvider,code);
            return result;
        }

        public async Task<bool> TwoFactorEnabledAsync(User user, bool chenge)
        {          
            var result = await _userManager.SetTwoFactorEnabledAsync(user, chenge);
            if (result.Succeeded) { return true; }
            else return false;
        }

        public async Task<bool> TwoFactorEnabledAsync(string email)
        {
            var user = await FindUserByEmail(email);

            if (user.TwoFactorEnabled) { return true; }
            else return false;
        }

        public async Task<bool> ReturnTwoFactorEnabledValue(string Email)
        {
            if (Email != null)
            {
                var user = await FindUserByEmail(Email);
                bool result = user.TwoFactorEnabled;
                return result;
            }
            return false;
        }

        public async Task<bool> ReturnConfirmEmailValue(string Email)
        {
            if (Email != null)
            {
                var user = await FindUserByEmail(Email);
                bool result = user.EmailConfirmed;
                return result;
            }
            return false;
        }

        public async Task<IdentityResult> ConfirmEmail(User user, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result;
        }

        public async Task<IdentityResult> ChengePassword(User user, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result;
        }

        public List<string> DisplayFull()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserId(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var id = user.Id;
            return id;
        }

        public async Task<UserDTO> DisplaySingle(UserDTO userDTO)
        {
            return userDTO;
        }
        

        

    }
   
}

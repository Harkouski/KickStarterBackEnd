using BLL;
using BLL.ConvertModel;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using BLL.Token;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KickStarter.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public IUserService<UserDTO> _service;
        private google _google;

        public SampleContext sampleContext;

        public UserController(SampleContext sample, IUserService<UserDTO> userService,google google)
        {
            _google = google;
            sampleContext = sample;
            _service = userService;

        }

        [HttpGet]
        [Route("Get")]
        public List<User> Get()
        {
            return sampleContext.Users.ToList();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetUser")]
        public async Task<ReturnUserModel> GetSingleUser(string email)
        {

            var user = await _service.FindUserByEmail(email);
            var returnuser = ConvertModel.ConvertUserToUserReturnModel(user);
            return returnuser;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Regestration")]
        public async Task<IActionResult> Add(UserDTO userDTO)
        {
            try
            {
                userDTO = await _service.Add(userDTO);
                if (userDTO != null)
                    return Ok("User with this email already exist");

                var user = await _service.FindUserByEmail(userDTO.Email);
                var code = await _service.ReturnConfirmEmailCode(user);

                var callbackUrl = Url.Action(
                "ConfirmEmail",
                "User",
                new
                {
                    code = code,
                    userEmail = userDTO.Email
                },
                protocol: Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(userDTO.Email, "Confirm your account",
                    $"Confirm your registration by clicking on the link: <a href='{callbackUrl}'>link</a>");
                return Ok("Check you email to confirm account");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string code, string userEmail)
        {
            User user = await _service.FindUserByEmail(userEmail);
            await _service.ConfirmEmail(user, code);
            return Ok("Email confirmed");
        }

        [HttpGet]
        [Route("GetUserId")]
        public async Task<IActionResult> GetUserId(string userEmail)
        {
            return Ok(await _service.GetUserId(userEmail));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            try
            {
                if (await _service.ReturnTwoFactorEnabledValue(signInModel.Email) == true && await _service.ReturnConfirmEmailValue(signInModel.Email) == true && signInModel.Code != "" && signInModel.Email != "" )
                {
                    var user = await _service.FindUserByEmail(signInModel.Email);
                    if( await _service.VerifyTwoFactorTokenAsync(user,signInModel.Code) == true )
                    {
                        //var result = await _service.SignIn(user,signInModel.Password);
                        return new ObjectResult(new TokenLogic(sampleContext).GetToken(signInModel.Email));
                    }
                    else
                    {
                        return Ok("Write valide code");
                    }
                }
                if (await _service.ReturnTwoFactorEnabledValue(signInModel.Email) == false && await _service.ReturnConfirmEmailValue(signInModel.Email) == true)
                {
                    var user = await _service.FindUserByEmail(signInModel.Email);
                    var result = await _service.SignIn(user,signInModel.Password);
                    return new ObjectResult(new TokenLogic(sampleContext).GetToken(result.Email));
                }
                else if (await _service.ReturnTwoFactorEnabledValue(signInModel.Email) == true && await _service.ReturnConfirmEmailValue(signInModel.Email) == true)
                {
                    var user = await _service.FindUserByEmail(signInModel.Email);
                    var code = _service.GenerateTwoFactorTokenAsync(user);

                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(user.Email, "SignIn code",
                                    $"You code: {code.Result}");

                    return Ok("Check you email and write code");
                }
                else
                {
                    return Ok("Check you email to confirm account");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        [Route("EnableTwoFactorAuth")]
        public async Task<IActionResult> EnableTwoFactorAuth(TwoFactorAuthModel twoFactorAuthModel)
        {
            User user = await _service.FindUserByEmail(twoFactorAuthModel.Email);
            await _service.TwoFactorEnabledAsync(user, true);
            return Ok("Enable");
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout(string email)
        {
            User user = await _service.FindUserByEmail(email);
            await _service.TwoFactorEnabledAsync(user, true);
            return Ok("Enable");
        }

        [HttpPost]
        [Route("DisableTwoFactorAuth")]
        public async Task<IActionResult> DisableTwoFactorAuth(TwoFactorAuthModel twoFactorAuthModel)
        {
            User user = await _service.FindUserByEmail(twoFactorAuthModel.Email);
            await _service.TwoFactorEnabledAsync(user, false);
            return Ok("Disable");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetTwoFactorAuth")]
        public async Task<IActionResult> GetTwoFactorAuthStatus(string email)
        {
            var result = await _service.ReturnTwoFactorEnabledValue(email);
            if(result == true)
            {
                return Ok("Enable");
            }
            else
            {
                return Ok("Disable");
            }
            
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser(UpdateUserModel updateUserModel)
        {
            try
            {
                var userDTO = ConvertModel.ConvertUserUpdateModelToUserDTO(updateUserModel);

                var checkuser = await _service.UpdateUser(userDTO);
                if (checkuser != null)
                {
                    return Ok("Update successful");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPut]
        [Route("ChengePassword")]
        public async Task<IActionResult> ChengePassword(ChengePasswordModel chengePasswordModel)
        {
            try
            {
                var user = await _service.FindUserByEmail(chengePasswordModel.Email);
                var result  = await _service.ChengePassword(user, chengePasswordModel.OldPassword, chengePasswordModel.NewPassword);
                if (result.Succeeded == true)
                    return Ok("You successfully changed your password");
                else
                    return Ok("Write rigth password");
                
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(DeleteUserModel deleteUserModel)
        {
            try
            {
                var userDTO = ConvertModel.ConvertUserDeleteModelToUserDTO(deleteUserModel);

                await _service.Delete(userDTO);
                return Ok("User deleted successful");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            var user = await _service.FindUserByEmail(forgotPasswordModel.Email);

            if (user == null) //|| !(await _userManager.IsEmailConfirmedAsync(user)) !!!!!!! Потом добавить
            {
                return Ok("User with this Email not found");
            }

            var code = await _service.ReturnResetCode(user);
            var callbackUrl = Url.Content($"http://localhost:3000/resetPassword?code={code}&email={forgotPasswordModel.Email}");
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(user.Email, "Reset Password",
                $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
            return Ok(code);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            var user = await _service.FindUserByEmail(resetPasswordModel.Email);
            if (user == null) //|| !(await _userManager.IsEmailConfirmedAsync(user)) !!!!!!! Потом добавить
            {
                return Ok("User with this Email not found");
            }
            var userAfterReset = await _service.ResetPassword(resetPasswordModel);

            return Ok("Password chenged");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginWithGoogle")]
        public async Task<IActionResult> SignqweIn(string token)
        {
            var info = await _google.VerifyGoogleTokenAsync(token);
            var user = await _service.FindUserByEmail(info.Email);
            
            if (user == null)
            {
                var userNew = new UserDTO();
                userNew.Email = info.Email;
                userNew.FirstName = info.Name;
                userNew.LastName = info.FamilyName;
                await _service.Add(userNew);
                return new ObjectResult(new TokenLogic(sampleContext).GetToken(userNew.Email));
            }
            else
            {
                return new ObjectResult(new TokenLogic(sampleContext).GetToken(user.Email));
            }

        }
    }
}

using BLL.ConvertModel;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Models;
using DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KickStarter.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class DonationHistoryController : Controller
    {
        public IDonationHistoryService<DonationHistoryDTO> _donationHistoryServise;

        public SampleContext sampleContext;

        public DonationHistoryController(SampleContext sample, IDonationHistoryService<DonationHistoryDTO> donationHistoryServise)
        {
            sampleContext = sample;
            _donationHistoryServise = donationHistoryServise;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllDonation")]
        public List<DonationHistoryDTO> GetAll()
        {
            return _donationHistoryServise.DisplayAll();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetUserDonation")]
        public List<UserDonations> GetUserDonation(string id)
        {

            var userDonation = _donationHistoryServise.DisplaySingle(id);

            return userDonation;
        }

        [HttpPost]
        [Route("CreateDonation")]
        public async Task<IActionResult> Add(DonationHistoryDTO donationHistoryDTO)
        {
            try
            {
                _donationHistoryServise.Add(donationHistoryDTO);
                if (donationHistoryDTO == null)
                    return Conflict("Write category data");
                return Ok("Donate added sucssessful");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("DeleteDonation")]
        public List<UserDonations> DeleteComment(int IdDonation, string Id)
        {
           
             _donationHistoryServise.Delete(IdDonation);
            return _donationHistoryServise.DisplaySingle(Id);
        }
    }
}

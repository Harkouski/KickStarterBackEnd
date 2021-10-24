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
    public class ElectedProjectController : Controller
    {
        public IElectedProjectService<ElectedProjectDTO> _electedProjectServise;

        public SampleContext sampleContext;

        public ElectedProjectController(SampleContext sample, IElectedProjectService<ElectedProjectDTO> electedProjectServise)
        {
            sampleContext = sample;
            _electedProjectServise = electedProjectServise;
        }

        [HttpGet]
        [Route("GetAllUserElectedProject")]
        public List<UserElectedProject> GetAllUserElectedProject(string id)
        {
            return _electedProjectServise.DisplaySingle(id);
        }

        [HttpPost]
        [Route("AddElectedProject")]
        public async Task<IActionResult> Add(ElectedProjectDTO electedProjectDTO)
        {
            try
            {
                _electedProjectServise.Add(electedProjectDTO);
                return Ok("This project add to elected list");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteDonation")]
        public List<UserElectedProject> DeleteComment(int IdElectedProject, string Id)
        {

            _electedProjectServise.Delete(IdElectedProject);
            return _electedProjectServise.DisplaySingle(Id);
        }
    }
}

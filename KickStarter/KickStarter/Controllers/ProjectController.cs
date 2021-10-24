using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using DAL.Context;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KickStarter.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        public IProjectService<ProjectDTO> _projectService;

        public SampleContext sampleContext;

        public IHangFireService _hangFireService;

        public ProjectController(SampleContext sample, IProjectService<ProjectDTO> projectService, IHangFireService hangFireService)
        {
            sampleContext = sample;
            _projectService = projectService;
            _hangFireService = hangFireService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllProject")]
        public List<ProjectDTO> GetAll()
        {          
            return _projectService.DisplayAll();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetSingleProject")]
        public ProjectDTO GetSingleProject(int id)
        {
            return _projectService.DisplaySingle(id);
        }

        [HttpPost]
        [Route("CreateProject")]
        public async Task<IActionResult> CreateProject(ProjectDTO projectDTO, string email)
        {
            try
            {
               _projectService.Add(projectDTO);

                if (projectDTO == null)
                    return Conflict("Write project info");

                _hangFireService.SendBeforeEndAWeekProject(email, projectDTO.Name, projectDTO.FinishDate);
                _hangFireService.SendBeforeEndAHourProject(email, projectDTO.Name, projectDTO.FinishDate);
                _hangFireService.SendBeforeEndADayProject(email, projectDTO.Name, projectDTO.FinishDate);

                return Ok("Project added sucssessful");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetAllUserProject")]
        public async Task<IActionResult> AllUserProject(string id)
        {
            try
            {
               var projects = _projectService.GetUserProejctList(id);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateProject")]
        public async Task<IActionResult> UpdateProject(ProjectDTO projectDTO)
        {
            try
            {
                _projectService.Update(projectDTO);
                return Ok("Update successful");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteProject")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                _projectService.Delete(id);
                return Ok("Project deleted successful");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateProjectPicture")]
        public async Task<IActionResult> UpdateProjectPicture([FromForm] IFormFile formFile, int id)
        {
            try
            {
                _projectService.UpdatePicture(formFile, id);
                return Ok("Update successful");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



    }
}

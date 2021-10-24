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
    public class CommentController : Controller
    {
        public ICommentService<CommentDTO> _commentServise;

        public SampleContext sampleContext;

        public CommentController(SampleContext sample, ICommentService<CommentDTO> commentService)
        {
            sampleContext = sample;
            _commentServise = commentService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllComments")]
        public List<CommentDTO> GetAll()
        {
            return _commentServise.DisplayAll();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetCommentsInProject")]
        public List<ProjectCommentsModel> GetCommentsInProject(int id)
        {
            return _commentServise.DisplayAllProjectComments(id);
        }

        [HttpPost]
        [Route("CreateComment")]
        public  List<ProjectCommentsModel> Add(CommentDTO commentDTO)
        {
         
            _commentServise.Add(commentDTO);
            return _commentServise.DisplayAllProjectComments(commentDTO.ProjectId);
        }

        [HttpPut]
        [Route("UpdateComment")]
        public async Task<IActionResult> UpdateProject(CommentDTO commentDTO)
        {
            try
            {
                _commentServise.Update(commentDTO);
                return Ok("Update successful");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                _commentServise.Delete(id);
                return Ok("Comment deleted successful");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}

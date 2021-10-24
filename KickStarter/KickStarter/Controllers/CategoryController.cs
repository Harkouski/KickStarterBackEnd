using BLL.DTO;
using BLL.Interfaces;
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
    public class CategoryController : Controller
    {
        public IService<CategoryDTO> _categoryServise;

        public SampleContext sampleContext;

        public CategoryController(SampleContext sample, IService<CategoryDTO> categoryServise)
        {
            sampleContext = sample;
            _categoryServise = categoryServise;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllCategory")]
        public List<CategoryDTO> GetAll()
        {
            return _categoryServise.DisplayAll();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetSingleCategory")]
        public CategoryDTO GetUserProject(int id)
        {
            return _categoryServise.DisplaySingle(id);
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> Add(CategoryDTO categoryDTO)
        {
            try
            {
                _categoryServise.Add(categoryDTO);
                if (categoryDTO == null)
                    return Conflict("Write category name");
                return Ok("Category added sucssessful");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateProject(CategoryDTO categoryDTO)
        {
            try
            {
                _categoryServise.Update(categoryDTO);
                return Ok("Update successful");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                _categoryServise.Delete(id);
                return Ok("Comment deleted successful");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

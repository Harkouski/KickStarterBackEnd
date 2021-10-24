using BLL.DTO;
using BLL.Interfaces;
using DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KickStarter.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class RatingController : Controller
    {

        public IRatingService<RatingDTO> _ratingServise;

        public SampleContext sampleContext;

        public RatingController(SampleContext sample, IRatingService<RatingDTO> ratingServise)
        {
            sampleContext = sample;
            _ratingServise = ratingServise;
        }

        [HttpPost]
        [Route("AddRating")]
        public float Add(RatingDTO ratingDTO)
        {
            _ratingServise.Add(ratingDTO);
            var rating = _ratingServise.ShowProjectRating(ratingDTO.ProjectId);
            return rating;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ShowRating")]
        public float ShowRating(int projectId)
        {

            var rating = _ratingServise.ShowProjectRating(projectId);
            return rating;
        }
    }
}

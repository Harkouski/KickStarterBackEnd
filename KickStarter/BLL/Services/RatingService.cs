using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RatingService : IRatingService<RatingDTO>
    {
        IUnitOfWork _unitOfWork;
        public RatingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(RatingDTO ratingDTO)
        {
            var configRating = new MapperConfiguration(cfg => cfg.CreateMap<RatingDTO, Rating>());

            var mapperRating = new Mapper(configRating);

            var rating = mapperRating.Map<Rating>(ratingDTO);

            _unitOfWork.Ratings.Add(rating);
        }

        public float ShowProjectRating(int projectId)
        {
            var rating = _unitOfWork.Ratings.ShowProjectRating(projectId);

            return rating;
        }

        public void Update(RatingDTO ratingDTO)
        {
            var configRating = new MapperConfiguration(cfg => cfg.CreateMap<RatingDTO, Rating>());

            var mapperRating = new Mapper(configRating);

            var rating = mapperRating.Map<Rating>(ratingDTO);

            _unitOfWork.Ratings.Update(rating);
        }
    }
}

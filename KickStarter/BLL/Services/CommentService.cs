using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommentService : ICommentService<CommentDTO>
    {
        IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(CommentDTO commentDTO)
        {
            var configComment = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>());

            var mapperComment = new Mapper(configComment);

            var comment = mapperComment.Map<Comment>(commentDTO);

            _unitOfWork.Comments.Add(comment);
        }

        public void Delete(int id)
        {
            _unitOfWork.Comments.Delete(id);
        }

        public List<CommentDTO> DisplayAll()
        {
            var comment = _unitOfWork.Comments.DisplayAll();

            var commentDTO = new List<CommentDTO> { };

            foreach (var prop in comment)
            {
                var configProject = new MapperConfiguration(cfg => cfg.CreateMap<Comment, CommentDTO>());

                var mapperProject = new Mapper(configProject);

                commentDTO.Add(mapperProject.Map<CommentDTO>(prop));

            }

            return commentDTO;
        }

        public List<ProjectCommentsModel> DisplayAllProjectComments(int id)
        {
            var comment = _unitOfWork.Comments.DisplayAllProjectComments(id);

            List<Comment> projectCommentsModels = (List<Comment>)comment[0];

            List<User> users = (List<User>)comment[1];

            var commentsForShow = new List<ProjectCommentsModel>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Comment, ProjectCommentsModel>());
            var mapper = new Mapper(config);

            foreach (var projectComment in projectCommentsModels)
            {
                var someDonationHistoryForShow = mapper.Map<ProjectCommentsModel>(projectComment);

                someDonationHistoryForShow.Email = users.Where(x => x.Id == projectComment.UserId).First().Email;

                someDonationHistoryForShow.Avatar = users.Where(x => x.Id == projectComment.UserId).First().Avatar;

                someDonationHistoryForShow.Date = new DateTime(someDonationHistoryForShow.Date.Year, someDonationHistoryForShow.Date.Month,
                    someDonationHistoryForShow.Date.Day, someDonationHistoryForShow.Date.Hour,
                    someDonationHistoryForShow.Date.Minute, someDonationHistoryForShow.Date.Second);
                commentsForShow.Add(someDonationHistoryForShow);
            }

            return commentsForShow;
   
        }

        public CommentDTO Update(CommentDTO commentDTO)
        {
            var configComment = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>());

            var mapperComment = new Mapper(configComment);

            var comment = mapperComment.Map<Comment>(commentDTO);

            comment = _unitOfWork.Comments.Update(comment);

            configComment = new MapperConfiguration(cfg => cfg.CreateMap<Comment, CommentDTO>());

            mapperComment = new Mapper(configComment);

            commentDTO = mapperComment.Map<CommentDTO>(comment);

            return commentDTO;
        }
    }
}

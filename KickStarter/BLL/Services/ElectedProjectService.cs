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
    public class ElectedProjectService : IElectedProjectService<ElectedProjectDTO>
    {
        IUnitOfWork _unitOfWork;
        public ElectedProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(ElectedProjectDTO electedProjectDTO)
        {
            var configElectedProject = new MapperConfiguration(cfg => cfg.CreateMap<ElectedProjectDTO, ElectedProject>());

            var mapperElectedProject = new Mapper(configElectedProject);

            var electedProject = mapperElectedProject.Map<ElectedProject>(electedProjectDTO);

            _unitOfWork.ElectedProjects.Add(electedProject);
        }

        public void Delete(int id)
        {
            _unitOfWork.ElectedProjects.Delete(id);
        }

        public List<ElectedProjectDTO> DisplayAll()
        {
            throw new NotImplementedException();
        }

        public List<UserElectedProject> DisplaySingle(string id)
        {
            try
            {
                var electedProject = _unitOfWork.ElectedProjects.DisplaySingle(id);

                List<ElectedProject> electedProjects = (List<ElectedProject>)electedProject[0];
                List<Project> prod = (List<Project>)electedProject[1];

                List<UserElectedProject> electedProjectForShow = new List<UserElectedProject>();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<ElectedProject, UserElectedProject>());
                var mapper = new Mapper(config);


                foreach (var currentElectedProject in electedProjects)
                {
                    var someElectedProjectForShow = mapper.Map<UserElectedProject>(currentElectedProject);
                    someElectedProjectForShow.ProjectName = prod.Where(x => x.Id == currentElectedProject.ProjectId).First().Name;
                    someElectedProjectForShow.CurrentDonation = prod.Where(x => x.Id == currentElectedProject.ProjectId).First().CurrentDonation;
                    someElectedProjectForShow.GoalDonation = prod.Where(x => x.Id == currentElectedProject.ProjectId).First().GoalDonation;
                    someElectedProjectForShow.FinishDate = prod.Where(x => x.Id == currentElectedProject.ProjectId).First().FinishDate;
                    someElectedProjectForShow.UserId = prod.Where(x => x.Id == currentElectedProject.ProjectId).First().UserId;
                    someElectedProjectForShow.ProjectId = prod.Where(x => x.Id == currentElectedProject.ProjectId).First().Id;
                    electedProjectForShow.Add(someElectedProjectForShow);
                }

                return electedProjectForShow;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}

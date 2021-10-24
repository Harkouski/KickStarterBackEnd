using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class ProjectService : IProjectService<ProjectDTO>
    {
        IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(ProjectDTO projectDTO)
        {
            var configProject = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>());

            var mapperProject = new Mapper(configProject);

            var project = mapperProject.Map<Project>(projectDTO);

            _unitOfWork.Projects.Add(project);

        }

        public List<ProjectDTO> GetUserProejctList(string userId)
        {
            var projects = _unitOfWork.Projects.DisplayAllUserProject(userId);

            var projectDTO = new List<ProjectDTO> { };

            foreach (var prop in projects)
            {
                var configProject = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>());

                var mapperProject = new Mapper(configProject);

                projectDTO.Add(mapperProject.Map<ProjectDTO>(prop));

            }
            return projectDTO;
        }

        public void Delete(int id)
        {
            _unitOfWork.Projects.Delete(id);
        }

        public List<ProjectDTO> DisplayAll()
        {
            var project = _unitOfWork.Projects.DisplayAll();
            var projectDTO = new List<ProjectDTO> { };
            foreach (var prop in project)
            {
                var configProject = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>());

                var mapperProject = new Mapper(configProject);

                 projectDTO.Add(mapperProject.Map<ProjectDTO>(prop));

            }
            return projectDTO;
        }

        public ProjectDTO DisplaySingle(int id)
        {
            var project = _unitOfWork.Projects.DisplaySingle(id);

            var configProject = new MapperConfiguration(cfg => cfg.CreateMap<Project,ProjectDTO>());

            var mapperProject = new Mapper(configProject);

            var projectDTO = mapperProject.Map<ProjectDTO>(project);

            return projectDTO;

        }

        public ProjectDTO Update(ProjectDTO projectDTO)
        {
            var configProject = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>());

            var mapperProject = new Mapper(configProject);

            var project = mapperProject.Map<Project>(projectDTO);

            project  =_unitOfWork.Projects.Update(project);

            configProject = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>());

            mapperProject = new Mapper(configProject);

            projectDTO = mapperProject.Map<ProjectDTO>(project);

            return projectDTO;
        }

        public void UpdatePicture(IFormFile formFile, int id )
        {
            _unitOfWork.Projects.UpdatePicture( formFile,  id);
        }
    }
}

using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProjectRepository : IProjectRepository<Project>
    {
        private SampleContext db;
        IWebHostEnvironment _appEnvironment;

        public ProjectRepository(SampleContext context, IWebHostEnvironment appEnvironment)
        {
            this.db = context;
            _appEnvironment = appEnvironment;
        }

        public void Add(Project project)
        {
            var check = db.Projects.FirstOrDefault(p => p.Name == project.Name);
            if (check == null)
            {
                var path = Path.Combine("Project", "defaultPicture.jpg");
                project.StartDate = DateTime.Now;
                project.Picture = path;
                project.StartDate = new DateTime(project.StartDate.Year, project.StartDate.Month, project.StartDate.Day, project.StartDate.Hour, project.StartDate.Minute, project.StartDate.Second);
                db.Projects.Add(project);
                db.SaveChanges();
            }
            else
                throw (new Exception("Project with this name already exist"));
        }

        public void Delete(int id)
        {
            var check = db.Projects.FirstOrDefault(p => p.Id == id);
            if (check != null)
            {
                db.Entry(check).State = EntityState.Deleted;
                db.SaveChanges();
            }
            else
                throw (new Exception("Project dosen't exist"));
        }

        public List<Project> DisplayAll()
        {
            List<Project> someProjects = new List<Project>();
            someProjects = db.Projects.ToList();
            if (someProjects != null)
            {
                return someProjects;
            }
            else throw (new Exception("No projects exist"));
        }

        public List<Project> DisplayAllUserProject(string userId)
        {
            List<Project> someProjects = new List<Project>();
            someProjects = db.Projects.Where(p => p.UserId == userId).ToList();
            if (someProjects != null)
            {
                return someProjects;
            }
            else throw (new Exception("No projects exist"));
        }

        public Project DisplaySingle(int id)
        {
            var someProjects = new Project();
            someProjects = db.Projects.FirstOrDefault(p => p.Id == id);
            if (someProjects != null)
            {
                return someProjects;
            }
            else throw (new Exception("Project dosn't exist"));
        }

        public Project Update(Project project)
        {
            var oldproject = db.Projects.AsNoTracking().FirstOrDefault(p => p.Id == project.Id);
            if (oldproject != null)
            {
                oldproject.HeaderDescription = project.HeaderDescription;
                oldproject.MainDescription = project.MainDescription;
                oldproject.GoalDonation = project.GoalDonation;
                
                db.Entry(oldproject).State = EntityState.Modified;
                db.SaveChanges();
                return oldproject;
            }
            else
            {
                throw (new Exception("User with this Email dosen`t exist"));
            }
        }

        public void UpdatePicture(IFormFile formFile, int id)
        {
            var project = db.Projects.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (project != null)
            {
                var namePicture = formFile.FileName;

                string path = Path.Combine("Project", id.ToString());
                string pathFull = Path.Combine(path, namePicture);
                string pathFullLocal = Path.Combine(_appEnvironment.WebRootPath, pathFull);
                Directory.CreateDirectory(Path.GetDirectoryName(pathFullLocal));

                using (var fileStream = new FileStream(pathFullLocal, FileMode.Create))
                {
                    formFile.CopyToAsync(fileStream);
                }
                project.Picture = pathFull;

                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw (new Exception("User with this Email dosen`t exist"));
            }

        }
    }
}

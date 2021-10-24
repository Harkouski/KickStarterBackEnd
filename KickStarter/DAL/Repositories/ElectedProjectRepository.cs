using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ElectedProjectRepository : IElectedProjectRepository<ElectedProject>
    {

        private SampleContext db;

        public ElectedProjectRepository(SampleContext context)
        {
            this.db = context;
        }

        public void Add(ElectedProject electedProject)
        {
            var project = db.Projects.FirstOrDefault(p => p.Id == electedProject.ProjectId);
            var check = db.ElectedProjects.FirstOrDefault(p => p.ProjectId == electedProject.ProjectId && p.UserId == electedProject.UserId);
            if (check == null)
            {
                db.ElectedProjects.Add(electedProject);
                db.SaveChanges();
            }
            else
                throw new Exception("You already added this project in elected list");
        }

        public void Delete(int id)
        {
            var check = db.ElectedProjects.FirstOrDefault(p => p.Id == id);
            if (check != null)
            {
                db.Entry(check).State = EntityState.Deleted;
                db.SaveChanges();
            }
            else
                throw (new Exception("Project dosen't exist"));
        }

        public List<ElectedProject> DisplayAll()
        {
            throw new NotImplementedException();
        }

        public ArrayList DisplaySingle(string id)
        {
            var someElectedProject = db.ElectedProjects.Where(p => p.UserId == id).ToList();

            var someElected = new ArrayList();

            someElected.Add(someElectedProject);

            var someproject = db.Projects.ToList();

            someElected.Add(someproject);


            if (someElectedProject != null)
            {
                return someElected;
            }
            else throw (new Exception("Elected project dosn't exist"));
        }
    }
}

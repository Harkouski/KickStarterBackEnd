using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class CommentRepository : ICommentRepository<Comment>
    {
        private SampleContext db;

        public CommentRepository(SampleContext context)
        {
            this.db = context;
        }

        public void Add(Comment comment)
        {
            db.Comments.Add(comment);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var check = db.Comments.FirstOrDefault(p => p.Id == id);
            if (check != null)
            {
                db.Entry(check).State = EntityState.Deleted;
                db.SaveChanges();
            }
            else
                throw (new Exception("Project dosen't exist"));
        }

        public List<Comment> DisplayAll()
        {
            List<Comment> someProjects = new List<Comment>();
            someProjects = db.Comments.ToList();
            if (someProjects != null)
            {
                return someProjects;
            }
            else throw (new Exception("No comments exist"));
        }

        public ArrayList DisplayAllProjectComments(int id)
        {
            var projectComments = db.Comments.Where(p=>p.ProjectId==id).ToList();

            var someHistory = new ArrayList();

            someHistory.Add(projectComments);

            var users = db.Users.ToList();

            someHistory.Add(users);

            if (projectComments != null)
            {
                return someHistory;
            }
            else throw (new Exception("Comment dosn't exist"));
        }

        public Comment Update(Comment comment)
        {
            var oldcomment = db.Comments.AsNoTracking().FirstOrDefault(p => p.Id == comment.Id);
            if (oldcomment != null)
            {
                oldcomment.Content = comment.Content;

                db.Entry(oldcomment).State = EntityState.Modified;
                db.SaveChanges();
                return oldcomment;
            }
            else
            {
                throw (new Exception("User with this Email dosen`t exist"));
            }
        }
    }
}

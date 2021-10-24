using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RatingRepocitory : IRatingRepository<Rating>
    {
        private SampleContext db;

        public RatingRepocitory(SampleContext context)
        {
            this.db = context;
        }

        public void Add(Rating rating)
        {
            var check = db.Ratings.Where(p =>p.ProjectId == rating.ProjectId && p.UserId == rating.UserId).FirstOrDefault();
            if (check == null)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
            }
            else
            {
                Update(rating);
            }
        }

        public float ShowProjectRating(int projectId)
        {
                var count = (float)db.Ratings.Where(p => p.ProjectId == projectId).ToList().Count;
                var ratingsSum = (float)db.Ratings.Where(p => p.ProjectId == projectId).Sum(p => p.Value);
                float rating = ratingsSum / count;
                return rating;
        }

        public void Update(Rating rating)
        {
            var oldProject = db.Ratings.Where(p => p.ProjectId == rating.ProjectId &&  p.UserId == p.UserId).FirstOrDefault();
            if (oldProject != null)
            {
                oldProject.Value = rating.Value;
                db.Entry(oldProject).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw (new Exception("Rating with this Id dosen`t exist"));
            }
        }
    }
}

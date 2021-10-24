using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private SampleContext db;
        public ProjectRepository projectRepository;
        public CommentRepository commentRepository;
        public DonationHistoryRepository donationHistoryRepository;
        public CategoryRepository categoryRepository;
        public RatingRepocitory ratingRepocitory;
        public ElectedProjectRepository electedProjectRepository;
        public IWebHostEnvironment _appEnvironment;

        public UnitOfWork(SampleContext sampleContext, IWebHostEnvironment appEnvironment)
        {
            db = sampleContext;
            _appEnvironment = appEnvironment;
        }
        public IProjectRepository<Project> Projects
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new ProjectRepository(db, _appEnvironment);
                return projectRepository;
            }
        }

        public IElectedProjectRepository<ElectedProject> ElectedProjects
        {
            get
            {
                if (electedProjectRepository == null)
                    electedProjectRepository = new ElectedProjectRepository(db);
                return electedProjectRepository;
            }
        }

        public ICommentRepository<Comment> Comments
        {
            get
            {
                if (commentRepository == null)
                    commentRepository = new CommentRepository(db);
                return commentRepository;
            }
        }

        public IDonationHistoryRepository<DonationHistory> DonationHistories
        {
            get
            {
                if (donationHistoryRepository == null)
                    donationHistoryRepository = new DonationHistoryRepository(db);
                return donationHistoryRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(db);
                return categoryRepository;
            }
        }

        public IRatingRepository<Rating> Ratings
        {
            get
            {
                if (ratingRepocitory == null)
                    ratingRepocitory = new RatingRepocitory(db);
                return ratingRepocitory;

            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}

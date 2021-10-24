using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IProjectRepository<Project> Projects { get; }

        ICommentRepository<Comment> Comments { get; }

        IRepository<Category> Categories { get; }

        IDonationHistoryRepository<DonationHistory> DonationHistories { get; }

        IRatingRepository<Rating> Ratings { get; }

        IElectedProjectRepository<ElectedProject> ElectedProjects { get; }

        void Save();
    }
}

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
    public class DonationHistoryRepository : IDonationHistoryRepository<DonationHistory>
    {
        private SampleContext db;

        public DonationHistoryRepository(SampleContext context)
        {
            this.db = context;
        }
        public void Add(DonationHistory donation)
        {
            var project = db.Projects.FirstOrDefault(p => p.Id == donation.ProjectId);
            project.CurrentDonation += donation.Donation;
            donation.Date = DateTime.Now;
            if (project != null)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
            }
            db.DonationHistories.Add(donation);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var check = db.DonationHistories.FirstOrDefault(p => p.Id == id);
            if (check != null)
            {
                db.Entry(check).State = EntityState.Deleted;
                db.SaveChanges();
            }
            else
                throw (new Exception("Project dosen't exist"));
        }

        public List<DonationHistory> DisplayAll()
        {

            var someDonationHistories = new List<DonationHistory>();
            someDonationHistories = db.DonationHistories.ToList();
            if (someDonationHistories != null)
            {
                return someDonationHistories;
            }
            else throw (new Exception("No donation histories exist"));
        }

        public ArrayList DisplaySingle(string id)
        {
            
            var someDonationHistory = db.DonationHistories.Where(p => p.UserId == id).ToList();

            var someHistory = new ArrayList();

            someHistory.Add(someDonationHistory);

            var someproject = db.Projects.ToList();

            someHistory.Add(someproject);           

            if (someDonationHistory != null)
            {
                return someHistory;
            }
            else throw (new Exception("Donation distory dosn't exist"));
        }

        public DonationHistory Update(DonationHistory donation)
        {
            throw new NotImplementedException();
        }
    }
}

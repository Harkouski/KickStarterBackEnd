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
    public class DonationHistoryService : IDonationHistoryService<DonationHistoryDTO>
    {
        IUnitOfWork _unitOfWork;
        public DonationHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(DonationHistoryDTO donationHistoryDTO)
        {
            var configDonationHistory = new MapperConfiguration(cfg => cfg.CreateMap<DonationHistoryDTO, DonationHistory>());

            var mapperDonationHistory = new Mapper(configDonationHistory);

            var donationHistory = mapperDonationHistory.Map<DonationHistory>(donationHistoryDTO);

            _unitOfWork.DonationHistories.Add(donationHistory);
        }

        public void Delete(int id)
        {
            _unitOfWork.DonationHistories.Delete(id);
        }

        public List<DonationHistoryDTO> DisplayAll()
        {
            var donationHistory = _unitOfWork.DonationHistories.DisplayAll();
            var donationHistoryDTO = new List<DonationHistoryDTO> { };
            foreach (var prop in donationHistory)
            {
                var configDonationHistory = new MapperConfiguration(cfg => cfg.CreateMap<DonationHistory, DonationHistoryDTO>());

                var mapperDonationHistory = new Mapper(configDonationHistory);

                donationHistoryDTO.Add(mapperDonationHistory.Map<DonationHistoryDTO>(prop));

            }
            return donationHistoryDTO;
        }

        public List<UserDonations> DisplaySingle(string id)
        {
           var donationHistory = _unitOfWork.DonationHistories.DisplaySingle(id);

            List<DonationHistory> DonationHistories = (List<DonationHistory>)donationHistory[0];
            List<Project> prod = (List<Project>)donationHistory[1];

            List<UserDonations> donationHistoryForShow = new List<UserDonations>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<DonationHistory, UserDonations>());
            var mapper = new Mapper(config);


            foreach (var DonationHistory in DonationHistories)
            {
                var someDonationHistoryForShow = mapper.Map<UserDonations>(DonationHistory);
                someDonationHistoryForShow.ProjectName = prod.Where(x => x.Id == DonationHistory.ProjectId).First().Name;
                someDonationHistoryForShow.Date = new DateTime(someDonationHistoryForShow.Date.Year, someDonationHistoryForShow.Date.Month,
                    someDonationHistoryForShow.Date.Day, someDonationHistoryForShow.Date.Hour,
                    someDonationHistoryForShow.Date.Minute, someDonationHistoryForShow.Date.Second);
                donationHistoryForShow.Add(someDonationHistoryForShow);
            }

            return donationHistoryForShow;
        }

       

        public DonationHistoryDTO Update(DonationHistoryDTO T)
        {
            throw new NotImplementedException();
        }
    }
}

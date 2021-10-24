using AutoMapper;
using BLL.DTO;
using BLL.Models;
using DAL.Context;
using DAL.Entities;
using System.Collections.Generic;

namespace BLL.ConvertModel
{
    public class ConvertModel
    {
        public static User ConvertUserDtoToUser(UserDTO userDTO, SampleContext sampleContext)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>());

            var mapper = new Mapper(config);

            var user = mapper.Map<User>(userDTO);

            return user;
        }

        public static UserDTO ConvertUserUpdateModelToUserDTO(UpdateUserModel updateUserModel)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateUserModel, UserDTO>());

            var mapper = new Mapper(config);

            var user = mapper.Map<UserDTO>(updateUserModel);

            return user;
        }

        public static ReturnUserModel ConvertUserToUserReturnModel(User User)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, ReturnUserModel>());

            var mapper = new Mapper(config);

            var user = mapper.Map<ReturnUserModel>(User);

            return user;
        }

        public static UserDTO ConvertUserDeleteModelToUserDTO(DeleteUserModel deleteUserModel)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<DeleteUserModel, UserDTO>());

            var mapper = new Mapper(config);

            var user = mapper.Map<UserDTO>(deleteUserModel);

            return user;
        }

        public static List<UserDonations> ConvertDonationHistoryDTOToUserDonatiion(List<DonationHistoryDTO> donationHistoryDTO)
        {

            var userDonations = new List<UserDonations> { };
            foreach (var prop in donationHistoryDTO)
            {
                var configDonationHistory = new MapperConfiguration(cfg => cfg.CreateMap<UserDonations, DonationHistoryDTO>());

                var mapperDonationHistory = new Mapper(configDonationHistory);

                var someUserDonation = mapperDonationHistory.Map<UserDonations>(prop);

                userDonations.Add(someUserDonation);         
            }
            return userDonations;
        }
    }
}

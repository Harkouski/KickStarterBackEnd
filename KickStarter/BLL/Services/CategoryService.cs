using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : IService<CategoryDTO>
    {
        IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(CategoryDTO categoryDTO)
        {
            var configCategory = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, Category>());

            var mapperCategory = new Mapper(configCategory);

            var category = mapperCategory.Map<Category>(categoryDTO);

            _unitOfWork.Categories.Add(category);
        }

        public void Delete(int id)
        {
            _unitOfWork.Categories.Delete(id);
        }

        public List<CategoryDTO> DisplayAll()
        {
            var category = _unitOfWork.Categories.DisplayAll();

            var categoryDTO = new List<CategoryDTO> { };

            foreach (var prop in category)
            {
                var configProject = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>());

                var mapperProject = new Mapper(configProject);

                categoryDTO.Add(mapperProject.Map<CategoryDTO>(prop));
            }

            return categoryDTO;
        }

        public CategoryDTO DisplaySingle(int id)
        {
            var category = _unitOfWork.Categories.DisplaySingle(id);

            var configCategory = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>());

            var mapperCategory = new Mapper(configCategory);

            var categoryDTO = mapperCategory.Map<CategoryDTO>(category);

            return categoryDTO;
        }

        public CategoryDTO Update(CategoryDTO categoryDTO)
        {
            var configCategory = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, Category>());

            var mapperCategory = new Mapper(configCategory);

            var category = mapperCategory.Map<Category>(categoryDTO);

            category = _unitOfWork.Categories.Update(category);

            configCategory = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>());

            mapperCategory = new Mapper(configCategory);

            categoryDTO = mapperCategory.Map<CategoryDTO>(category);

            return categoryDTO;
        }
    }
}

﻿using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Controllers
{
    // https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository) 
        {
            this.categoryRepository = categoryRepository;
        }   

        
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            // Map DTO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await categoryRepository.CreateAsync(category);

            // Domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        //GET: https://localhost:7196/api/Categories 
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            // Map Domain Model to DTO

            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto 
                { 
                    Id = category.Id, 
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }

            return Ok(response);
        }

        //Get https://localhost:7196/api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await categoryRepository.GetById(id);

            if (existingCategory is null)
            {
                return NotFound();
            }

            var response = new CategoryDto 
            { 
                Id = existingCategory.Id, 
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };

            return Ok(response);
        }

        // PUT: https://localhost:7196/api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryRequestDto request)
        {
            // Convedrt DTO to Domain Model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            category = await categoryRepository.UpdateAsync(category);

            if (category == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }
    }
}

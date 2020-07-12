using Microsoft.AspNetCore.Mvc;
using PepegaFoodServer.Contracts;
using PepegaFoodServer.Contracts.Requests;
using PepegaFoodServer.Models;
using PepegaFoodServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpPost(ApiRoutes.Products.AddProducts)]
        public async Task<IActionResult> AddProducts([FromBody] AddNewProductRequest[] request)
        {
            if (request == null)
            {
                return BadRequest("Request model is not correct");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage)));
            }

            var response = await _productsService.AddProducts(request);

            if (!response.Success)
            {
                return BadRequest(response.ErrorsMessages);
            }

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Products.GetNumOfAllPages)]
        public async Task<IActionResult> GetNumOfAllPages()
        {
            var response = await _productsService.GetNumOfAllPages();

            if (response < 0)
            {
                return BadRequest("Невозможно получить количество страниц");
            }

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Products.GetProducts)]
        public async Task<IActionResult> GetProducts(int pageSize)
        {
            var response = await _productsService.GetProducts(pageSize);

            if (response == null || response.Count() == 0)
            {
                return BadRequest("Невозможно получить список продуктов");
            }

            return Ok(response);

        }

        [HttpGet(ApiRoutes.Products.GetNumOfPagesForCategory)]
        public async Task<IActionResult> GetNumOfPagesForCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return BadRequest("Пустой запрос");
            }

            var response = await _productsService.GetNumOfPagesForCategory(categoryName);

            if (response < 0)
            {
                return BadRequest("Невозможно получить количество страниц");
            }

            return Ok(response);

        }

        [HttpGet(ApiRoutes.Products.GetProductsByCategory)]
        public async Task<IActionResult> GetProductsByCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return BadRequest("Пустой запрос");
            }

            var response = await _productsService.GetProductsByCategory(categoryName);

            if (response == null || response.Count() == 0)
            {
                return BadRequest("Невозможно получить список продуктов");
            }

            return Ok(response);

        }

        [HttpDelete(ApiRoutes.Products.RemoveProducts)]
        public async Task<IActionResult> RemoveProducts([FromBody] RemoveProductsRequest request)
        {
            if (request == null)
            {
                return BadRequest("Пустой запрос");
            }

            var response = await _productsService.RemoveProducts(request.ProductsId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorsMessages);
            }

            return Ok(response);
        }

        [HttpPut(ApiRoutes.Products.UpdateProduct)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductModel request)
        {
            if (request == null)
            {
                return BadRequest("Пустой запрос");
            }

            var response = await _productsService.UpdateProduct(request);

            if (!response.Success)
            {
                return BadRequest(response.ErrorsMessages);
            }

            return Ok(response);

        }

        [HttpGet(ApiRoutes.Products.GetProductByName)]
        public async Task<IActionResult> GetProductByName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return BadRequest("Пустой запрос");
            }

            var response = await _productsService.GetProductByName(productName);
            
            if(response == null)
            {
                return BadRequest("Невозомжно получить объект");
            }

            return Ok(response);

        }

        [HttpGet(ApiRoutes.Products.GetProductById)]
        public async Task<IActionResult> GetProductById(int productId)
        {
            if(productId<0)
            {
                return BadRequest("Id не может быть меньше нуля");
            }

            var response = await _productsService.GetProductById(productId);

            if (response == null)
            {
                return BadRequest("Невозомжно получить объект");
            }

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Products.GetCategories)]
        public async Task<IActionResult> GetCategories()
        {

            var response = await _productsService.GetCategories();

            if (response == null)
            {
                return BadRequest("Невозомжно получить объект");
            }

            return Ok(response);
        }


    }
}

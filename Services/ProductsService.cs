using PepegaFoodServer.Contracts.Requests;
using PepegaFoodServer.Models;
using PepegaFoodServer.Models.DbModels;
using PepegaFoodServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Services
{
    public class ProductsService : IProductsService
    {
        public ProductsService()
        {

        }

        public Task<ChangedInformationResultModel> AddProducts(AddNewProductRequest[] products)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryDBModel>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNumOfAllPages()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNumOfPagesForCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> GetProductByName(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductModel>> GetProducts(int pageSize = 0)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductModel>> GetProductsByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<ChangedInformationResultModel> RemoveProducts(int[] productsId)
        {
            throw new NotImplementedException();
        }

        public Task<ChangedInformationResultModel> UpdateProduct(ProductModel product)
        {
            throw new NotImplementedException();
        }
    }
}

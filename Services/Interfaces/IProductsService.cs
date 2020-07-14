using PepegaFoodServer.Contracts.Requests;
using PepegaFoodServer.Models;
using PepegaFoodServer.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Services.Interfaces
{
    public interface IProductsService
    {
        // добавить коллекцию продуктов
        public Task<ChangedInformationResultModel> AddProducts(AddNewProductRequest[] products);

        // получить количество страниц для всех продуктов
        public int GetNumOfAllPages(int numProductsOnPage);

        // получить коллекцию продуктов
        public Task<IEnumerable<ProductModel>> GetProducts(int pageSize = 0);

        // получить количество страниц для продуктов указанной категории
        public Task<int> GetNumOfPagesWithProductsForCategory(string categoryName, int numProductsOnPage);

        // получить коллекцию продуктов по указанной категории
        public Task<IEnumerable<ProductModel>> GetProductsByCategory(string categoryName);

        // удалить коллекцию продуктов
        public Task<ChangedInformationResultModel> RemoveProducts(int[] productsId);

        // обновить продукт
        public Task<ChangedInformationResultModel> UpdateProduct(UpdateProductRequest product);

        // Найти продукт по имени
        public Task<ProductModel> GetProductByName(string productName);

        //Найти продукт по айди
        public Task<ProductModel> GetProductById(int productId);

        // Получить список категорий
        public Task<IEnumerable<CategoryDBModel>> GetCategories();
    }
}

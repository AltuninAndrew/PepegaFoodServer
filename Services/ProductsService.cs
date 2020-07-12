using Microsoft.EntityFrameworkCore;
using MimeKit.Encodings;
using Org.BouncyCastle.Math.EC.Rfc7748;
using PepegaFoodServer.Contracts.Requests;
using PepegaFoodServer.Data;
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
        private readonly DataContext _dataContext;

        public ProductsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ChangedInformationResultModel> AddProducts(AddNewProductRequest[] products)
        {
            int addedCount = 0;
            foreach (var product in products)
            {
                var existProduct = await _dataContext.Products.FirstOrDefaultAsync(x => x.Name == product.Name);

                if (existProduct == null)
                {

                    var dbModel = new ProductDBModel
                    {
                        Name = product.Name,
                        Count = product.Count,
                        PrimaryPrice = product.PrimaryPrice,
                        SecondaryPrice = product.SecondaryPrice,
                    };

                    var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Name == product.Category);

                    if (category == null)
                    {
                        category = (await _dataContext.Categories.AddAsync(new CategoryDBModel { Name = product.Category })).Entity;
                        await _dataContext.SaveChangesAsync();
                    }

                    dbModel.CategoryId = category.CategoryID;

                    var countType = await _dataContext.CountTypes.FirstOrDefaultAsync(x => x.Name == product.CountType);

                    if (countType == null)
                    {
                        countType = (await _dataContext.CountTypes.AddAsync(new CountTypeDBModel { Name = product.CountType })).Entity;
                        await _dataContext.SaveChangesAsync();
                    }

                    dbModel.CounteTypeId = countType.CountTypeId;

                    var productImage = await _dataContext.ProductImages.FirstOrDefaultAsync(x => x.ImageUrl == product.ImageProductUrl);

                    if (productImage == null)
                    {
                        productImage = (await _dataContext.ProductImages.AddAsync(new ImageProductDBModel { ImageUrl = product.ImageProductUrl })).Entity;
                        await _dataContext.SaveChangesAsync();
                    }

                    dbModel.ImageId = productImage.ImageId;

              
                    dbModel = (await _dataContext.Products.AddAsync(dbModel)).Entity;
                    await _dataContext.SaveChangesAsync();

                    foreach (var shop in product.Shops)
                    {
                        
                        var shopDB = await _dataContext.Shops.FirstOrDefaultAsync(x => x.Name == shop);

                        if (shopDB == null)
                        {
                            shopDB = (await _dataContext.AddAsync(new ShopDBModel { Name = shop })).Entity;
                            await _dataContext.SaveChangesAsync();
                        }
                        await _dataContext.ProductsToShops.AddAsync(new ProductToShopDBModel { ProductId = dbModel.ProductId, ShopId = shopDB.ShopId });
                        await _dataContext.SaveChangesAsync();
                    }

                    await _dataContext.SaveChangesAsync();            
                    addedCount++;
                }
            }

            if(addedCount>0)
            {
                return new ChangedInformationResultModel { Success = true };
            }
            else
            {
                return new ChangedInformationResultModel { Success = false, ErrorsMessages = new[] { "Продукты не добавлены"} };
            }

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

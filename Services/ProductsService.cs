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

        public async Task<IEnumerable<CategoryDBModel>> GetCategories()
        {
            var result = await _dataContext.Categories?.ToArrayAsync();
            
            return result;
        }

        public int GetNumOfAllPages(int numProductsOnPage)
        {
            var productsCount = _dataContext.Products?.Count();

            var result = (numProductsOnPage <=0)
                ? productsCount ?? 0
                : (int)Math.Ceiling((float)productsCount / (float)numProductsOnPage);

            return result;
        }

        public async Task<int> GetNumOfPagesWithProductsForCategory(string categoryName, int numProductsOnPage)
        {
            var categoryId = (await _dataContext.Categories.FirstOrDefaultAsync(x=>x.Name == categoryName)).CategoryID;

            var productsCount = _dataContext.Products.Where(x => x.CategoryId == categoryId)?.Count();

            var result = (numProductsOnPage <= 0)
                ? productsCount ?? 0
                : (int)MathF.Ceiling((float)productsCount / (float)numProductsOnPage);

            return result;

        }

        public async Task<ProductModel> GetProductById(int productId)
        {
            var product = await _dataContext.Products
                .Include(x => x.Category)
                .Include(x => x.ImageProductUrl)
                .Include(x => x.CountType)
                .FirstOrDefaultAsync(x => x.ProductId == productId);
            
            var productModel = (product != null) ? new ProductModel
            {
                Category = product.Category?.Name,
                Count = product.Count,
                CountType = product.CountType?.Name,
                ImageProductUrl = product.ImageProductUrl?.ImageUrl,
                Name = product.Name,
                PrimaryPrice = product.PrimaryPrice,
                ProductId = productId,
                SecondaryPrice = product.SecondaryPrice,
            } : null;

            return productModel;
        }

        public async Task<ProductModel> GetProductByName(string productName)
        {
            var product = await _dataContext.Products
              .Include(x => x.Category)
              .Include(x => x.ImageProductUrl)
              .Include(x => x.CountType)
              .FirstOrDefaultAsync(x => x.Name == productName);

            var productModel = (product != null) ? new ProductModel
            {
                Category = product.Category?.Name,
                Count = product.Count,
                CountType = product.CountType?.Name,
                ImageProductUrl = product.ImageProductUrl?.ImageUrl,
                Name = product.Name,
                PrimaryPrice = product.PrimaryPrice,
                ProductId = product.ProductId,
                SecondaryPrice = product.SecondaryPrice,
            } : null;

            return productModel;
        }

        public Task<IEnumerable<ProductModel>> GetProducts(int pageSize = 0)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductModel>> GetProductsByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public async Task<ChangedInformationResultModel> RemoveProducts(int[] productsId)
        {
            List<ProductDBModel> productDBModels = new List<ProductDBModel>();
            foreach(var productId in productsId)
            {
                var existProduct = await _dataContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

                if(existProduct != null)
                {
                    productDBModels.Add(existProduct);
                }
            }

            if(productDBModels.Count >0)
            {
                _dataContext.Products.RemoveRange(productDBModels);
                await _dataContext.SaveChangesAsync();

                if(productsId.Length == productDBModels.Count)
                {
                    return new ChangedInformationResultModel { Success = true };
                }
                else
                {
                    return new ChangedInformationResultModel { Success = true, ErrorsMessages = new[] {"Некоторых продуктов не существует"} };
                }
                
            }
            else
            {
                return new ChangedInformationResultModel { Success = false };
            }


        }

        public async Task<ChangedInformationResultModel> UpdateProduct(UpdateProductRequest product)
        {
            var existProduct = await _dataContext.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId);

            if(existProduct == null)
            {
                return new ChangedInformationResultModel { Success = false, ErrorsMessages = new[] { "Продукт не найден" } };
            }

            if(product.NewCategory != null)
            {

            }

            if(product.NewCount != null)
            {

            }

            if(product.NewCountType != null)
            {

            }

            if(product.NewImageProductUrl !=null)
            {

            }   
            
            if(product.NewName != null)
            {

            }

            if(product.NewPrimaryPrice != null)
            {

            }

            if(product.NewSecondaryPrice != null)
            {

            }
            
        }
    }
}

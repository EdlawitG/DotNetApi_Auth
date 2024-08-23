using System.Collections;
using authApi.Dtos;
using authApi.Models;
using authApi.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace authApi.Service
{
    public class ProductService(ProductRepository _productrepository)
    {
        public async Task<Product> GetProductAsync(Guid id)
        {
            var p = await _productrepository.GetProductById(id);
            return p;
        }
        public async Task<IEnumerable> GetProductsAsync() => await _productrepository.GetAllProduct();
        public async Task<Product> CreateProductAsync(CreateProRequest request)
        {
            var pro = await _productrepository.CreateProduct(request);
            return pro;
        }
        public async Task<Product> UpdateProductAsync(Guid id, UpdateProRequest request)
        {

            var pro = await _productrepository.UpdateProduct(id, request);
            return pro;
        }
        public async Task DeleteProductAsync(Guid id)
        {
            await _productrepository.DeleteProduct(id);
        }
        public async Task<IEnumerable<Product>> SearchProductAsync(string searchTerm)
        {
            var p = await _productrepository.SearchProduct(searchTerm);
            return p;
        }
        public async Task<IEnumerable<Product>> FilterbyCategoryAsync(string category)
        {
            var p = await _productrepository.FilterbyCategory(category);
            return p;
        }
    }
}
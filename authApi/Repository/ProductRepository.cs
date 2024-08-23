using authApi.Data;
using authApi.Dtos;
using authApi.Interface;
using authApi.Models;
using authApi.Service;
using AutoMapper;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace authApi.Repository
{
    public class ProductRepository(ApplicationDbContext context, IMapper mapper, ILogger<ProductRepository> logger, PhotoService photoService) : IProduct
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<ProductRepository> _logger = logger;
        private readonly PhotoService _photoService = photoService;
        public async Task<Product> CreateProduct(CreateProRequest request)
        {
            try
            {
                if (request.ImageUrl == null || request.ImageUrl.Length == 0)
                {
                    throw new ArgumentException("No file provided for upload.");
                }
                var photoResult = await _photoService.AddPhotoAsync(request.ImageUrl);
                var products = _mapper.Map<Product>(request);
                products.ImageUrl = photoResult.PhotoUrl;
                _context.Products.Add(products);
                await _context.SaveChangesAsync();
                return products;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while creating user.");
                throw new Exception("An error occurred while creating user.");
            }
        }
        public async Task<List<Product>> GetAllProduct()
        {
            var products = await _context.Products.ToListAsync() ?? throw new Exception(" No Todo items found");
            return products;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var product = await _context.Products.FindAsync(id) ?? throw new KeyNotFoundException($"No Product item with Id {id} found.");
            return product;
        }

        public async Task<Product> UpdateProduct(Guid id, UpdateProRequest request)
        {
            var product = _mapper.Map<Product>(request);
            try
            {
                var product1 = await _context.Products.FindAsync(id) ?? throw new KeyNotFoundException($"No Product item with Id {id} found.");
                if (product.ProductName != null)
                {
                    product1.ProductName = product.ProductName;
                }
                if (product.Description != null)
                {
                    product1.Description = product.Description;
                }
                if (product1.ImageUrl != null)
                {
                    product1.ImageUrl = product.ImageUrl;
                }
                if (product.Category != null)
                {
                    product1.Category = product.Category;
                }
                if (product1.Price != null)
                {
                    product1.Price = product.Price;
                }

                _context.Update(product1);
                await _context.SaveChangesAsync();
                return product1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Todo item with Id: {Id}.", id);
                throw;
            }
        }
        public async Task DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var publicId = ExtractPublicIdFromUrl(product.ImageUrl);
                    await _photoService.DeletePhotoAsync(publicId);
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No Product item with Id {id} found.");

            }
        }
        private string ExtractPublicIdFromUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                var segments = uri.AbsolutePath.Trim('/').Split('/');
                return segments.Last();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while extracting the public ID from URL: {Url}.", url);
                throw new ArgumentException("Invalid URL format.", nameof(url));
            }
        }

        public async Task<IEnumerable<Product>> SearchProduct(string searchTerm)
        {
            var normalizedSearchTerm = searchTerm.ToUpper();
            var results = await _context.Products
                .Where(t => t.ProductName.ToUpper().Contains(normalizedSearchTerm))
                .ToListAsync();
            return results;
        }
        public async Task<IEnumerable<Product>> FilterbyCategory(string category)
        {
            return await _context.Products.Where(p => p.Category == category).ToListAsync();

        }
       
       
        Task<string> IProduct.CreateProduct(CreateProRequest request)
        {
            throw new NotImplementedException();
        }

        Task<string> IProduct.GetAllProduct()
        {
            throw new NotImplementedException();
        }

        Task<string> IProduct.GetProductById(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<string> IProduct.UpdateProduct(Guid id, UpdateProRequest request)
        {
            throw new NotImplementedException();
        }

        Task<string> IProduct.DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
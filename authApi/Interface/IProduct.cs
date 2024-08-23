using authApi.Dtos;

namespace authApi.Interface
{
    public interface IProduct
    {
        Task<string> CreateProduct(CreateProRequest request);
        Task<string> GetAllProduct();
        Task<string> GetProductById(Guid id);
        Task<string> UpdateProduct(Guid id, UpdateProRequest request);
        Task<string> DeleteProduct(Guid id);
    }
}
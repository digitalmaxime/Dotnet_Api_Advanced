namespace Containerized_Api_K8s;

public interface IProductService
{
    public Task<List<ProductDetails>> ProductListAsync();

    public Task<ProductDetails> GetProductDetailByIdAsync(int productId);

    public Task<bool> AddProductAsync(ProductDetails productDetails);

    public Task<bool> UpdateProductAsync(ProductDetails productDetails);

    public Task<bool> DeleteProductAsync(int productId);
}
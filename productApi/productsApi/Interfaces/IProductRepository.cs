using productsApi.Domains;

namespace productsApi.Interfaces
{
    public interface IProductRepository
    {
        public List<ProductsDomain> Get();
        public ProductsDomain GetById(Guid id);
        public void Register(ProductsDomain product);
        public void Update(Guid id, ProductsDomain product);
        public void Delete(Guid id);
    }
}

using productsApi.Domains;
using productsApi.Interfaces;
using productsApi.Context;
using static productsApi.Repositories.ProductRepository;

namespace productsApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
       
        private readonly ProductContext ctx;

        public ProductRepository()
        {
            ctx = new ProductContext();
        }

        public List<ProductsDomain> Get()
        {
            return ctx.ProdutosContext.ToList();
        }

        public ProductsDomain GetById(Guid id)
        {
            try
            {
                return ctx.ProdutosContext.FirstOrDefault(x => x.IdProduct == id)!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Register(ProductsDomain product)
        {
            try
            {
                ctx.ProdutosContext.Add(product);
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Guid id, ProductsDomain product)
        {
            try
            {
                ProductsDomain produtoBuscado = ctx.ProdutosContext.FirstOrDefault(x => x.IdProduct == id)!;
                ctx.Update(produtoBuscado);
                ctx.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(Guid id)
        {
            ctx.ProdutosContext.Remove(ctx.ProdutosContext.FirstOrDefault(y => y.IdProduct == id)!);
        }
    }
    }


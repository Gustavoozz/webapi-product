using Microsoft.EntityFrameworkCore;
using productsApi.Domains;

namespace productsApi.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductsDomain> ProdutosContext { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server = NOTE12-SALA21\\SQLEXPRESS; DataBase = TecschoolBD; TrustServerCertificate = true; Integrated Security=True;");
            optionsBuilder.UseSqlServer("Server=NOTE12-SALA21\\SQLEXPRESS; DataBase=Products_Database; User Id=sa; Pwd=Senai@134; TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}

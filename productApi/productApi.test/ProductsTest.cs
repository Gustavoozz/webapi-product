using Moq;
using productsApi.Domains;
using productsApi.Interfaces;
using productsApi.Repositories;
using Xunit;

namespace productApi.test
{
    public class ProductsTest
    {
        /// <summary>
        /// Teste para a funcionalidade de listar todos os produtos:
        /// </summary>
        [Fact]
        public void Get()
        {
            // Arrange ( Organização ):
            // Lista de produtos:
            List<ProductsDomain> productsList = new List<ProductsDomain>
            {
                new ProductsDomain { IdProduct = Guid.NewGuid(), Nome = "Produto 1", Price = 10 },
                new ProductsDomain { IdProduct = Guid.NewGuid(), Nome = "Produto 2", Price = 90 },
                new ProductsDomain { IdProduct = Guid.NewGuid(), Nome = "Produto 3", Price = 30 }
            };

            // Cria um objeto de simulação do tipo ProductRepository:
            var mockRepository = new Mock<IProductRepository>();

            // Configura o método para que quando for acionado retorne a lista mockada ( productsList ):
            mockRepository.Setup(x => x.Get()).Returns(productsList);


            // Act ( Agir ):
            // Executando o método "Get" e atribue a resposta em result:
            var result = mockRepository.Object.Get();


            // Assert ( Provar ):
            // Valor esperado, variavel result:
            Assert.Equal(3, result.Count);
        }


        [Fact]
        public void Post()
        {
            ProductsDomain produto = new ProductsDomain { IdProduct = Guid.NewGuid(), Nome = "Desgraca", Price = 10 };

            List<ProductsDomain> listaDeProdutos = new List<ProductsDomain>();

            // Cria um objeto de simulação do tipo ProductRepository:
            var mockRepository = new Mock<IProductRepository>();

            // Configura o método para que quando for acionado retorne a lista mockada ( productsList ):
            mockRepository.Setup(x => x.Register(It.IsAny<ProductsDomain>())).Callback<ProductsDomain>(x => listaDeProdutos.Add(x));

            // Act ( Agir ):
            // Executando o método "Get" e atribue a resposta em result:
            mockRepository.Object.Register(produto);


            // Assert ( Provar ):
            // Valor esperado, variavel result:
            Assert.Contains(produto, listaDeProdutos);
        }


        [Fact]
        public void GetById()
        {
            // Arrange ( Organização ):
            var productId = Guid.NewGuid();
            // Lista de produtos:
            List<ProductsDomain> productsList = new List<ProductsDomain>
            {
                new ProductsDomain { IdProduct = productId, Nome = "Produto 1", Price = 10 },
                new ProductsDomain { IdProduct = Guid.NewGuid(), Nome = "Produto 2", Price = 90 },
                new ProductsDomain { IdProduct = Guid.NewGuid(), Nome = "Produto 3", Price = 30 }
            };

            // Cria um objeto de simulação do tipo ProductRepository:
            var mockRepository = new Mock<IProductRepository>();

            // Configura o método para que quando for acionado retorne a lista mockada ( productsList ):
            mockRepository.Setup(x => x.GetById(productId)).Returns(productsList.FirstOrDefault(x => x.IdProduct == productId));


            // Act ( Agir ):
            // Executando o método "Get" e atribue a resposta em result:
            var result = mockRepository.Object.GetById(productId);


            // Assert ( Provar ):
            // Valor esperado, variavel result:
            Assert.NotNull(result);
            Assert.Equal("Produto 1", result.Nome);
        }


        [Fact]
        public void Remove()
        {
            ProductsDomain produto = new ProductsDomain { IdProduct = Guid.NewGuid(), Nome = "Desgraca", Price = 10 };

            List<ProductsDomain> listaDeProdutos = new List<ProductsDomain>();

            var mockRepository = new Mock<IProductRepository>();

            mockRepository.Setup(x => x.Register(It.IsAny<ProductsDomain>())).Callback<ProductsDomain>(x => listaDeProdutos.Add(x));
       
            mockRepository.Setup(x => x.Delete(It.IsAny<Guid>())).Callback<Guid>(id =>
            {
                var productToRemove = listaDeProdutos.Find(p => p.IdProduct == id);
                if (productToRemove != null)
                {
                    listaDeProdutos.Remove(productToRemove);
                }
            });
           
            mockRepository.Object.Register(produto);
           
            mockRepository.Object.Delete(produto.IdProduct);

            Assert.DoesNotContain(produto, listaDeProdutos);
        }




        [Fact]
        public void Atualizar()
        {
            var initialProduct = new ProductsDomain
            {
                IdProduct = Guid.NewGuid(),
                Nome = "Desgraca",
                Price = 10
            };

            var updatedProduct = new ProductsDomain
            {
                IdProduct = initialProduct.IdProduct,
                Nome = "UpdatedName",
                Price = 20
            };

            var listaDeProdutos = new List<ProductsDomain>();

            var mockRepository = new Mock<IProductRepository>();

        
            mockRepository.Setup(x => x.Register(It.IsAny<ProductsDomain>())).Callback<ProductsDomain>(x => listaDeProdutos.Add(x));

            mockRepository.Setup(x => x.Update(It.IsAny<ProductsDomain>())).Callback<ProductsDomain>(productToUpdate =>
            {
                var existingProduct = listaDeProdutos.Find(p => p.IdProduct == productToUpdate.IdProduct);
                if (existingProduct != null)
                {
                    existingProduct.Nome = productToUpdate.Nome;
                    existingProduct.Price = productToUpdate.Price;
                }
            });

            // Adiciona o produto inicial à lista
            mockRepository.Object.Register(initialProduct);


            // Act
            // Executa o método Update para modificar o produto existente
            mockRepository.Object.Update(updatedProduct);

            // Assert
            // Verifica se o produto foi atualizado na lista
            var productInList = listaDeProdutos.Find(p => p.IdProduct == updatedProduct.IdProduct);
 
            Assert.Equal(updatedProduct.Nome, productInList!.Nome);
            Assert.Equal(updatedProduct.Price, productInList.Price);
        }

    }
}


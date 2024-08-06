using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using productsApi.Domains;
using productsApi.Interfaces;
using productsApi.Repositories;
using productsApi.ViewModels;

namespace productsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private  IProductRepository? _productRepository;

        public ProductController(IProductRepository product)
        {
            _productRepository = product;
        }

        [HttpGet("GetAll")]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_productRepository?.Get());
            }
            catch (Exception e)
            {

               return BadRequest(e.Message);
            }
        }




        [HttpPost("Register")]
        public IActionResult Cadastrar(ProductsDomain products)
        {
            try
            {
                _productRepository!.Register(products);
                return Ok(products);    
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }



        [HttpGet("GetById")]
        public IActionResult BuscarPorId(Guid Id)
        {
            try
            {
                return Ok(_productRepository?.GetById(Id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }



        [HttpDelete("Delete")]
        public IActionResult Deletar(Guid Id)
        {
            try
            {
                _productRepository!.Delete(Id);
                return Ok("Produto deletado com exito!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("Update")]
        public IActionResult Atualizar(Guid Id, ProductsDomain products)
        {
            try
            {
                _productRepository!.Update(Id, products);
                return Ok("Atividade atualizada com sucesso");
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}


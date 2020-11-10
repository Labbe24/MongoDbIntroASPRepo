using MongoDbIntroASP.Models;
using MongoDbIntroASP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MongoDbIntroASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService bookService)
        {
            _productService = bookService;
        }

        [HttpGet]
        public ActionResult<List<ProductCatalog>> Get() =>
            _productService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<ProductCatalog> Get(string id)
        {
            var book = _productService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<ProductCatalog> Create(ProductCatalog book)
        {
            _productService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, ProductCatalog bookIn)
        {
            var book = _productService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _productService.Update(id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _productService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _productService.Remove(book.Id);

            return NoContent();
        }
    }
}
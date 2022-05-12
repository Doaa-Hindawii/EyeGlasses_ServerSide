using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL.DTO;
using System;
using BL.AppServices;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace EyeGlasses_ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        Product_AppService _product;
        ShoppingCart_AppService _shoppingcart;
        public ProductController(Product_AppService product,
            ShoppingCart_AppService shoppingcart)
        {
            this._product = product;
            this._shoppingcart = shoppingcart;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_product.GetAllProduct());
        }

        [HttpGet("{id:int}", Name = "ProductDetails")]
        public IActionResult GetProductById(int id)
        {
            return Ok(_product.GetProduct(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Product_DTO product_DTO)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _product.SaveNewProduct(product_DTO);

                return Created("ProductDetails", product_DTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, Product_DTO product_DTO)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _product.UpdateProduct(product_DTO);
                return Ok(product_DTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _product.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tea.Application;
using Tea.Domain.Models;

namespace Tea_Commerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/<ProductController>
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var result = await _productService.GetAllAsync();
        return Ok(result);
    }

    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var result = await _productService.GetAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // POST api/<ProductController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Product product)
    {
        var result = await _productService.AddAsync(product);

        return Ok(result);
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Product product)
    {
        var result = await _productService.UpdateAsync(product);

        return Ok(result);
    }


    [HttpPatch]
    public ActionResult Add([FromBody] Product product)
    {
        var result = _productService.AddAsync(product);
        return Ok(result);
    }


    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var product = await _productService.GetAsync(id);
        product.Deleted = true;
        var result = await _productService.UpdateAsync(product);

        return Ok(result);
    }
}

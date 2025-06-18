using Moq;
using Xunit;
using Tea.Domain.Models;
using Tea.Application;
using Tea_Commerce.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;


namespace Tea_Commerce.Tests;

public class ProductControllerTest
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductController _controller;

    public ProductControllerTest()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductController(_mockService.Object);
    }
    [Fact]
    public async Task Get_ShouldReturnTrue()
    {
        var products = new List<Product>
        {
            new Product { Id=1 },
            new Product { Id=2 }
        };

        _mockService.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

        var result = await _controller.Get();

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var value = okResult.Value.Should().BeAssignableTo<IEnumerable<Product>>().Subject;
        value.Count().Should().Be(2);
    }


    [Fact]
    public async Task GetById_ShouldReturnTrue_WhenExists()
    {
        var product = new Product { Id = 1 };
        _mockService.Setup(i => i.GetAsync(1)).ReturnsAsync(product);

        var result = await _controller.Get(1);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var returned = okResult.Value.Should().BeOfType<Product>().Subject;
        returned.Id.Should().Be(1);

    }

    [Fact]
    public async Task GetById_ShouldReturnTrue_WhenNotExists()
    {
        _mockService.Setup(i => i.GetAsync(-1)).ReturnsAsync((Product)null!);

        var result = await _controller.Get(-1);

        result.Should().BeOfType<NotFoundResult>();

    }

    [Fact]
    public async Task Post_ShouldAddProduct()
    {
        var product = new Product { Id = 1 };

        _mockService.Setup(s => s.AddAsync(It.IsAny<Product>())).ReturnsAsync(product);

        var result = await _controller.Post(product);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var added = okResult.Value.Should().BeOfType<Product>().Subject;
        added.Id.Should().Be(1);
    }

    [Fact]
    public async Task Put_ShouldUpdateProduct()
    {
        var product = new Product { Id = 1, Price = 100 };

        _mockService.Setup(u => u.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(product);

        var result = await _controller.Put(1, product);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var updated = okResult.Value.Should().BeOfType<Product>().Subject;
        updated.Price.Should().Be(100);

    }


    [Fact]
    public async Task Delete_ShouldMarkProductAsDeleted()
    {
        var product = new Product { Id = 1, Deleted = false };

        _mockService.Setup(d => d.GetAsync(1)).ReturnsAsync(product);
        _mockService.Setup(d => d.UpdateAsync(It.Is<Product>(p => p.Deleted))).ReturnsAsync(product);

        var result = await _controller.Delete(1);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var deleted = okResult.Value.Should().BeOfType<Product>().Subject;
        deleted.Deleted.Should().BeTrue();
    }

}
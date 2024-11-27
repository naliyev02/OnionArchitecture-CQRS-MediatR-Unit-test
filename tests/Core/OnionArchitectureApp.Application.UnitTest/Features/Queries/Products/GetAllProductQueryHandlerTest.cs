using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using NUnit.Framework;
using OnionArchitectureApp.Application.Dtos.ProductDtos;
using OnionArchitectureApp.Application.Features.Queries.Products;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Domain.Entities;
using System.Linq.Expressions;
using System.Net;

namespace OnionArchitectureApp.Application.UnitTest.Features.Queries.Products;

[TestFixture]
public class GetAllProductQueryHandlerTest
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMapper> _mapperMock;
    private GetAllProductQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllProductQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task Handle_WhenProductsExist_ReturnsProductList()
    {
        // Arrange

        var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product1", Type = new ProductType() },
            new Product { Id = Guid.NewGuid(), Name = "Product2", Type = new ProductType() }
        }.AsQueryable();

        _unitOfWorkMock.Setup(u => u.ProductRepository.GetAll(It.IsAny<Func<IQueryable<Product>, IIncludableQueryable<Product, object>>>())).Returns(products);

        _mapperMock.Setup(m => m.Map<List<ProductGetAllDto>>(It.IsAny<List<Product>>())).Returns(new List<ProductGetAllDto>());

        // Act
        var result = await _handler.Handle(new GetAllProductQuery(), CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        Assert.IsNotEmpty(result.Data);
        _unitOfWorkMock.Verify(u => u.ProductRepository.GetAll(It.IsAny<Func<IQueryable<Product>, IIncludableQueryable<Product, object>>>()), Times.Once);
    }

    [Test]
    public async Task Handle_WhenNoProductsExist_ReturnsEmptyList()
    {
        // Arrange
        var query = new GetAllProductQuery();
        var emptyProductList = new List<Product>();
        var emptyProductDtoList = new List<ProductGetAllDto>();

        _unitOfWorkMock.Setup(u => u.ProductRepository.GetAll(It.IsAny<Func<IQueryable<Product>, IIncludableQueryable<Product, object>>>())).Returns(emptyProductList.AsQueryable());
        _mapperMock.Setup(m => m.Map<List<ProductGetAllDto>>(emptyProductList)).Returns(emptyProductDtoList);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        Assert.AreEqual(emptyProductDtoList, result.Data);

        _unitOfWorkMock.Verify(u => u.ProductRepository.GetAll(It.IsAny<Func<IQueryable<Product>, IIncludableQueryable<Product, object>>>()), Times.Once);
        _mapperMock.Verify(m => m.Map<List<ProductGetAllDto>>(emptyProductList), Times.Once);
    }
}

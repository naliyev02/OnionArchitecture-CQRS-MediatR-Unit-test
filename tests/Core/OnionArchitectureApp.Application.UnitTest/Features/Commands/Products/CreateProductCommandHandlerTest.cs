using AutoMapper;
using Moq;
using NUnit.Framework;
using OnionArchitectureApp.Application.Features.Commands.Products;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Domain.Entities;
using System.Net;

namespace OnionArchitectureApp.Application.UnitTest.Features.Commands.Products;

[TestFixture]
public class CreateProductCommandHandlerTest
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMapper> _mapperMock;
    private CreateProductCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateProductCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task Handle_ValidRequest_CreatesProductAndReturnsSuccessResult()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Price = 100.0,
            StockQuantity = 10
        };

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Price = command.Price,
            StockQuantity = command.StockQuantity
        };

        _mapperMock
            .Setup(m => m.Map<Product>(command))
            .Returns(product);

        _unitOfWorkMock
            .Setup(u => u.ProductRepository.CreateAsync(product))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(product.Id, result.Data);
        Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

        // Verify mocks
        _mapperMock.Verify(m => m.Map<Product>(command), Times.Once);
        _unitOfWorkMock.Verify(u => u.ProductRepository.CreateAsync(product), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Test]
    public void Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "Invalid Product",
            Price = -10.0, // Invalid price
            StockQuantity = 10
        };

        var product = new Product
        {
            Name = command.Name,
            Price = command.Price,
            StockQuantity = command.StockQuantity
        };

        _mapperMock
            .Setup(m => m.Map<Product>(command))
            .Returns(product);

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));

        // Verify mocks
        _mapperMock.Verify(m => m.Map<Product>(command), Times.Once);
        _unitOfWorkMock.Verify(u => u.ProductRepository.CreateAsync(It.IsAny<Product>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}

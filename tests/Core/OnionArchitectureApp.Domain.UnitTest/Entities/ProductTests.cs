using NUnit.Framework;
using OnionArchitectureApp.Domain.Entities;
using System.Reflection;

namespace OnionArchitectureApp.Domain.UnitTest.Entities;

[TestFixture]
public class ProductTests
{
    [Test]
    public void UpdatePrice_WithValidPrice_UpdatesPrice()
    {
        // Arrange
        var product = new Product();
        double validPrice = 50.0;

        //Act
        product.UpdatePrice(validPrice);

        // Assert
        Assert.AreEqual(validPrice, product.Price);
    }

    [Test]
    public void UpdatePrice_WithInvalidPrice_ThrowsArgumentException()
    {
        // Arrange
        var product = new Product();
        double invalidPrice = -50.0;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => product.UpdatePrice(invalidPrice));
    }

    [Test]
    public void UpdateStockQuantity_WithValidStockQuantity_UpdatesStockQuantity()
    {
        // Arrange
        var product = new Product();
        int validStockQuantity = 50;

        //Act
        product.UpdateStockQuantity(validStockQuantity);

        // Assert
        Assert.AreEqual(validStockQuantity, product.StockQuantity);
    }

    [Test]
    public void UpdateStockQuantity_WithInvalidStockQuantity_ThrowsArgumentException()
    {
        var product = new Product();
        int invalidStockQuantity = -50;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => product.UpdateStockQuantity(invalidStockQuantity));
    }


}

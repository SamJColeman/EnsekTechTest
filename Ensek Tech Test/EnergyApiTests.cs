using Xunit;
using System.Threading.Tasks;
using FluentAssertions;
using System.Linq;
using System;
using System.Net.Http;
using Ensek_Tech_Test.Models;

namespace Ensek_Tech_Test
{
    public class EnergyApiTests
    {
        [Fact]
        public async Task EnsekApi_GetEnergy_ReturnsResponse()
        {
            // Arrange + Act
            var response = await ApiHelper.GetEnergies();

            // Assert
            response.Should().NotBeNull();
            response?.Electric.Should().NotBeNull();
            response?.Gas.Should().NotBeNull();
            response?.Nuclear.Should().NotBeNull();
            response?.Oil.Should().NotBeNull();
        }

        [Fact]
        public async Task EnsekApi_GetOrders_ReturnsResponse()
        {
            // Arrange + Act
            var response = await ApiHelper.GetOrders();

            // Assert
            response.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task EnsekApi_GetOrders_OnlyTwoOrdersInFeb()
        {
            // Arrange + Act
            var response = await ApiHelper.GetOrders();

            // Assert
            response.Should().NotBeNullOrEmpty();
            response.Where(x => x.Time.Month == 2).Count().Should().Be(2);
        }

        [Theory]
        [InlineData(typeof(Gas), 1)]
        [InlineData(typeof(Nuclear), 2)]
        [InlineData(typeof(Electric), 3)]
        [InlineData(typeof(Oil), 4)]
        public async Task EnsekApi_Buy_PlaceOrderSuccessfully(Type energyType, int quantity)
        {
            // Arrange
            var energies = await ApiHelper.GetEnergies();
            energies.Should().NotBeNull();

            var energyId = energyType switch
            {
                Type t when t == typeof(Gas) => energies?.Gas.EnergyId,
                Type t when t == typeof(Nuclear) => energies?.Nuclear.EnergyId,
                Type t when t == typeof(Electric) => energies?.Electric.EnergyId,
                Type t when t == typeof(Oil) => energies?.Oil.EnergyId,
            };

            // Act
            var response = await ApiHelper.BuyEnergy(energyId.Value, quantity);

            // Arrange
            response.Should().NotBeNull();
            response?.Message.Should().NotBeNull();
            var id = response?.Message.Replace(".", string.Empty).Split(" ").Last();
            Guid.TryParse(id, out var orderId).Should().BeTrue();

            var orders = await ApiHelper.GetOrders();
            orders.Should().NotBeNullOrEmpty();
            orders.Should().Contain(x => x.Id == orderId && x.Quantity == quantity);
        }

        [Theory]
        [InlineData(15, 1)]
        [InlineData(1, -10)]
        public void EnsekApi_Buy_FailsWhenInvalidData(int energyType, int quantity)
        {
            // Arrange + Act
            Func<Task> act = async () => await ApiHelper.BuyEnergy(energyType, 10);

            // Arrange
            act.Should().ThrowAsync<HttpRequestException>();
        }
    }
}

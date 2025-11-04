using Bogus;
using TechFood.Order.Domain.Entities;

namespace TechFood.Order.Domain.Tests.Fixtures
{
    public class ProductFixture
    {
        private readonly Faker _faker;
        public ProductFixture()
        {
            _faker = new Faker("pt_BR");
        }

        private readonly string[] _productName = { "coca", "X-Bacon", "Milk Shake de Baunilha", "Fanta", "Batata Frita" };
        private readonly string[] _productImageFileName = { "coca-cola.png", "x-bacon.png", "milk-shake-baunilha.png", "milk-shake-chocolate.png", "fanta.png" };

        public Product CreateProductNameIsEmpty()
            => new(
                new Guid(),
                string.Empty,
                _faker.Random.Number(20, 40),
                _faker.PickRandom(_productImageFileName));

        public Product CreateProductPriceIsGreaterThanZero()
            => new(
                new Guid(),
                _faker.PickRandom(_productName),
                _faker.Random.Number(-40, -1),
                _faker.PickRandom(_productImageFileName));
    }
}

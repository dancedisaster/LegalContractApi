using LegalContractApi.Repositories;
using LegalContractApi.Services;
using LegalContractApi.DTOs;
using Microsoft.Extensions.Logging;

using Xunit;
using Moq;
using LegalContractApi.Models;

namespace LegalContractTests
{
    public class ContractServiceTests
    {
        private readonly Mock<IContractRepository> _repoMock = new();
        private readonly Mock<ILogger<ContractService>> _loggerMock = new();


        //test the create async method
        [Fact]
        public async Task CreateAsync_ShouldCreateContract_WhenValidDtoProvided()
        {
            // Arrange
            var service = new ContractService(_repoMock.Object, _loggerMock.Object);
            var dto = new ContractCreateDto
            {
                LegalEntityName = "Test Entity",
                AuthorName = "Test Author",
                Description = "Test Description",
            };

            LegalContract? expectedContract = null;
            _repoMock.Setup(r => r.AddAsync(It.IsAny<LegalContract>()))
                     .Callback<LegalContract>(c => expectedContract = c)
                     .Returns(Task.CompletedTask);



            // Act
            var result = await service.CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Entity", expectedContract.LegalEntityName);
            Assert.Equal("Test Author", expectedContract.AuthorName);
            Assert.Equal("Test Description", expectedContract.Description);
            Assert.NotEqual(Guid.Empty, expectedContract.Id);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<LegalContract>()), Times.Once);
        }


        // Test the GetAllAsync method
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllContracts()
        {
            // Arrange
            var service = new ContractService(_repoMock.Object, _loggerMock.Object);
            var contracts = new List<LegalContract>
            {
                new LegalContract { Id = Guid.NewGuid(), AuthorName = "Author 1", LegalEntityName = "Author 2" },
                new LegalContract { Id = Guid.NewGuid(), AuthorName = "Author 2",LegalEntityName = "Entity2" }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(contracts);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.AuthorName == "Author 1");
            Assert.Contains(result, c => c.AuthorName == "Author 2");

            _repoMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsContract_WhenExists()
        {
            var contractId = Guid.NewGuid();
            var mockContract = new LegalContract { Id = contractId, AuthorName = "Author test", LegalEntityName = "Entity Name" };
            _repoMock.Setup(r => r.GetByIdAsync(contractId)).ReturnsAsync(mockContract);
            var service = new ContractService(_repoMock.Object, _loggerMock.Object);

            var result = await service.GetByIdAsync(contractId);

            Assert.NotNull(result);
            Assert.Equal(contractId, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenContractNotFound()
        {
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((LegalContract?)null);
            var service = new ContractService(_repoMock.Object, _loggerMock.Object);

            var result = await service.UpdateAsync(id, new ContractUpdateDto());

            Assert.False(result);
        }

        //Test Delete
        [Fact]
        public async Task DeleteAsync_ShouldDeleteContract_WhenExists()
        {
            // Arrange
            var service = new ContractService(_repoMock.Object, _loggerMock.Object);
            var contractId = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(contractId)).ReturnsAsync(new LegalContract { Id = contractId, AuthorName = "Author test", LegalEntityName = "Entity test" });
            _repoMock.Setup(r => r.DeleteAsync(contractId)).Returns(Task.CompletedTask);

            // Act
            await service.DeleteAsync(contractId);
            // Assert
            _repoMock.Verify(r => r.DeleteAsync(contractId), Times.Once);
        }


        [Theory]
        [InlineData("Henrique", "Benfica", "Contrat Abc")]
        [InlineData("John Doe", "Xpto", "")]
        [InlineData("Cristine", "Tech Something", "Contract Xpto")]
        public async Task CreateAsync_ShouldCreateContract_WithDifferentInputs(
       string author, string legalEntity, string description)
        {
            // Arrange
            var dto = new ContractCreateDto
            {
                AuthorName = author,
                LegalEntityName = legalEntity,
                Description = description
            };

            var repoMock = new Mock<IContractRepository>();
            var loggerMock = new Mock<ILogger<ContractService>>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<LegalContract>())).Returns(Task.CompletedTask);

            var service = new ContractService(repoMock.Object, loggerMock.Object);

            // Act
            var result = await service.CreateAsync(dto);

            // Assert
            // Assert
            Assert.NotNull(result);
            Assert.Equal(author, result.AuthorName);
            Assert.Equal(legalEntity, result.LegalEntity);
            Assert.Equal(description, result.Description);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000001")]
        [InlineData("11111111-1111-1111-1111-111111111111")]
        public async Task GetByIdAsync_ShouldReturnNull_WhenContractDoesNotExist(string guidStr)
        {
            // Arrange
            var id = Guid.Parse(guidStr);
            var repoMock = new Mock<IContractRepository>();
            repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((LegalContract?)null);

            var service = new ContractService(repoMock.Object, Mock.Of<ILogger<ContractService>>());

            // Act
            var result = await service.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
        }

    }
}
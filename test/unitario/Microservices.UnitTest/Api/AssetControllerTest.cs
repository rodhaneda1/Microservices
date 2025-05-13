using Moq;
using Xunit;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microservices.Api.Controllers.V1;
using Microservices.Application.Querys;
using NSubstitute.ExceptionExtensions;

namespace Microservices.UnitTest.Api
{
    public class AssetControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<AssetController>> _mockLogger;

        public AssetControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<AssetController>>();
        }

        [Fact]
        public async Task GetAssetValue_Should_Return_200_With_Valid_Response()
        {
            // Arrange
            var request = new GetAssetValueRequest
            {
                InitialValue = 1000m,
                CDI = 0.1m,
                BankFee = 0.05m
            };

            var response = new GetAssetValueResponse
            {
                InitialValue = request.InitialValue,
                CDI = request.CDI,
                BankFee = request.BankFee,
                FinalValue = 1005m
            };

            _mockMediator
                .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var controller = new AssetController(_mockMediator.Object);

            // Act
            var result = await controller.GetAssetValueAsync(request);
            var okResult = result as OkObjectResult;
            var value = okResult?.Value as GetAssetValueResponse;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(value);
            Assert.Equal(response.FinalValue, value.FinalValue);
            Assert.Equal(response.InitialValue, value.InitialValue);
            Assert.Equal(response.CDI, value.CDI);
            Assert.Equal(response.BankFee, value.BankFee);
        }

        [Fact]
        public async Task GetAssetValue_Should_Return_BadRequest_When_Request_Is_Null()
        {
            // Arrange
            var expResult = new ArgumentNullException();
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAssetValueRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(expResult);

            var controller = new AssetController(_mockMediator.Object);

            // Act
            var result = await controller.GetAssetValueAsync(null);
            var badRequestResult = result as BadRequestResult;

            // Assert
            Assert.Equal(400, badRequestResult.StatusCode);
        }
    }
}

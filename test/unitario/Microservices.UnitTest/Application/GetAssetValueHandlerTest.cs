using Moq;
using Xunit;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microservices.Application.Querys;

public class GetAssetValueHandlerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<GetAssetValueHandler>> _loggerMock;
    private readonly GetAssetValueHandler _handler;

    public GetAssetValueHandlerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<GetAssetValueHandler>>();
        _handler = new GetAssetValueHandler(_mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCorrectFinalValue()
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
            BankFee = request.BankFee
        };

        _mapperMock.Setup(m => m.Map<GetAssetValueResponse>(request)).Returns(response);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        var expectedFinalValue = request.InitialValue * (1 + (request.CDI * request.BankFee));
        Assert.Equal(expectedFinalValue, result.FinalValue);
        Assert.Equal(request.InitialValue, result.InitialValue);
        Assert.Equal(request.CDI, result.CDI);
        Assert.Equal(request.BankFee, result.BankFee);
    }

    [Fact]
    public async Task Handle_NullRequest_ThrowsArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
    }
}

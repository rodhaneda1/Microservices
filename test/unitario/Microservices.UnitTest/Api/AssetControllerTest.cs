using Moq;
using Xunit;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microservices.Api.Controllers.V1;
//using Microservices.Application.Querys.Base;
//using Microservices.Application.Querys.Limit;
//using Microservices.Application.Querys.LimitCp;
//using Microservices.Application.Querys.GetNodeLimit;
//using Microservices.Application.Querys.PostNodeLimit;
//using Microservices.Application.Querys.GetPeriod;
//using Microservices.Application.Querys.GetFavoriteLimit;

namespace Microservices.UnitTest.Api
{
    public class AssetControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger> _mockLogger;

        public AssetControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger>();
        }

        //[Fact]
        //public async Task OperationLimit_Should_Return_200()
        //{
        //    // arrange
        //    var request = new LimitProductRequest { NumberAccount = "0111100566", ReferenceDate = DateTime.Now, NewLimit = 0, OperationValue = 100, Product = ProductEnum.TED, Situation = SituationEnum.E };
        //    var response = new LimitProductResponse()
        //    {
        //        Body = new LimitProductResponseBody
        //        {
        //            ListLimitProduct = new List<OperationLimitProductResponse> {
        //                new OperationLimitProductResponse {
        //                    AvailableLimit = 10000, AllowedTransaction = true, CodeBranch = "0019", Product = "PIX", NumberAccount = "0111100566"}
        //            }
        //        }
        //    };

        //    _mockMediator.Setup(x => x.Send(request, default(CancellationToken)))
        //        .ReturnsAsync(response)
        //        .Verifiable();

        //    var _sut = new LimitsController(_mockMediator.Object);

        //    // act
        //    var result = await _sut.OperationLimit(request);
        //    var okResult = result.Result as OkObjectResult;
        //    var accountBalanceRetornado = okResult.Value as LimitProductResponse;

        //    // assert
        //    Assert.IsType<OkObjectResult>(result.Result);
        //    Assert.Equal(accountBalanceRetornado, response);
        //}

        //[Fact]
        //public async Task OperationLimitCp_Should_Return_200()
        //{
        //    // arrange
        //    var request = new LimitProductCpRequest { NumberAccount = "0111100566", ReferenceDate = DateTime.Now, NewLimit = 0, OperationValue = 100, Product = ProductCpEnum.CPTED };
        //    var response = new LimitProductCpResponse()
        //    {
        //        Body = new LimitProductResponseBody
        //        {
        //            ListLimitProduct = new List<OperationLimitProductResponse> {
        //                new OperationLimitProductResponse {
        //                    AvailableLimit = 10000, AllowedTransaction = true, CodeBranch = "0019", Product = "PIX", NumberAccount = "0111100566"}
        //            }
        //        }
        //    };

        //    _mockMediator.Setup(x => x.Send(request, default(CancellationToken)))
        //        .ReturnsAsync(response)
        //        .Verifiable();

        //    var _sut = new LimitsController(_mockMediator.Object);

        //    // act
        //    var result = await _sut.OperationLimitCp(request);
        //    var okResult = result.Result as OkObjectResult;
        //    var accountBalanceRetornado = okResult.Value as LimitProductCpResponse;

        //    // assert
        //    Assert.IsType<OkObjectResult>(result.Result);
        //    Assert.Equal(accountBalanceRetornado, response);
        //}

        //[Fact]
        //public async Task GetPeriodAsync_Should_Return_200()
        //{
        //    // arrange
        //    var request = new GetPeriodRequest { cpfCnpj = "62868578853", agency = "0001", accountNumber = "0106035743" };
        //    var response = new GetPeriodResponse();

        //    _mockMediator.Setup(x => x.Send(request, default(CancellationToken)))
        //        .ReturnsAsync(response)
        //        .Verifiable();

        //    var _sut = new LimitsController(_mockMediator.Object);

        //    // act
        //    var result = await _sut.GetPeriods(request);
        //    var okResult = result.Result as OkObjectResult;
        //    var limitsResponse = okResult.Value as GetPeriodResponse;

        //    // assert
        //    Assert.IsType<OkObjectResult>(result.Result);
        //    Assert.Equal(limitsResponse, response);
        //}

        //[Fact]
        //public async Task GetFavoriteLimitsAsync_Should_Return_200()
        //{
        //    // arrange
        //    var request = new GetFavoriteLimitRequest 
        //    { 
        //        agencyNumberOrig = "0001", 
        //        accountNumberOrig = "0106035743", 
        //        documentOrig = "62868578853", 
        //        documentFav = "123456789", 
        //        product ="PIX" 
        //    };
        //    var response = new List<GetFavoriteLimitResponse>();

        //    _mockMediator.Setup(x => x.Send(request, default(CancellationToken)))
        //        .ReturnsAsync(response)
        //        .Verifiable();

        //    var _sut = new LimitsController(_mockMediator.Object);

        //    // act
        //    var result = await _sut.GetFavoriteLimit(request);
        //    var okResult = result.Result as OkObjectResult;
        //    var limitsResponse = okResult.Value as List<GetFavoriteLimitResponse>;

        //    // assert
        //    Assert.IsType<OkObjectResult>(result.Result);
        //    Assert.Equal(limitsResponse, response);
        //}

        //[Fact]
        //public async Task GetLimitsAsync_Should_Return_200()
        //{
        //    // arrange
        //    var request = new GetNodeLimitRequest { cpfCnpj = "62868578853", agency= "0001", accountNumber= "0106035743" };
        //    var response = new List<GetNodeLimitResponse>();

        //    _mockMediator.Setup(x => x.Send(request, default(CancellationToken)))
        //        .ReturnsAsync(response)
        //        .Verifiable();

        //    var _sut = new LimitsController(_mockMediator.Object);

        //    // act
        //    var result = await _sut.GetNodeLimit(request);
        //    var okResult = result.Result as OkObjectResult;
        //    var limitsResponse = okResult.Value as List<GetNodeLimitResponse>;

        //    // assert
        //    Assert.IsType<OkObjectResult>(result.Result);
        //    Assert.Equal(limitsResponse, response);
        //}

        //[Fact]
        //public async Task PostLimitsAsync_Should_Return_200()
        //{
        //    // arrange
        //    var request = new PostNodeLimitRequest { cpfCnpj = "62868578853", agency = "0001", accountNumber = "0106035743" };
        //    var response = new PostNodeLimitResponse();

        //    _mockMediator.Setup(x => x.Send(request, default(CancellationToken)))
        //        .ReturnsAsync(response)
        //        .Verifiable();

        //    var _sut = new LimitsController(_mockMediator.Object);

        //    // act
        //    var result = await _sut.PostNodeLimit(request);
        //    var okResult = result.Result as OkObjectResult;
        //    var limitsResponse = okResult.Value as PostNodeLimitResponse;

        //    // assert
        //    Assert.IsType<OkObjectResult>(result.Result);
        //    Assert.Equal(limitsResponse, response);
        //}
    }
}
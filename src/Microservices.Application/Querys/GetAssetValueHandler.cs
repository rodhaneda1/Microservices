using MediatR;
using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Microservices.Application.Querys
{
    public class GetAssetValueHandler : IRequestHandler<GetAssetValueRequest, GetAssetValueResponse>
    {
        private readonly ILogger<GetAssetValueHandler> _logger;
        private readonly IMapper _mapper;

        public GetAssetValueHandler(IMapper mapper, ILogger<GetAssetValueHandler> logger)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GetAssetValueResponse> Handle(GetAssetValueRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                _logger.LogWarning("Received null request in GetAssetValueHandler.");
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Handling GetAssetValueRequest: {@Request}", request);

            var result = _mapper.Map<GetAssetValueResponse>(request);
            result.FinalValue = result.InitialValue * (1 + (result.CDI * result.BankFee));

            _logger.LogInformation("Returning GetAssetValueResponse: {@Response}", result);

            return await Task.FromResult(result);
        }
    }
}

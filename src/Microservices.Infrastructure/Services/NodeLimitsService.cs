using AutoMapper;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microservices.Configuration;
using Microservices.Domain.Dtos;
using Microservices.Domain.Interfaces;
using Microservices.Infrastructure.Extensions;

namespace Microservices.Infrastructure.Services
{
    public class NodeLimitsService : ServiceBae, INodeLimitsService
    {
        private readonly ILogger _log;
        private readonly IMapper _mapper;
        public readonly HttpClient _client;
        public readonly MicroservicesSettings _settings;

        public NodeLimitsService(ILogger log, IMapper mapper, HttpClient client, MicroservicesSettings settings)
        {
            _log = log;
            _mapper = mapper;
            _client = client;
            _settings = settings;

            var timeOut = string.IsNullOrEmpty(_settings.Timeout) ? 30000 : Convert.ToInt32(_settings.Timeout);
            _client.Timeout =  TimeSpan.FromSeconds(timeOut);
        }

        public async Task<IEnumerable<GetFavoriteLimitResponseDto>> GetFavoriteLimitsAsync(GetFavoriteLimitRequestDto request)
        {
            var url = _settings.UrlService + $"/favored/limits/{request.agencyNumberOrig}/{request.accountNumberOrig}/{request.documentOrig}/{request.documentFav}/{request.product}";
            _log.LogInformation($"GetFavoriteLimits - {url} - {JsonSerializer.Serialize(request)}");
            var response = await _client.GetAsync(url);

            return await DeserializarObjetoResponse<IEnumerable<GetFavoriteLimitResponseDto>>(response);
        }

        public async Task<IEnumerable<GetLimitResponseDto>> GetLimitsAsync(GetLimitRequestDto request)
        {
            var url = _settings.UrlService + $"/limits/{request.cpfCnpj}/{request.agency}/{request.accountNumber}";
            _log.LogInformation($"GetLimits - {url} - {JsonSerializer.Serialize(request)}");
            var response = await _client.GetAsync(url);

            return await DeserializarObjetoResponse<IEnumerable<GetLimitResponseDto>>(response);
        }

        public async Task<PostLimitsResponseDto> PostLimitsAsync(PostLimitsRequestDto request)
        {
            var url = _settings.UrlService + $"/limits/{request.cpfCnpj}/{request.agency}/{request.accountNumber}/{request.product}";
            var nodeRequest = _mapper.Map<PostLimitDto>(request);
            _log.LogInformation($"Post Limits - {url} - {JsonSerializer.Serialize(request)}");
            var response = await _client.PostAsync(url, ObterConteudo(nodeRequest));

            return await DeserializarObjetoResponse<PostLimitsResponseDto>(response);
        }
    }
}

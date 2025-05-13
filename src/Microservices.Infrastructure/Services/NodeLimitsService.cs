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
    }
}

using AutoMapper;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microservices.CrossCutting.AutoMapper.Profiles;

namespace Microservices.CrossCutting.AutoMapper
{
    public static class AutoMapperSetup
    {
        public static void SetupAutoMapper(this IServiceCollection services) => services.AddSingleton(GetMapper());

        public static IMapper GetMapper()
        {
            var _mce = new MapperConfigurationExpression();
            _mce.ConstructServicesUsing(Activator.CreateInstance);
            var _profileType = typeof(Profile);
            var _profiles = typeof(GetAssetValueProfile).Assembly.ExportedTypes
                    .Where(type => !type.IsAbstract && _profileType.IsAssignableFrom(type))
                    .Select(Activator.CreateInstance)
                    .Cast<Profile>()
                    .ToArray();

            _mce.AddProfiles(_profiles);
            var _config = new MapperConfiguration(_mce);

            _config.AssertConfigurationIsValid();
            return _config.CreateMapper();
        }
    }
}
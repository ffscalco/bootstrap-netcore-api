using System;
using Api.Mappings;
using AutoMapper;
using NUnit.Framework;

namespace Api.Test
{
    public class AutoMapperTest
    {
        [Test]
        // Source: https://docs.automapper.org/en/stable/Configuration-validation.html
        public void TestAutoMapperConfiguration()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            mappingConfig.AssertConfigurationIsValid();
        }
    }
}

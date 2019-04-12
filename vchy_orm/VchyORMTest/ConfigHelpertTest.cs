using Microsoft.Extensions.Configuration;
using System;
using VchyORMFactory;
using Xunit;

namespace VchyORMTest
{
    public class ConfigHelpertTest
    {
        [Fact]
        public void TestReadJsonFile()
        {
            var equal = "Data Source = 192.168.0.104;Initial Catalog = XSYB.Spider;User Id = sa;Password = 123456;";
            Assert.NotNull(ConfigHelper.Configuration.GetConnectionString("dapper"));
            Assert.NotEmpty(ConfigHelper.Configuration.GetConnectionString("dapper"));
            Assert.Equal(ConfigHelper.Configuration.GetConnectionString("dapper"),equal);
        }
    }
}

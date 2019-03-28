using System;
using vchy_orm;
using Xunit;

namespace VchyORMTest
{
    public class ConfigHelpertTest
    {
        [Fact]
        public void TestReadJsonFile()
        {
            Assert.NotNull(ConfigHelper.GetSectionValue("conn"));
            Assert.NotEmpty(ConfigHelper.GetSectionValue("conn"));
        }
    }
}

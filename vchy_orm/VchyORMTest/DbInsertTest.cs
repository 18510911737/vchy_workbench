using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VchyModel;
using VchyORMAttribute;
using VchyORMFactory.Factotry;
using Xunit;

namespace VchyORMTest
{
    public class DbInsertTest
    {
        [Fact]
        public void TestAddModel()
        {
            User user = new User
            {
                LoginName = "test",
                Password = "test",
            };
            DapperFactory factory = new DapperFactory();
            Assert.NotEqual(0, factory.ExcuteInsert(user));
        }

        [Fact]
        public void TestAddModels()
        {
            var users = Enumerable.Range(0, 10).Select(i =>
                new User
                {
                    LoginName = $"test{i}",
                    Password = $"test{i}"
                }
            ).ToList();
            DapperFactory factory = new DapperFactory();
            Assert.NotEqual(0, factory.ExcuteInsert(users));
        }
    }

    [Table(table: "User")]
    public class User : BaseEntity
    {
        [Field(field: "ID", IsKey = true)]
        public int ID { get; set; }

        [Field(field: "RoleId")]
        public int RoleId { get; set; }

        [Field(field: "LoginName")]
        public string LoginName { get; set; }

        [Field(field: "Password")]
        public string Password { get; set; }
    }
}

using System.Collections.Generic;
using CustomerManagement.Domain.Entities;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var list = new List<string> { "emma", null, "oi" };

            var x = string.Join("-", list);
            Assert.True(true);
        }
    }
}
using Entity.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.Organization
{
    public class TerminalOrganization_Test
    {
        [Fact]
        public void Clone()
        {
            var a = new TerminalOrganization(new("aaaa"), new(new("aaa", "bbb"), new(1000, 1, 1)));
            var b = a.Clone();

            Assert.True(a == b);
        }
    }
}

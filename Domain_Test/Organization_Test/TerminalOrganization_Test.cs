using Entity.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.Organization_Test
{
    public class TerminalOrganization_Test
    {
        [Fact]
        public void Clone()
        {
            var a = new TerminalOrganization(new("aaaa"));
            var b = a.Clone();

            Assert.True(a.SameIdentityAs(b));
            Assert.True(a.DisplayName == b.DisplayName);
        }
    }
}

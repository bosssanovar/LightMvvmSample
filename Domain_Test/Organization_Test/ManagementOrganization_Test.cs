using Entity.Organization;
using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.Organization_Test
{
    public class ManagementOrganization_Test
    {
        [Fact]
        public void コンストラクタ_例外()
        {
            try
            {
                var a = new ManagementOrganization(new("aa"), Ranks.Team, new List<OrganizationBase>());
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            catch
            {
                Assert.Fail();
            }
            Assert.Fail();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10000)]
        public void 下部組織数_管理組織(int count)
        {
            var a = new List<OrganizationBase>();
            for(int i=0; i<count; i++)
            {
                a.Add(new ManagementOrganization(new("aa"), Ranks.Department, new List<OrganizationBase>()));
            }

            var b = new ManagementOrganization(new("aa"), Ranks.Department, a);

            Assert.Equal(count, b.OrganizationCount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10000)]
        public void 下部組織数_末端組織(int count)
        {
            var a = new List<OrganizationBase>();
            for (int i = 0; i < count; i++)
            {
                a.Add(new TerminalOrganization(new("aa")));
            }

            var b = new ManagementOrganization(new("aa"), Ranks.Section, a);

            Assert.Equal(count, b.OrganizationCount);
        }
    }
}

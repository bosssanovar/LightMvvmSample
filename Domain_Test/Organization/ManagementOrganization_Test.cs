using Entity.Organization;
using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.Organization
{
    public class ManagementOrganization_Test
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10000)]
        public void 下部組織数(int count)
        {
            var a = new List<OrganizationBase>();
            for(int i=0; i<count; i++)
            {
                a.Add(new ManagementOrganization(new("aa"), new Person(new("aaa", "bbb"), new(1000, 1, 1)), new List<OrganizationBase>()));
            }

            var b = new ManagementOrganization(new("aa"), new Person(new("aaa", "bbb"), new(1000, 1, 1)), a);

            Assert.Equal(count, b.OrganizationCount);
        }
    }
}

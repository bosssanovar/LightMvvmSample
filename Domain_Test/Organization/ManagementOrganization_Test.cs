﻿using Entity.Organization;
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
        public void 下部組織数_管理組織(int count)
        {
            var a = new List<OrganizationBase>();
            for(int i=0; i<count; i++)
            {
                a.Add(new ManagementOrganization(new("aa"), Lanks.Department, new Person(new("aaa", "bbb"), new(1000, 1, 1)), new List<OrganizationBase>()));
            }

            var b = new ManagementOrganization(new("aa"), Lanks.Department, new Person(new("aaa", "bbb"), new(1000, 1, 1)), a);

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
                a.Add(new TerminalOrganization(new("aa"), new Person(new("aaa", "bbb"), new(1000, 1, 1))));
            }

            var b = new ManagementOrganization(new("aa"), Lanks.Section, new Person(new("aaa", "bbb"), new(1000, 1, 1)), a);

            Assert.Equal(count, b.OrganizationCount);
        }

        [Fact]
        public void Clone()
        {
            var a = new List<OrganizationBase>();
            for (int i = 0; i < 10; i++)
            {
                a.Add(new ManagementOrganization(new("aa"), Lanks.Section, new Person(new("aaa", "bbb"), new(1000, 1, 1)), new List<OrganizationBase>()));
            }
            var b = new ManagementOrganization(new("aa"), Lanks.Department, new Person(new("aaa", "bbb"), new(1000, 1, 1)), a);

            var c = b.Clone();

            Assert.True(c.DisplayName == b.DisplayName);
            Assert.True(c.DirectEmployeeCount == b.DirectEmployeeCount);
            Assert.True(c == b);
        }
    }
}
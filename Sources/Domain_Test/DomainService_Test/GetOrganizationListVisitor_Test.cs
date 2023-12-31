﻿using Entity.DomainService.OrganizationVisitor;
using Entity.Organization;
using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.DomainService_Test
{
    public class GetOrganizationListVisitor_Test
    {
        [Fact]
        public void Visit()
        {
            // 組織構築
            var a = new TerminalOrganization(new("1"));
            a.SetBoss(new Person(new("1", "boss"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "1"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "2"), new(1000, 1, 1)));
            a.AddMember(new Person(new("1", "3"), new(1000, 1, 1)));
            var b = new TerminalOrganization(new("2"));
            b.SetBoss(new Person(new("2", "boss"), new(1000, 1, 1)));
            b.AddMember(new Person(new("2", "1"), new(1000, 1, 1)));
            b.AddMember(new Person(new("2", "2"), new(1000, 1, 1)));
            var c = new ManagementOrganization(new("3"), Ranks.Section, new() { a, b });
            c.SetBoss(new Person(new("3", "boss"), new(1000, 1, 1)));
            c.AddMember(new Person(new("3", "1"), new(1000, 1, 1)));
            c.AddMember(new Person(new("3", "2"), new(1000, 1, 1)));
            c.AddMember(new Person(new("3", "3"), new(1000, 1, 1)));
            var d = new TerminalOrganization(new("4"));
            d.SetBoss(new Person(new("4", "boss"), new(1000, 1, 1)));
            d.AddMember(new Person(new("4", "1"), new(1000, 1, 1)));
            d.AddMember(new Person(new("4", "2"), new(1000, 1, 1)));
            d.AddMember(new Person(new("4", "3"), new(1000, 1, 1)));
            var e = new TerminalOrganization(new("5"));
            e.SetBoss(new Person(new("5", "boss"), new(1000, 1, 1)));
            e.AddMember(new Person(new("5", "1"), new(1000, 1, 1)));
            e.AddMember(new Person(new("5", "2"), new(1000, 1, 1)));
            var f = new ManagementOrganization(new("6"), Ranks.Section, new() { d, e });
            f.SetBoss(new Person(new("6", "boss"), new(1000, 1, 1)));
            f.AddMember(new Person(new("6", "1"), new(1000, 1, 1)));
            f.AddMember(new Person(new("6", "2"), new(1000, 1, 1)));
            f.AddMember(new Person(new("6", "3"), new(1000, 1, 1)));
            var top = new ManagementOrganization(new("7"), Ranks.Department, new() { c, f });
            top.SetBoss(new Person(new("7", "boss"), new(1000, 1, 1)));
            top.AddMember(new Person(new("7", "1"), new(1000, 1, 1)));
            top.AddMember(new Person(new("7", "2"), new(1000, 1, 1)));

            var visitor = new GetOrganizationListVisitor();
            top.Accept(visitor);

            Assert.Equal(7, visitor.Oganizations.Count);
            Assert.Equal("7部", visitor.Oganizations[0].FullName);
            Assert.Equal("7部 6課 4チーム", visitor.Oganizations[5].FullName);
        }
    }
}

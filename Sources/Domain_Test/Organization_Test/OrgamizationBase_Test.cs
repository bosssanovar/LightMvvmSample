﻿using Entity.Organization;
using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.Organization_Test
{
    public class OrgamizationBase_Test
    {
        [Fact]
        public void 社員追加_と_所属確認()
        {
            var boss = new Person(new("aaa", "bbb"), new(1000, 1, 1));
            var a = new ManagementOrganization(new("aa"), Ranks.Department, new List<OrganizationBase>());
            a.SetBoss(boss);

            var p1 = new Person(new("ccc", "ddd"), new(1000, 1, 1));
            var p2 = new Person(new("eeee", "ffff"), new(1000, 1, 1));

            Assert.True(a.IsBoss(boss));
            Assert.False(a.IsContainDirectEmployee(p1));
            Assert.False(a.IsContainDirectEmployee(p2));

            a.AddMember(p1);

            Assert.True(a.IsBoss(boss));
            Assert.True(a.IsContainDirectEmployee(p1));
            Assert.False(a.IsContainDirectEmployee(p2));

            a.AddMember(p2);

            Assert.True(a.IsBoss(boss));
            Assert.True(a.IsContainDirectEmployee(p1));
            Assert.True(a.IsContainDirectEmployee(p2));
        }

        [Fact]
        public void 社員の重複登録回避()
        {
            var boss = new Person(new("aaa", "bbb"), new(1000, 1, 1));
            var a = new ManagementOrganization(new("aa"), Ranks.Department, new List<OrganizationBase>());
            a.SetBoss(boss);

            var p1 = new Person(new("ccc", "ddd"), new(1000, 1, 1));

            Assert.True(a.IsBoss(boss));
            Assert.False(a.IsContainDirectEmployee(p1));
            Assert.Equal(0, a.DirectEmployeeCount);

            a.AddMember(p1);

            Assert.True(a.IsBoss(boss));
            Assert.True(a.IsContainDirectEmployee(p1));
            Assert.Equal(1, a.DirectEmployeeCount);

            a.AddMember(p1);

            Assert.True(a.IsBoss(boss));
            Assert.True(a.IsContainDirectEmployee(p1));
            Assert.Equal(1, a.DirectEmployeeCount);
        }

        [Fact]
        public void 社員削除()
        {
            var boss = new Person(new("aaa", "bbb"), new(1000, 1, 1));
            var a = new ManagementOrganization(new("aa"), Ranks.Department, new List<OrganizationBase>());
            a.SetBoss(boss);

            var p1 = new Person(new("ccc", "ddd"), new(1000, 1, 1));

            Assert.True(a.IsBoss(boss));
            Assert.False(a.IsContainDirectEmployee(p1));
            Assert.Equal(0, a.DirectEmployeeCount);

            a.RemoveMember(p1);

            Assert.True(a.IsBoss(boss));
            Assert.False(a.IsContainDirectEmployee(p1));
            Assert.Equal(0, a.DirectEmployeeCount);

            a.AddMember(p1);

            Assert.True(a.IsBoss(boss));
            Assert.True(a.IsContainDirectEmployee(p1));
            Assert.Equal(1, a.DirectEmployeeCount);

            a.RemoveMember(p1);

            Assert.True(a.IsBoss(boss));
            Assert.False(a.IsContainDirectEmployee(p1));
            Assert.Equal(0, a.DirectEmployeeCount);
        }

        [Fact]
        public void 組織長変更()
        {
            var boss = new Person(new("aaa", "bbb"), new(1000, 1, 1));
            var a = new ManagementOrganization(new("aa"), Ranks.Department, new List<OrganizationBase>());
            var p1 = new Person(new("ccc", "ddd"), new(1000, 1, 1));

            Assert.False(a.IsBoss(boss));
            Assert.False(a.IsBoss(p1));
            Assert.False(a.IsContainDirectEmployee(boss));
            Assert.False(a.IsContainDirectEmployee(p1));

            a.SetBoss(boss);

            Assert.True(a.IsBoss(boss));
            Assert.False(a.IsBoss(p1));
            Assert.False(a.IsContainDirectEmployee(boss));
            Assert.False(a.IsContainDirectEmployee(p1));

            a.SetBoss(p1);

            Assert.False(a.IsBoss(boss));
            Assert.True(a.IsBoss(p1));
            Assert.False(a.IsContainDirectEmployee(boss));
            Assert.False(a.IsContainDirectEmployee(p1));
        }

        [Theory]
        [InlineData("aaa aaa aaa")]
        [InlineData("")]
        [InlineData("あああ")]
        [InlineData(" ")]
        [InlineData("　")]
        [InlineData("12345")]
        [InlineData(null)]
        public void 組織名称(string name)
        {
            var boss = new Person(new("aaa", "bbb"), new(1000, 1, 1));
            var a = new ManagementOrganization(new(name), Ranks.Department, new List<OrganizationBase>());
            a.SetBoss(boss);

            Assert.True(a.DisplayName == (name + Ranks.Department.GetDisplayText()));
        }

        [Theory]
        [InlineData(Ranks.Campany)]
        [InlineData(Ranks.Department)]
        [InlineData(Ranks.Section)]
        public void 組織ランク名称(Ranks rank)
        {
            var name = "aaaabbbccc";
            var boss = new Person(new("aaa", "bbb"), new(1000, 1, 1));
            var a = new ManagementOrganization(new(name), rank, new List<OrganizationBase>());
            a.SetBoss(boss);

            Assert.True(a.DisplayName == (name + rank.GetDisplayText()));
        }

        [Fact]
        public void 組織ランク名称_Team()
        {
            var name = "aaaabbbccc";
            var boss = new Person(new("aaa", "bbb"), new(1000, 1, 1));
            var a = new TerminalOrganization(new(name));
            a.SetBoss(boss);

            Assert.True(a.DisplayName == (name + Ranks.Team.GetDisplayText()));
        }
    }
}

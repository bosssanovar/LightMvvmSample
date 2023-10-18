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
    public class OrgamizationBase_Test
    {
        [Fact]
        public void 社員追加_と_所属確認()
        {
            var boss = new Person(new("aaa", "bbb"), new(1000, 1, 1));
            var a = new ManagementOrganization(new("aa"), Lanks.Department, new List<OrganizationBase>());
            a.ChangeBoss(boss);

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
            var a = new ManagementOrganization(new("aa"), Lanks.Department, new List<OrganizationBase>());
            a.ChangeBoss(boss);

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
            var a = new ManagementOrganization(new("aa"), Lanks.Department, new List<OrganizationBase>());
            a.ChangeBoss(boss);

            var p1 = new Person(new("ccc", "ddd"), new(1000, 1, 1));

            Assert.True(a.IsBoss(boss));
            Assert.False(a.IsContainDirectEmployee(p1));
            Assert.Equal(0, a.DirectEmployeeCount);

            a.RemoveMember(p1.Clone());

            Assert.True(a.IsBoss(boss));
            Assert.False(a.IsContainDirectEmployee(p1));
            Assert.Equal(0, a.DirectEmployeeCount);

            a.AddMember(p1.Clone());

            Assert.True(a.IsBoss(boss));
            Assert.True(a.IsContainDirectEmployee(p1));
            Assert.Equal(1, a.DirectEmployeeCount);

            a.RemoveMember(p1.Clone());

            Assert.True(a.IsBoss(boss));
            Assert.False(a.IsContainDirectEmployee(p1));
            Assert.Equal(0, a.DirectEmployeeCount);
        }

        [Fact]
        public void 組織長変更()
        {
            var boss = new Person(new("aaa", "bbb"), new(1000, 1, 1));
            var a = new ManagementOrganization(new("aa"), Lanks.Department, new List<OrganizationBase>());
            a.ChangeBoss(boss);

            var p1 = new Person(new("ccc", "ddd"), new(1000, 1, 1));

            Assert.True(a.IsBoss(boss));
            Assert.False(a.IsContainDirectEmployee(p1));

            var old = a.ChangeBoss(p1);

            Assert.False(a.IsContainDirectEmployee(boss));
            Assert.True(a.IsBoss(p1));
            Assert.True(old == boss);
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
            var a = new ManagementOrganization(new(name), Lanks.Department, new List<OrganizationBase>());
            a.ChangeBoss(boss);

            Assert.True(a.DisplayName == (name + " " + Lanks.Department.GetDisplayText()));
        }

        [Theory]
        [InlineData(Lanks.Campany)]
        [InlineData(Lanks.Department)]
        [InlineData(Lanks.Section)]
        [InlineData(Lanks.Team)]
        public void 組織ランク名称(Lanks lank)
        {
            var name = "aaaabbbccc";
            var boss = new Person(new("aaa", "bbb"), new(1000, 1, 1));
            var a = new ManagementOrganization(new(name), lank, new List<OrganizationBase>());
            a.ChangeBoss(boss);

            Assert.True(a.DisplayName == (name + " " + lank.GetDisplayText()));
        }
    }
}

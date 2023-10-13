using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.Persons
{
    public class Person_Test
    {

        [Fact]
        public void コンストラクタ_GUIDユニーク()
        {
            for (int i = 0; i < 100; i++)
            {
                var a = new Person(new NameVO("aaa", "bbb"), new BirthdayVO(100, 1, 1));
                var b = new Person(new NameVO("aaa", "bbb"), new BirthdayVO(100, 1, 1));
                Assert.False(a.HasSameIdentity(b));
            }
        }

        [Fact]
        public void Equal()
        {
            var a = new Person(new NameVO("aaa", "bbb"), new BirthdayVO(100, 1, 1));
            var b = a;
            b.Name = new NameVO("ccc", "ddd");
            b.Birthday = new(2000, 12, 12);
            Assert.True(a.HasSameIdentity(b));
        }

        [Fact]
        public void Clone()
        {
            var a = new Person(new NameVO("aaa", "bbb"), new BirthdayVO(100, 1, 1));
            var b = a.Clone();

            Assert.True(a.HasSameIdentity(b));
            Assert.True(a.Name.Equals(b.Name));
            Assert.True(a.Birthday.Equals(b.Birthday));
            Assert.Equal(a.PostText, b.PostText);

            b.Name = new("cccc", "ddddd");
            Assert.True(a.HasSameIdentity(b));
            Assert.False(a.Name.Equals(b.Name));
        }

        [Fact]
        public void CopyTo()
        {
            var a = new Person(new NameVO("aaa", "bbb"), new BirthdayVO(100, 1, 1));
            var b = new Person(new NameVO("ccccc", "ddddd"), new BirthdayVO(100, 1, 1));
            b.UpdatePost(Post.Chief);
            a.CopyTo(b);
            Assert.False(a.HasSameIdentity(b));
            Assert.True(a.Name.Equals(b.Name));
            Assert.True(a.Birthday.Equals(b.Birthday));
            Assert.Equal(a.PostText, b.PostText);
        }

        [Theory]
        [InlineData(Post.Employee, Post.Chief)]
        [InlineData(Post.Chief, Post.SectionChief)]
        [InlineData(Post.SectionChief, Post.Manager)]
        [InlineData(Post.Manager, Post.Director)]
        [InlineData(Post.Director, Post.President)]
        public void 役職比較(Post p1, Post p2)
        {
            var a = new Person(new NameVO("aaa", "bbb"), new BirthdayVO(100, 1, 1));
            a.UpdatePost(p1);
            var b = new Person(new NameVO("ccccc", "ddddd"), new BirthdayVO(100, 1, 1));
            b.UpdatePost(p2);

            Assert.False(a.IsHigherPostThan(a));
            Assert.False(b.IsHigherPostThan(b));
            Assert.True(a.IsHigherPostThan(b));
            Assert.False(b.IsHigherPostThan(a));
        }

        [Theory]
        [InlineData(Post.Employee, Post.Chief)]
        [InlineData(Post.Chief, Post.SectionChief)]
        [InlineData(Post.SectionChief, Post.Manager)]
        [InlineData(Post.Manager, Post.Director)]
        [InlineData(Post.Director, Post.President)]
        public void 役職変更(Post p1, Post p2)
        {
            var a = new Person(new NameVO("aaa", "bbb"), new BirthdayVO(100, 1, 1));
            a.UpdatePost(p1);
            var b = a.Clone();
            Assert.False(a.IsHigherPostThan(a));
            Assert.False(b.IsHigherPostThan(b));
            Assert.False(a.IsHigherPostThan(b));
            Assert.False(b.IsHigherPostThan(a));

            b.UpdatePost(p2);
            Assert.False(a.IsHigherPostThan(a));
            Assert.False(b.IsHigherPostThan(b));
            Assert.True(a.IsHigherPostThan(b));
            Assert.False(b.IsHigherPostThan(a));
        }
    }
}

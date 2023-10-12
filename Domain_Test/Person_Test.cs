using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test
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
            b.Name = new("cccc", "ddddd");
            Assert.True(a.HasSameIdentity(b));
        }

        [Fact]
        public void CopyTo()
        {
            var a = new Person(new NameVO("aaa", "bbb"), new BirthdayVO(100, 1, 1));
            var b = new Person(new NameVO("ccccc", "ddddd"), new BirthdayVO(100, 1, 1));
            a.CopyTo(b);
            Assert.False(a.HasSameIdentity(b));
            Assert.True(a.Name.Equals(b.Name));
            Assert.True(a.Birthday.Equals(b.Birthday));
        }
    }
}

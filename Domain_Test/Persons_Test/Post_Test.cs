using Entity.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test.Persons_Test
{
    public class Post_Test
    {
        const int MaxValue = 6;

        [Fact]
        public void 表示用文字列の定義漏れ監視()
        {
            var isException = false;
            var a = (Posts)MaxValue;
            try
            {
                var _ = a.GetDisplayText();
            }
            catch (ArgumentOutOfRangeException)
            {
                isException = true;
            }
            catch
            {
                Assert.Fail();
            }

            Assert.True(isException);
        }

        [Theory]
        [InlineData(Posts.Employee, Posts.Chief)]
        [InlineData(Posts.Chief, Posts.SectionChief)]
        [InlineData(Posts.SectionChief, Posts.Manager)]
        [InlineData(Posts.Manager, Posts.Director)]
        [InlineData(Posts.Director, Posts.President)]
        public void 比較(Posts low, Posts high)
        {
            Assert.True(low < high);
            Assert.False(low == high);
        }

        [Fact]
        public void 全リスト取得()
        {
            var list = PostsExtend.GetAllDispValueList();
            Assert.Equal(MaxValue, list.Count);
        }
    }

}

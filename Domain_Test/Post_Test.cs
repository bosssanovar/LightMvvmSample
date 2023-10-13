using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test
{
    public class Post_Test
    {
        const int MaxValue = 6;

        [Fact]
        public void 表示用文字列の定義漏れ監視()
        {
            var isException = false;
            var a = (Post)MaxValue;
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
        [InlineData(Post.Employee, Post.Chief)]
        [InlineData(Post.Chief, Post.SectionChief)]
        [InlineData(Post.SectionChief, Post.Manager)]
        [InlineData(Post.Manager, Post.Director)]
        [InlineData(Post.Director, Post.President)]
        public void 比較(Post low, Post high)
        {
            Assert.True(low < high);
            Assert.False(low == high);
        }

        [Fact]
        public void 全リスト取得()
        {
            var list = PostExtend.GetAllDispValueList();
            Assert.Equal(MaxValue, list.Count);
        }
    }

}

using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Entity_Test
{
    public class Birthday_Test
    {
        [Theory]
        [InlineData(0, 0, 0, true, typeof(ArgumentOutOfRangeException))]
        [InlineData(0, 1, 1, true, typeof(ArgumentOutOfRangeException))]
        [InlineData(1, 0, 1, true, typeof(ArgumentOutOfRangeException))]
        [InlineData(1, 1, 0, true, typeof(ArgumentOutOfRangeException))]
        [InlineData(1, 12, 1, false, null)]
        [InlineData(1, 13, 1, true, typeof(ArgumentOutOfRangeException))]
        [InlineData(1, 1, 31, false, null)]
        [InlineData(1, 1, 32, true, typeof(ArgumentOutOfRangeException))]
        [InlineData(1, 2, 28, false, null)]
        [InlineData(1, 2, 29, true, typeof(ArgumentOutOfRangeException))]
        [InlineData(4, 2, 28, false, null)]
        [InlineData(4, 2, 29, false, null)]
        [InlineData(4, 2, 30, true, typeof(ArgumentOutOfRangeException))]
        public void コンストラクタ＿例外(int year, int month, int day, bool isException, Type type)
        {
            bool isExcpt = false; ;

            try
            {
                var _ = new BirthdayVO(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                isExcpt = true;
                Assert.Equal(typeof(ArgumentOutOfRangeException), type);
            }
            catch
            {
                Assert.Fail();
            }

            Assert.Equal(isException, isExcpt);
        }

        [Theory]
        [InlineData(1, 1, 1, 2022)]
        [InlineData(1, 1, 2, 2021)]
        [InlineData(2023, 1, 1, 0)]
        [InlineData(2022, 12, 31, 0)]
        [InlineData(2022, 1, 2, 0)]
        [InlineData(2022, 1, 1, 1)]
        [InlineData(2021, 12, 31, 1)]
        public void 年齢取得(int year, int month, int day, int age)
        {
            var birthday = new BirthdayVO(year, month, day);
            var today = new DateTime(2023, 1, 1);

            Assert.Equal(birthday.GetAge(today), age);
        }

        [Theory]
        [InlineData(2023, 1, 2)]
        [InlineData(2023, 2, 1)]
        [InlineData(2024, 1, 1)]
        [InlineData(2024, 2, 1)]
        [InlineData(2024, 1, 2)]
        public void 年齢取得＿例外(int year, int month, int day)
        {
            var birthday = new BirthdayVO(year, month, day);

            try
            {
                birthday.GetAge(new DateTime(2023, 1, 1));
            }
            catch
            {
                Assert.True(true);
                return;
            }

            Assert.Fail();
        }

        [Theory]
        [InlineData(2023, 1, 1, "2023/1/1")]
        [InlineData(2023, 1, 2, "2023/1/2")]
        [InlineData(2023, 1, 10, "2023/1/10")]
        [InlineData(2023, 10, 1, "2023/10/1")]
        [InlineData(2023, 10, 2, "2023/10/2")]
        [InlineData(2023, 10, 10, "2023/10/10")]
        public void 生年月日文字列取得(int year, int month, int day, string text)
        {
            var birthday = new BirthdayVO(year, month, day);

            Assert.Equal(text, birthday.Text);
        }

        [Fact]
        public void EqualsMethod()
        {
            var a = new BirthdayVO(1000, 10, 10);
            var b = new BirthdayVO(1000, 10, 10);
            Assert.True(a.Equals(b));
            Assert.True(a == b);
            Assert.False(a != b);
        }

        [Fact]
        public void NotEqualsMethod()
        {
            var a = new BirthdayVO(1000, 10, 10);
            var b = new BirthdayVO(1001, 10, 10);
            Assert.False(a.Equals(b));
            Assert.False(a == b);
            Assert.True(a != b);
        }

        [Fact]
        public void クローン()
        {
            var a = new BirthdayVO(1000, 10, 10);
            var c = a.Clone();

            Assert.Equal(a, c);
            Assert.Equal(a.Year, c.Year);
            Assert.Equal(a.Month, c.Month);
            Assert.Equal(a.Day, c.Day);
            Assert.Equal(a.Text, c.Text);
            Assert.Equal(a.GetAge(DateTime.Today), c.GetAge(DateTime.Today));
        }
    }
}

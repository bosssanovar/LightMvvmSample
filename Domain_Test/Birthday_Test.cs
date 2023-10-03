using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain_Test
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
        public void コンストラクタ＿例外(int year, int month, int day, bool isException, Type type)
        {
            bool isExcpt = false; ;

            try
            {
                var _ = new Birthday(year, month, day);
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
            var birthday = new Birthday(year, month, day);
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
            var birthday = new Birthday(year, month, day);

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
    }
}

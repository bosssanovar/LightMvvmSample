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
        [InlineData(0, 0, 0, true, typeof(ArgumentException))]
        [InlineData(0, 1, 1, false, null)]
        [InlineData(1, 0, 1, true, typeof(ArgumentException))]
        [InlineData(1, 1, 0, true, typeof(ArgumentException))]
        [InlineData(1, 12, 1, false, null)]
        [InlineData(1, 13, 1, true, typeof(ArgumentException))]
        [InlineData(1, 1, 31, false, null)]
        [InlineData(1, 1, 32, true, typeof(ArgumentException))]
        public void コンストラクタ＿例外(int year, int month, int day, bool isException, Type type)
        {
            bool isExcpt = false; ;

            try
            {
                var _ = new Birthday(year, month, day);
            }
            catch (ArgumentException)
            {
                isExcpt = true;
                Assert.Equal(typeof(ArgumentException), type);
            }
            catch
            {
                Assert.Fail();
            }

            Assert.Equal(isException, isExcpt);
        }
    }
}

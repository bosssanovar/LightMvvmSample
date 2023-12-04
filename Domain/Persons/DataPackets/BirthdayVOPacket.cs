using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Persons.DataPackets
{
    /// <summary>
    /// <see cref="BirthdayVO"/>のデータパケット
    /// </summary>
    public class BirthdayVOPacket
    {
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// <see cref="BirthdayVO"/>インスタンスを取得します。
        /// </summary>
        /// <returns><see cref="BirthdayVO"/>インスタンス</returns>
        public BirthdayVO Get()
        {
            return new(Year, Month, Day);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Persons.DataPackets
{
    /// <summary>
    /// <see cref="NameVO"/>のデータパケット
    /// </summary>
    public class NameVOPacket
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string First { get; set; }

        /// <summary>
        /// 苗字
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// <see cref="NameVO"/>インスタンスを取得します。
        /// </summary>
        /// <returns><see cref="NameVO"/>インスタンス</returns>
        public NameVO Get()
        {
            return new(Family, First);
        }
    }
}

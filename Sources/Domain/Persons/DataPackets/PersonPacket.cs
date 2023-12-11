using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Persons.DataPackets
{
    /// <summary>
    /// <see cref="Person"/>のデータパケット
    /// </summary>
    public class PersonPacket
    {
        /// <summary>
        /// 識別子
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public NameVOPacket Name { get; set; }

        /// <summary>
        /// 誕生日
        /// </summary>
        public BirthdayVOPacket Birthday { get; set; }

        /// <summary>
        /// <see cref="Person"/>インスタンスを取得します。
        /// </summary>
        /// <returns><see cref="Person"/>インスタンス</returns>
        public Person Get()
        {
            return new(Identifier, Name.Get(), Birthday.Get());
        }
    }
}

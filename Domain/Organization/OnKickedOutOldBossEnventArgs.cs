using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Organization
{
    /// <summary>
    /// 組織長が交代して、前の組織長の所属が未定となった場合に発行されるイベントデータ
    /// </summary>
    public class OnKickedOutOldBossEnventArgs
    /* TODO K.I : 削除*/
    {
        /// <summary>
        /// 未定となった前の組織長を取得します。
        /// </summary>
        public Person OldBoss { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="oldBoss">前の組織長</param>
        public OnKickedOutOldBossEnventArgs(Person oldBoss)
        {
            OldBoss = oldBoss;
        }
    }
}

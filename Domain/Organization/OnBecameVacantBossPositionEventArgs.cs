using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Organization
{
    /// <summary>
    /// 組織長ポストが空欄となった場合に発行されるイベントデータ
    /// </summary>
    public class OnBecameVacantBossPositionEventArgs
    /* TODO K.I : 削除*/
    {
        /// <summary>
        /// 組織長ポストが空欄になった組織
        /// </summary>
        public OrganizationBase Organization { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="organization">組織長ポストが空欄になった組織</param>
        public OnBecameVacantBossPositionEventArgs(OrganizationBase organization)
        {
            Organization = organization;
        }
    }
}

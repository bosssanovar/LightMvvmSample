using Entity.Organization;
using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.EditWindow
{
    /// <summary>
    /// <see cref="EditWindow"/>の入力完了イベントデータクラス
    /// </summary>
    public class OnEditWindowCompletedEventArgs
    {
        /// <summary>
        /// 変更対象社員を取得します。
        /// </summary>
        public Person Person { get; }

        /// <summary>
        /// 所属組織を取得します。
        /// </summary>
        public OrganizationBase Organization { get; }

        /// <summary>
        /// 組織長かどうかを取得します。
        /// </summary>
        public bool IsBoss { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="person">変更対象社員</param>
        /// <param name="organization">所属組織</param>
        /// <param name="isBoss">組織長かどうか</param>
        public OnEditWindowCompletedEventArgs(Person person, OrganizationBase organization, bool isBoss)
        {
            Person = person;
            Organization = organization;
            IsBoss = isBoss;
        }
    }
}

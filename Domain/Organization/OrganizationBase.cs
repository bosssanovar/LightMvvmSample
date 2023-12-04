using Entity.Organization.DataPackets;
using Entity.Persons;
using Entity.Service.OrganizationVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Organization
{
    /// <summary>
    /// 組織の抽象クラス
    /// </summary>
    public abstract class OrganizationBase
    {
        private static int counter = 0;

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 識別子
        /// </summary>
        internal int Identifier { get; }

        /// <summary>
        /// 直属社員
        /// </summary>
        protected List<Person> Members { get; } = new List<Person>();

        /// <summary>
        /// 組織長
        /// </summary>
        internal Person? Boss { get; private set; }

        /// <summary>
        /// 組織名称
        /// </summary>
        protected OrganizationNameVO Name { get; private set; }

        /// <summary>
        /// 組織ランク
        /// </summary>
        internal Ranks Rank { get; private set; }

        /// <summary>
        /// 組織名を取得します。
        /// </summary>
        public string DisplayName
        {
            get
            {
                var ret = new StringBuilder();
                ret.Append(Name.Name);
                ret.Append(Rank.GetDisplayText());
                return ret.ToString();
            }
        }

        /// <summary>
        /// 直属社員の数
        /// </summary>
        public int DirectEmployeeCount
        {
            get
            {
                return Members.Count;
            }
        }

        /// <summary>
        /// debuug
        /// </summary>
        /// <returns>debug</returns>
        public string DebugPring()
        {
            static string GetIndent(Ranks rank)
            {
                return rank switch
                {
                    Ranks.Campany => string.Empty,
                    Ranks.Department => "    ",
                    Ranks.Section => "        ",
                    Ranks.Team => "            ",
                    _ => throw new NotImplementedException(),
                };
            }

            string indent = GetIndent(Rank);

            var sb = new StringBuilder();
            sb.Append(indent + DisplayName);
            sb.Append(", ");
            sb.Append("ボス : " + (Boss?.Name.FullName ?? "【長不在】"));
            sb.Append(", ");
            sb.AppendLine(
                Members.Select(x => x.Name.FullName)
                .DefaultIfEmpty("【直属メンバーなし】")
                .Aggregate((a, b) => a + ", " + b));

            return sb.ToString();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">組織名</param>
        /// <param name="rank">組織ランク</param>
        public OrganizationBase(OrganizationNameVO name, Ranks rank)
        {
            Identifier = counter++;
            Name = name;
            Rank = rank;
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="original">コピー元</param>
        protected OrganizationBase(OrganizationBase original)
        {
            Identifier = original.Identifier;
            Members = original.Members;
            Name = original.Name;
            Rank = original.Rank;
            Boss = original.Boss;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        /// <summary>
        /// <see cref="IOrganizationVisitor"/>を受け入れる抽象メソッド
        /// </summary>
        /// <param name="visitor"><see cref="IOrganizationVisitor"/>インスタンス</param>
        internal abstract void Accept(IOrganizationVisitor visitor);

        /// <summary>
        /// 直属社員を追加する。
        /// </summary>
        /// <param name="member">追加する社員</param>
        internal void AddMember(Person member)
        {
            if (IsContainDirectEmployee(member))
            {
                return;
            }

            Members.Add(member);
        }

        /// <summary>
        /// 直属社員を削除します。
        /// </summary>
        /// <param name="member">削除する写真</param>
        /// <returns>指定の社員が削除されたらtrue</returns>
        internal bool RemoveMember(Person member)
        {
            if (!IsContainDirectEmployee(member))
            {
                return false;
            }

            Members.RemoveAll(x => x.SameIdentityAs(member));

            return true;
        }

        /// <summary>
        /// 直属社員を全て削除します。
        /// </summary>
        internal void RemoveAllMember()
        {
            Members.Clear();
        }

        /// <summary>
        /// 指定社員が直属社員として属しているか判定します。
        /// </summary>
        /// <param name="member">確認対象社員</param>
        /// <returns>確認対象社員が所属していればtrue</returns>
        internal bool IsContainDirectEmployee(Person member) => Members.Any(x => x.SameIdentityAs(member));

        /// <summary>
        /// 指定社員が組織長かどうかを判定します。
        /// </summary>
        /// <param name="boss">確認対象社員</param>
        /// <returns>組織長ならtrue</returns>
        internal bool IsBoss(Person boss)
        {
            if (Boss is null)
            {
                return false;
            }

            if (Boss.SameIdentityAs(boss))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 組織長を設定します。
        /// 組織長が既に設定されている場合には元組織長として所属が無くなる。
        /// </summary>
        /// <returns>元の組織長</returns>
        /// <param name="newBoss">新しい組織長</param>
        internal Person? SetBoss(Person newBoss)
        {
            var oldBoss = Boss;

            Boss = newBoss;

            return oldBoss;
        }

        /// <summary>
        /// ボスが離職します。
        /// </summary>
        internal void RemoveBoss()
        {
            Boss = null;
        }

        /// <summary>
        /// 同一性を判定します。
        /// </summary>
        /// <param name="target">ターゲット</param>
        /// <returns>同一性を有している場合 true</returns>
        internal bool SameIdentityAs(OrganizationBase target)
        {
            return Identifier == target.Identifier;
        }

        /// <summary>
        /// データパケットを出力します。
        /// </summary>
        /// <returns>データパケット</returns>
        internal OrganizationBasePacket ExportPacket()
        {
            return new()
            {
                Identifier = Identifier,
                MemberIds = Members.Select(x => x.Identifier).ToList(),
                BossId = Boss?.Identifier ?? Guid.Empty,
            };
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}

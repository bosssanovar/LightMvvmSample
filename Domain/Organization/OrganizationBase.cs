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
    internal abstract class OrganizationBase
    {
        private static int counter = 0;

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        /// <summary>
        /// 識別子
        /// </summary>
        protected int Identifier { get; }

        /// <summary>
        /// 直属社員
        /// </summary>
        protected List<Person> Members { get; } = new List<Person>();

        /// <summary>
        /// 組織長
        /// </summary>
        protected Person Boss { get; private set; } = new(new(string.Empty, string.Empty), new(1, 1, 1));

        /// <summary>
        /// 組織名称
        /// </summary>
        protected OrganizationNameVO Name { get; private set; }

        /// <summary>
        /// 組織ランク
        /// </summary>
        public Lanks Lank { get; private set; }

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 組織名を取得します。
        /// </summary>
        public string DisplayName
        {
            get
            {
                var ret = new StringBuilder();
                ret.Append(Name.Name);
                ret.Append(' ');
                ret.Append(Lank.GetDisplayText());
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

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">組織名</param>
        /// <param name="lank">組織ランク</param>
        public OrganizationBase(OrganizationNameVO name, Lanks lank)
        {
            Identifier = counter++;
            Name = name;
            Lank = lank;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <param name="name">組織名称</param>
        /// <param name="lank">組織ランク</param>
        /// <param name="boss">組織長</param>
        protected OrganizationBase(int identifier, OrganizationNameVO name, Lanks lank, Person boss)
        {
            Identifier = identifier;
            Name = name;
            Lank = lank;
            Boss = boss;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 複製します。
        /// </summary>
        /// <returns>複製インスタンス</returns>
        public abstract OrganizationBase Clone();

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
        /// 指定社員が直属社員として属しているか判定します。
        /// </summary>
        /// <param name="member">確認対象社員</param>
        /// <returns>確認対象社員が所属していればtrue</returns>
        internal bool IsContainDirectEmployee(Person member)
        {
            foreach (Person m in Members)
            {
                if (m.SameIdentityAs(member))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 指定社員が組織長かどうかを判定します。
        /// </summary>
        /// <param name="boss">確認対象社員</param>
        /// <returns>組織長ならtrue</returns>
        internal bool IsBoss(Person boss)
        {
            if(Boss.SameIdentityAs(boss))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 組織長を変更します。
        /// </summary>
        /// <param name="newBoss">新しい組織長</param>
        /// <returns>元の組織長</returns>
        internal Person ChangeBoss(Person newBoss)
        {
            var ret = Boss;

            Boss = newBoss;

            return ret;
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

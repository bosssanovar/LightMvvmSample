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
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        /// <summary>
        /// 識別子
        /// </summary>
        protected Guid Identifier { get; }

        /// <summary>
        /// 直属社員
        /// </summary>
        protected List<Person> Members { get; } = new List<Person>();

        /// <summary>
        /// 組織長
        /// </summary>
        protected Person Boss { get; private set; }

        /// <summary>
        /// 組織名称
        /// </summary>
        protected OrganizationNameVO Name { get; private set; }

        /// <summary>
        /// 組織ランク
        /// </summary>
        protected Lanks Lank { get; private set; }

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
        /// <param name="boss">組織長</param>
        public OrganizationBase(OrganizationNameVO name, Lanks lank, Person boss)
        {
            Identifier = Guid.NewGuid();
            Name = name;
            Lank = lank;
            Boss = boss;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <param name="name">組織名称</param>
        /// <param name="lank">組織ランク</param>
        /// <param name="boss">組織長</param>
        protected OrganizationBase(Guid identifier, OrganizationNameVO name, Lanks lank, Person boss)
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

            Members.Remove(member);

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
                if (m == member)
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
            if(Boss == boss)
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

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        /// <summary>
        /// 等価性を判定します。
        /// </summary>
        /// <param name="obj">比較対象</param>
        /// <returns>等価の場合はtrue</returns>
        public override bool Equals(object? obj)
        {
            //objがnullか、型が違うときは、等価でない
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            //NumberとMessageで比較する
            var c = (OrganizationBase)obj;
            return Identifier == c.Identifier;
        }

        /// <summary>
        /// ハッシュコードを取得します。
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        /// <summary>
        /// == のオーバーライド
        /// </summary>
        /// <param name="c1">値１</param>
        /// <param name="c2">値2</param>
        /// <returns>等価の場合true</returns>
        public static bool operator ==(OrganizationBase c1, OrganizationBase c2)
        {
            //nullの確認（構造体のようにNULLにならない型では不要）
            //両方nullか（参照元が同じか）
            //(c1 == c2)とすると、無限ループ
            if (ReferenceEquals(c1, c2))
            {
                return true;
            }

            //どちらかがnullか
            //(c1 == null)とすると、無限ループ
            if (c1 is null || c2 is null)
            {
                return false;
            }

            return c1.Identifier == c2.Identifier;
        }

        /// <summary>
        /// != のオーバーライド
        /// </summary>
        /// <param name="c1">値1</param>
        /// <param name="c2">値2</param>
        /// <returns>等価でなければtrue</returns>
        public static bool operator !=(OrganizationBase c1, OrganizationBase c2)
        {
            return !(c1 == c2);
            //(c1 != c2)とすると、無限ループ
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}

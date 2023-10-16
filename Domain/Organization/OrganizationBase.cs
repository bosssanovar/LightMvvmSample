using Entity.Persons;
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

        private readonly Guid _identifier;
        private readonly List<Person> _members;
        private Person _boss;
        private OrganizationNameVO _name;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 組織名を取得します。
        /// </summary>
        public string Name
        {
            get
            {
                return _name.Name;
            }
        }

        /// <summary>
        /// 直属社員の数
        /// </summary>
        public int DirectEmployeeCount
        {
            get
            {
                return _members.Count;
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
        /// <param name="boss">組織長</param>
        public OrganizationBase(OrganizationNameVO name, Person boss)
        {
            _identifier = Guid.NewGuid();
            _name = name;
            _boss = boss;
            _members = new List<Person>();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 直属社員を追加する。
        /// </summary>
        /// <param name="member">追加する社員</param>
        public void AddMember(Person member)
        {
            if (IsContainMember(member))
            {
                return;
            }

            _members.Add(member);
        }

        /// <summary>
        /// 直属社員を削除します。
        /// </summary>
        /// <param name="member">削除する写真</param>
        public void RemoveMember(Person member)
        {
            if (!IsContainMember(member))
            {
                return;
            }

            _members.Remove(member);
        }

        /// <summary>
        /// 指定社員が直属社員または組織長として属しているか判定します。
        /// </summary>
        /// <param name="member">確認対象社員</param>
        /// <returns>確認対象社員が所属していればtrue</returns>
        public bool IsContainMember(Person member)
        {
            if (_boss == member)
            {
                return true;
            }

            foreach (Person m in _members)
            {
                if (m == member)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 組織長を変更します。
        /// </summary>
        /// <param name="newBoss">新しい組織長</param>
        /// <returns>元の組織長</returns>
        public Person ChangeBoss(Person newBoss)
        {
            var ret = _boss;

            _boss = newBoss;

            return ret;
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

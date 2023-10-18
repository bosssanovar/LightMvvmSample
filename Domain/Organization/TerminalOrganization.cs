﻿using Entity.Persons;
using Entity.Service.OrganizationVisitor;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Entity_Test")]

namespace Entity.Organization
{
    /// <summary>
    /// 末端組織クラス
    /// </summary>
    internal class TerminalOrganization : OrganizationBase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">社名</param>
        /// <param name="boss">社長</param>
        public TerminalOrganization(OrganizationNameVO name, Person boss)
            : base(name, Lanks.Team, boss)
        {
        }

        private TerminalOrganization(Guid identifier, OrganizationNameVO name, Person boss)
            : base(identifier, name, Lanks.Team, boss)
        {
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 複製します。
        /// </summary>
        /// <returns>複製インスタンス</returns>
        public override OrganizationBase Clone()
        {
            return new TerminalOrganization(Identifier, Name.Clone(), Boss.Clone());
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        /// <summary>
        /// <see cref="IOrganizationVisitor"/>を受け入れる抽象メソッド
        /// </summary>
        /// <param name="visitor"><see cref="IOrganizationVisitor"/>インスタンス</param>
        public override void Accept(IOrganizationVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}

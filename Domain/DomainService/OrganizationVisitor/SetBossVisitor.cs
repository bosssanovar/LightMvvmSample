﻿using Entity.Organization;
using Entity.Persons;
using Entity.Service.OrganizationVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DomainService.OrganizationVisitor
{
    /// <summary>
    ///  所属長を設定するVisitor
    /// </summary>
    internal class SetBossVisitor : IOrganizationVisitor
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly Person _newBoss;
        private readonly OrganizationBase _targetOrganization;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 所属長設定に成功したかを判断する。
        /// </summary>
        public bool IsSetted { get; private set; } = false;

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 前の組織長がはじき出されたイベント
        /// </summary>
        public event Action<OnKickedOutOldBossEnventArgs> OnKickedOutOldBoss;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="newBoss">新しい組織長</param>
        /// <param name="targetOrganization">ターゲット組織</param>
        public SetBossVisitor(Person newBoss, OrganizationBase targetOrganization)
        {
            _newBoss = newBoss;
            _targetOrganization = targetOrganization;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 実行します。
        /// </summary>
        /// <param name="target">ターゲット</param>
        public void Visit(OrganizationBase target)
        {
            if(target.SameIdentityAs(_targetOrganization))
            {
                target.OnKickedOutOldBoss += Target_OnKickedOutOldBoss;
                target.SetBoss(_newBoss);
                target.OnKickedOutOldBoss -= Target_OnKickedOutOldBoss;

                IsSetted = true;
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void Target_OnKickedOutOldBoss(OnKickedOutOldBossEnventArgs args)
        {
            OnKickedOutOldBoss?.Invoke(args);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}

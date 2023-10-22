using Entity.Organization;
using Entity.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Service.OrganizationVisitor
{
    /// <summary>
    /// 直属と組織長の堺無く社員を削除するVisitorクラス
    /// </summary>
    internal class RemovePersonVisitor : IOrganizationVisitor
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly Person _targetPerson;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 削除が完了したかを取得します。
        /// </summary>
        public bool IsRemoved { get; private set; } = false;

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 組織長ポストが空欄となった場合に発行されるイベント
        /// </summary>
        internal event Action<OnBecameVacantBossPositionEventArgs> OnBecameVacantBossPosition;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetPerson">削除する直属社員</param>
        public RemovePersonVisitor(Person targetPerson)
        {
            _targetPerson = targetPerson;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 処理します。
        /// </summary>
        /// <param name="target">ターゲット</param>
        public void Visit(OrganizationBase target)
        {
            if (IsRemoved)
            {
                return;
            }

            if (target.IsContainDirectEmployee(_targetPerson))
            {
                target.RemoveMember(_targetPerson);
                IsRemoved = true;
            }
            else if (target.IsBoss(_targetPerson))
            {
                target.OnBecameVacantBossPosition += Target_OnBecameVacantBossPosition;
                target.RemoveBoss();
                IsRemoved = true;
                target.OnBecameVacantBossPosition -= Target_OnBecameVacantBossPosition;
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void Target_OnBecameVacantBossPosition(OnBecameVacantBossPositionEventArgs args)
        {
            OnBecameVacantBossPosition?.Invoke(args);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}

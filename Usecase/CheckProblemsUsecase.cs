using Entity.Organization;
using Entity.Persons;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase.Sub;

namespace Usecase
{
    /// <summary>
    /// 組織人員問題状況を取得するユースケース
    /// </summary>
    public class CheckProblemsUsecase
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly ICheckProblems _checkProblems;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 組織人員問題発生時イベント
        /// </summary>
        public event Action<OnArisedProblemsEventArgs> OnArisedProblems;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="checkProblems">組織人員問題検出</param>
        public CheckProblemsUsecase(ICheckProblems checkProblems)
        {
            _checkProblems = checkProblems;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 社員を組織にアサインします。
        /// </summary>
        public void Check()
        {
            var checkResult = _checkProblems.Check();
            if(checkResult.Count > 0)
            {
                OnArisedProblems(new(checkResult, _checkProblems.UnAssignedPersons, _checkProblems.NoBossOrganizaiotns));
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

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

using Entity.Organization;
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
    ///  無所属社員一覧を取得するVisitor
    /// </summary>
    internal class GetUnAssignedPersonsVisitor : IOrganizationVisitor
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 無所属社員一覧を取得します。
        /// </summary>
        public List<Person> UnAssignedPersons { get; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="persons">社員一覧</param>
        public GetUnAssignedPersonsVisitor(List<Person> persons)
        {
            UnAssignedPersons = persons.Select(x => x.Clone()).ToList();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 実行する
        /// </summary>
        /// <param name="target">ターゲット</param>
        public void Visit(OrganizationBase target)
        {
            List<Person> assignedPersons = GetAssignedPersons(target);

            RemoveAssignedPersons(assignedPersons);
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private List<Person> GetAssignedPersons(OrganizationBase target)
        {
            var assignedPersons = new List<Person>();
            foreach (var p in UnAssignedPersons)
            {
                if (target.IsBoss(p))
                {
                    assignedPersons.Add(p);
                }
                else if (target.IsContainDirectEmployee(p))
                {
                    assignedPersons.Add(p);
                }
            }

            return assignedPersons;
        }

        private void RemoveAssignedPersons(List<Person> assignedPersons)
        {
            foreach (var p in assignedPersons)
            {
                UnAssignedPersons.Remove(p);
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}

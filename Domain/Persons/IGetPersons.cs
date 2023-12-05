using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Persons
{
    /// <summary>
    /// 社員一覧を取得するインターフェース
    /// </summary>
    public interface IGetPersons
    {
        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報リストを取得します。
        /// </summary>
        public ReadOnlyCollection<Person> Persons { get; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}

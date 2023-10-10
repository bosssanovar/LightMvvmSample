using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 名前クラス
    /// </summary>
    public class NameVO
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly string _family;

        private readonly string _first;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// フルネームを取得します。
        /// </summary>
        public string FullName => $"{_family} {_first}";

        /// <summary>
        /// 苗字を取得します。
        /// </summary>
        public string Family => _family;

        /// <summary>
        /// 名前を取得します。
        /// </summary>
        public string First => _first;

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="family">苗字</param>
        /// <param name="first">名前</param>
        public NameVO(string family, string first)
        {
            _family = family ?? throw new ArgumentNullException(nameof(family));
            _first = first ?? throw new ArgumentNullException(nameof(first));
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// パラメータが有効かを判定します。
        /// </summary>
        /// <param name="family">苗字</param>
        /// <param name="first">名前</param>
        /// <returns>有効ならtrue</returns>
        public static bool IsValid(string family, string first)
        {
            if (family == null)
            {
                return false;
            }

            if (first == null)
            {
                return false;
            }

            return true;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}

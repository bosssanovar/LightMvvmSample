using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Persons.DataPackets;

namespace Entity.Persons
{
    /// <summary>
    /// 名前クラス
    /// </summary>
    /// <param name="Family">苗字</param>
    /// <param name="First">名前</param>
    public record NameVO(string Family, string First)
    {
        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// フルネームを取得します。
        /// </summary>
        public string FullName => $"{Family} {First}";

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

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

        /// <summary>
        /// データパケットを出力します。
        /// </summary>
        /// <returns>データパケット</returns>
        internal NameVOPacket ExportPacket() => new() { Family = Family, First = First };

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}

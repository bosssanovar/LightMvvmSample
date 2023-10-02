using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sample
{
    /// <summary>
    /// Sampleの文字列ValueObject
    /// </summary>
    public class SampleTextVO
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly string _text;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 文字列を取得します。
        /// </summary>
        public string Text { get => _text; }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="text">文字列</param>
        public SampleTextVO(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("null", nameof(text));
            }

            if (text.Length > 10)
            {
                throw new ArgumentException("10文字以上はNG", nameof(text));
            }

            _text = text;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// 入力値を検証する
        /// </summary>
        /// <param name="text">入力文字列</param>
        /// <returns>不適合の場合false</returns>
        public static bool IsValid(string text)
        {
            if(text == null)
            {
                return false;
            }

            if(text.Length > 10)
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

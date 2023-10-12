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

        /// <summary>
        /// インスタンスを複製します。
        /// </summary>
        /// <returns>複製したインスタンス</returns>
        public NameVO Clone() => new(_family, _first);

        /// <summary>
        /// 等価性を判定します。
        /// </summary>
        /// <param name="obj">比較対象</param>
        /// <returns>等価の場合はtrue</returns>
        public override bool Equals(object? obj)
        {
            //objがnullか、型が違うときは、等価でない
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            //NumberとMessageで比較する
            var c = (NameVO)obj;
            return (_family == c._family) && (_first == c._first);
        }

        /// <summary>
        /// ハッシュコードを取得します。
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode()
        {
            return _family.GetHashCode() ^ _first.GetHashCode();
        }

        /// <summary>
        /// == のオーバーライド
        /// </summary>
        /// <param name="c1">値１</param>
        /// <param name="c2">値2</param>
        /// <returns>等価の場合true</returns>
        public static bool operator ==(NameVO c1, NameVO c2)
        {
            //nullの確認（構造体のようにNULLにならない型では不要）
            //両方nullか（参照元が同じか）
            //(c1 == c2)とすると、無限ループ
            if (object.ReferenceEquals(c1, c2))
            {
                return true;
            }

            //どちらかがnullか
            //(c1 == null)とすると、無限ループ
            if ((c1 is null) || (c2 is null))
            {
                return false;
            }

            return (c1._family == c2._family) && (c1._first == c2._first);
        }

        /// <summary>
        /// != のオーバーライド
        /// </summary>
        /// <param name="c1">値1</param>
        /// <param name="c2">値2</param>
        /// <returns>等価でなければtrue</returns>
        public static bool operator !=(NameVO c1, NameVO c2)
        {
            return !(c1 == c2);
            //(c1 != c2)とすると、無限ループ
        }
        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}

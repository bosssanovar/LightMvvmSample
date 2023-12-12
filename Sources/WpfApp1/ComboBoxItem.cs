using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    /// <summary>
    /// コンボボックス項目の値、表示文字列定義クラス
    /// </summary>
    /// <typeparam name="T">コンボボックス内部値</typeparam>
    public class ComboBoxItem<T>
    {
        /// <summary>
        /// 表示文字列を取得します。
        /// </summary>
        public string Disp { get; }

        /// <summary>
        /// 内部値を取得します。
        /// </summary>
        public T Val { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="disp">表示文字列</param>
        /// <param name="val">内部値</param>
        public ComboBoxItem(string disp, T val)
        {
            Disp = disp;
            Val = val;
        }
    }
}

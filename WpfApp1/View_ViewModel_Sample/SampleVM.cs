using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{

    /// <summary>
    /// MainWindowのViewModel責務コード記述部
    /// しかし実体は、Viewのコードビハインド（partial）。
    /// View操作を柔軟にするために、ViewとViewModelを一体化した。（MVVMの軽量カスタム）
    /// </summary>
    public partial class Sample
    {
        #region Fields

        #endregion

        #region Constants

        #endregion

        #region Properties

        public string Text { get => "AAAAAAAAAa"; }

        #endregion

        #region Constructors

        /// <summary>
        /// ViewModel部コンストラクタ
        /// </summary>
        private void InitializeViewModel()
        {
        }

        #endregion

        #region Methods

        #endregion
    }
}

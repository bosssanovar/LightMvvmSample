using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace WpfApp1
{
    /// <summary>
    /// MainWindowのViewModel責務コード記述部
    /// しかし実体は、Viewのコードビハインド（partial）。
    /// View操作を柔軟にするために、ViewとViewModelを一体化した。（MVVMの軽量カスタム）
    /// </summary>
    public partial class Sample : INotifyPropertyChanged
    {
        #region Fields

        private Domain.Sample.Sample _model;

        #endregion

        #region Constants

        #endregion

        #region Properties

        /// <summary>
        /// サンプルテキスト
        /// </summary>
        public string Text { get => _model.Text; }

        /// <summary>
        /// 不要だが、バインドした際のメモリリーク対策のため追加 INotifyPropertyChanged
        /// </summary>
#pragma warning disable CS0067 // イベント 'Sample.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'Sample.PropertyChanged' は使用されていません

        #endregion

        #region Constructors

        /// <summary>
        /// ViewModel部コンストラクタ
        /// </summary>
        private void InitializeViewModel()
        {
            _model = new();
        }

        #endregion

        #region Methods

        #endregion
    }
}

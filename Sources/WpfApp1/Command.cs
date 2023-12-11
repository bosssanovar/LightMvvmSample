using System;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// コマンドクラス
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// コマンドを実行するかどうかに影響するような変更があった場合に発生します。
        /// </summary>
#pragma warning disable CS0067 // イベント 'Command.CanExecuteChanged' は使用されていません
        public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067 // イベント 'Command.CanExecuteChanged' は使用されていません

        private readonly Action action;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="action">コマンドアクション</param>
        public Command(Action action)
        {
            this.action = action;
        }

        /// <inheritdoc/>
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public void Execute(object? parameter)
        {
            action?.Invoke();
        }
    }
}

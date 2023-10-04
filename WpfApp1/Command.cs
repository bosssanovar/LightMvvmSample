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
        public event EventHandler? CanExecuteChanged;

        private readonly Action action;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="action">コマンドアクション</param>
        public Command(Action action)
        {
            this.action = action;
        }

        /// <summary>
        /// 実行可能かを判定します。
        /// </summary>
        /// <param name="parameter">パラメータ</param>
        /// <returns>実行可能ならtrue</returns>
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        /// <summary>
        /// コマンドアクションを実行します。
        /// </summary>
        /// <param name="parameter">パラメータ</param>
        public void Execute(object? parameter)
        {
            action?.Invoke();
        }
    }
}

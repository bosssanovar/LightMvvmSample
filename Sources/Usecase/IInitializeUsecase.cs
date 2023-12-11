namespace Usecase
{
    /// <summary>
    /// データを初期化するユースケースを提供するインターフェース
    /// </summary>
    public interface IInitializeUsecase
    {
        /// <summary>
        /// 設置値を初期化します。
        /// </summary>
        void Initialize();
    }
}
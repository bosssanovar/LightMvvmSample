namespace Usecase
{
    /// <summary>
    /// 組織人員問題状況を取得するユースケース
    /// </summary>
    public interface ICheckProblemsUsecase
    {
        /// <summary>
        /// 組織人員問題発生時イベント
        /// </summary>
        event Action<OnArisedProblemsEventArgs> OnArisedProblems;

        /// <summary>
        /// 社員を組織にアサインします。
        /// </summary>
        void Check();
    }
}
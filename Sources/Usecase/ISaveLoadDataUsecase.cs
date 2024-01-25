namespace Usecase
{
    /// <summary>
    /// データを保存するユースケース
    /// </summary>
    public interface ISaveLoadDataUsecase
    {
        /// <summary>
        /// 組織人員問題を通知します。
        /// </summary>
        event Action<OnArisedProblemsEventArgs> OnArisedProblems;

        /// <summary>
        /// 組織構成が変更されたことを通知します。
        /// </summary>
        event Action OnOrganizationUpdated;

        /// <summary>
        /// 社員リストが更新されたことを通知します。
        /// </summary>
        event Action OnPeopleUpdated;

        /// <summary>
        /// データをファイルから読み込みます。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Load(string path);

        /// <summary>
        /// データをファイルに保存します。
        /// </summary>
        /// <param name="path">保存パス</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Save(string path);
    }
}
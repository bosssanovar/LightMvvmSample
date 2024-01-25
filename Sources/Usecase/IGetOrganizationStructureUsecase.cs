namespace Usecase
{
    /// <summary>
    /// 組織構造を取得するユースケースを提供します。
    /// </summary>
    public interface IGetOrganizationStructureUsecase
    {
        /// <summary>
        /// 組織構造を取得します。
        /// </summary>
        /// <returns>組織情報</returns>
        string GetOrganizationSructureInfo();
    }
}
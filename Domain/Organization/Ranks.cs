using Entity.Persons;

namespace Entity.Organization
{
    /// <summary>
    /// 組織ランクの列挙子
    /// </summary>
    public enum Ranks
    {
        /// <summary>
        /// 社
        /// </summary>
        Campany,

        /// <summary>
        /// 部
        /// </summary>
        Department,

        /// <summary>
        /// 課
        /// </summary>
        Section,

        /// <summary>
        /// チーム
        /// </summary>
        Team,

        /// <summary>
        /// 管理外
        /// </summary>
        Outside,
    }

    /// <summary>
    /// <see cref="Ranks"/>列挙子の拡張メソッド
    /// </summary>
    public static partial class RanksExtend
    {
        /// <summary>
        /// 表示用文字列を取得します。
        /// </summary>
        /// <param name="value"><see cref="Ranks"/>列挙子</param>
        /// <returns>表示用文字列</returns>
        public static string GetDisplayText(this Ranks value)
        {
            return value switch
            {
                Ranks.Campany => "社",
                Ranks.Department => "部",
                Ranks.Section => "課",
                Ranks.Team => "チーム",
                Ranks.Outside => string.Empty,
                _ => throw new ArgumentOutOfRangeException(nameof(value), "未定義です"),
            };
        }

        /// <summary>
        /// 表示用文字列を取得します。
        /// </summary>
        /// <param name="value"><see cref="Ranks"/>列挙子</param>
        /// <returns>表示用文字列</returns>
        public static Posts GetBossPost(this Ranks value)
        {
            return value switch
            {
                Ranks.Campany => Posts.President,
                Ranks.Department => Posts.Manager,
                Ranks.Section => Posts.SectionChief,
                Ranks.Team => Posts.Chief,
                Ranks.Outside => Posts.Employee,
                _ => throw new ArgumentOutOfRangeException(nameof(value), "未定義です"),
            };
        }
    }
}

using Entity.Persons;

namespace Entity.Organization
{
    /// <summary>
    /// 組織ランクの列挙子
    /// </summary>
    public enum Lanks
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
    }

    /// <summary>
    /// <see cref="Lanks"/>列挙子の拡張メソッド
    /// </summary>
    public static partial class LanksExtend
    {
        /// <summary>
        /// 表示用文字列を取得します。
        /// </summary>
        /// <param name="value"><see cref="Lanks"/>列挙子</param>
        /// <returns>表示用文字列</returns>
        public static string GetDisplayText(this Lanks value)
        {
            return value switch
            {
                Lanks.Campany => "社",
                Lanks.Department => "部",
                Lanks.Section => "課",
                Lanks.Team => "チーム",
                _ => throw new ArgumentOutOfRangeException(nameof(value), "未定義です"),
            };
        }

        /// <summary>
        /// 表示用文字列を取得します。
        /// </summary>
        /// <param name="value"><see cref="Lanks"/>列挙子</param>
        /// <returns>表示用文字列</returns>
        public static Posts GetBossPost(this Lanks value)
        {
            return value switch
            {
                Lanks.Campany => Posts.President,
                Lanks.Department => Posts.Manager,
                Lanks.Section => Posts.SectionChief,
                Lanks.Team => Posts.Chief,
                _ => throw new ArgumentOutOfRangeException(nameof(value), "未定義です"),
            };
        }
    }
}

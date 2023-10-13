using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Persons
{
    /// <summary>
    /// 役職　列挙子
    /// </summary>
    public enum Post : int
    {
        /// <summary>
        /// 従業員
        /// </summary>
        Employee = 0,

        /// <summary>
        /// 主任
        /// </summary>
        Chief = 1,

        /// <summary>
        /// 課長
        /// </summary>
        SectionChief = 2,

        /// <summary>
        /// 部長
        /// </summary>
        Manager = 3,

        /// <summary>
        /// 役員
        /// </summary>
        Director = 4,

        /// <summary>
        /// 社長
        /// </summary>
        President = 5,
    }

    /// <summary>
    /// <see cref="Post"/>列挙子の拡張メソッド
    /// </summary>
    public static partial class PostExtend
    {
        /// <summary>
        /// 表示用文字列を取得します。
        /// </summary>
        /// <param name="post"><see cref="Post"/>列挙子</param>
        /// <returns>表示用文字列</returns>
        public static string GetDisplayText(this Post post)
        {
            return post switch
            {
                Post.Employee => "従業員",
                Post.Chief => "主任",
                Post.SectionChief => "課長",
                Post.Manager => "部長",
                Post.Director => "役員",
                Post.President => "社長",
                _ => throw new ArgumentOutOfRangeException(nameof(post), "未定義です"),
            };
        }

        /// <summary>
        /// 役職一覧の表示と値ペアのリストを取得します。
        /// </summary>
        /// <returns>役職一覧の表示と値ペアのリスト</returns>
        public static List<(Post value, string disp)> GetAllDispValueList()
        {
            var ret = new List<(Post, string)>();

            var postList = Enum.GetValues(typeof(Post))
            .Cast<Post>()
            .ToList();

            foreach (var p in postList)
            {
                ret.Add((p, p.GetDisplayText()));
            }

            return ret;
        }
    }
}

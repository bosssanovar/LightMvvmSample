using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usecase.Sub
{
    /// <summary>
    /// 組織人員問題の状態列挙子
    /// </summary>
    internal enum Problems
    {
        /// <summary>
        /// 無所属社員あり
        /// </summary>
        UnAssigned,

        /// <summary>
        /// 長不在組織あり
        /// </summary>
        NoBoss,
    }
}

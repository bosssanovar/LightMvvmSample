using Entity.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Service.OrganizationVisitor
{
    /// <summary>
    /// <see cref="OrganizationBase"/>を巡るVisitorインターフェース
    /// </summary>
    internal interface IOrganizationVisitor
    {
        /// <summary>
        /// 処理を行います。
        /// </summary>
        /// <param name="target">ターゲット</param>
        public void Visit(OrganizationBase target);
    }
}

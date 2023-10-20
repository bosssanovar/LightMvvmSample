using Entity.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DomainService
{
    /// <summary>
    /// 組織構成ビルダーのインターフェース
    /// </summary>
    public interface IOrganizationBuilder
    {
        /// <summary>
        /// 構築します。
        /// </summary>
        /// <returns>構築された組織のトップインスタンス</returns>
        public OrganizationBase Build();
    }
}

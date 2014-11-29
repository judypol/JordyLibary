using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JordyLibary.ORM.Mappings
{
    /// <summary>
    /// 汇总类型
    /// </summary>
    public enum AggregationType
    {
        /// <summary>
        /// 平均值
        /// </summary>
        AVG,
        /// <summary>
        /// 最大值
        /// </summary>
        MAX,
        /// <summary>
        /// 最小值
        /// </summary>
        MIN,
        /// <summary>
        /// 记录数
        /// </summary>
        COUNT,
        /// <summary>
        /// 求和
        /// </summary>
        SUM
    }
}

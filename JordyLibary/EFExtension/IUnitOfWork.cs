using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JordyLibary.EFExtension
{
    /// <summary>
    /// 定义一个接口，必须在声明DbContext时继承
    /// </summary>
    interface IUnitOfWork
    {
        int SaveChanges();
    }
}

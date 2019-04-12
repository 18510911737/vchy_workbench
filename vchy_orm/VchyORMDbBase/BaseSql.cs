using System;
using System.Collections.Generic;
using System.Text;
using VchyModel;
using VchyORMCommon.Enum;
using VchyORMSql.Interface;

namespace VchyORMDbBase
{
    public abstract class BaseSql : IDbInsert
    {
        public virtual StringBuilder CreateInsert(BaseEntity model)
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateInsert(List<BaseEntity> model)
        {
            throw new NotImplementedException();
        }
    }
}

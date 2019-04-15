using System;
using System.Collections.Generic;
using System.Text;
using VchyModel;

namespace VchyORMSql.Interface
{
    public interface IDbInsert
    {
        StringBuilder CreateInsert(BaseEntity model);

        StringBuilder CreateInsertAndResultKey(BaseEntity model);

        StringBuilder CreateInsert<T>(List<T> models)
            where T : BaseEntity, new();
    }
}

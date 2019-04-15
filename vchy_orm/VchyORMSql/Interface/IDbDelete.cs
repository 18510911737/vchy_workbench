using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VchyModel;

namespace VchyORMSql.Interface
{
    public interface IDbDelete
    {
        StringBuilder CreateDelete(BaseEntity model);

        StringBuilder CreateDelete<T>(int key)
            where T : BaseEntity;

        StringBuilder CreateDelete<T>(string key)
            where T : BaseEntity;

        StringBuilder CreateDelete<T>(List<T> models)
            where T : BaseEntity, new();

        StringBuilder CreateDelete<T>(Expression<Func<T, bool>> expression)
            where T : BaseEntity, new();
    }
}

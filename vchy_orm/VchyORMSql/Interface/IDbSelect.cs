using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VchyModel;

namespace VchyORMSql.Interface
{
    public interface IDbSelect
    {
        StringBuilder CreateSelect(BaseEntity model);

        StringBuilder CreateSelect<T>(int key)
            where T : BaseEntity;

        StringBuilder CreateSelect<T>(string key)
            where T : BaseEntity;

        StringBuilder CreateSelect<T>()
            where T : BaseEntity;

        StringBuilder CreateSelectFirst<T>(Expression<Func<T, bool>> expression)
            where T : BaseEntity, new();

        StringBuilder CreateSelectList<T>(Expression<Func<T, bool>> expression)
            where T : BaseEntity, new();
    }
}

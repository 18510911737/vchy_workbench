using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VchyModel;

namespace VchyORMSql.Interface
{
    public interface IDbUpdate
    {
        StringBuilder CreateUpdate(BaseEntity model);

        StringBuilder CreateUpdateOnUpdateFields(BaseEntity model, params string[] fields);

        StringBuilder CreateUpdateOnNotUpdateFields(BaseEntity model, params string[] fields);

        StringBuilder CreateUpdate<T>(T model, Expression<Func<T, bool>> expression)
            where T : BaseEntity, new();

        StringBuilder CreateUpdateOnUpdateFields<T>(T model, Expression<Func<T, bool>> expression, params string[] fields)
            where T : BaseEntity, new();

        StringBuilder CreateUpdateOnNotUpdateFields<T>(T model, Expression<Func<T, bool>> expression, params string[] fields)
            where T : BaseEntity, new();

    }
}

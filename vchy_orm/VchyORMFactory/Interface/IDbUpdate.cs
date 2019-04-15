using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VchyModel;

namespace VchyORMFactory.Interface
{
    interface IDbUpdate
    {
        int ExcuteUpdate(BaseEntity model);

        int ExcuteUpdateOnUpdateFields(BaseEntity model, params string[] fields);

        int ExcuteUpdateOnNotUpdateFields(BaseEntity model, params string[] fields);

        int ExcuteUpdate<T>(T model, Expression<Func<T, bool>> expression)
            where T : BaseEntity, new();

        int ExcuteUpdateOnUpdateFields<T>(T model, Expression<Func<T, bool>> expression, params string[] fields)
            where T : BaseEntity, new();

        int ExcuteUpdateOnNotUpdateFields<T>(T model, Expression<Func<T, bool>> expression, params string[] fields)
            where T : BaseEntity, new();
    }
}

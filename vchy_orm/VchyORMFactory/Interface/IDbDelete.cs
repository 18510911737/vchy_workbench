using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VchyModel;

namespace VchyORMFactory.Interface
{
    public interface IDbDelete
    {
        int ExcuteDelete(BaseEntity model);

        int ExcuteDelete(List<BaseEntity> models);

        int ExcuteDelete<T>(int key)
           where T : BaseEntity;

        int ExcuteDelete<T>(string key)
            where T : BaseEntity;

        int ExcuteDelete<T>(List<T> models)
            where T : BaseEntity, new();

        int ExcuteDelete<T>(Expression<Func<T, bool>> expression)
            where T : BaseEntity,new();
    }
}

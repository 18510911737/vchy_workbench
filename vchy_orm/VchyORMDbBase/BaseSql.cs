using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VchyModel;
using VchyORMCommon.Enum;
using VchyORMSql.Interface;

namespace VchyORMDbBase
{
    public abstract class BaseSql : IDbInsert, IDbDelete
    {

        #region IDbInsert

        public virtual StringBuilder CreateInsert(BaseEntity model)
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateInsertAndResultKey(BaseEntity model)
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateInsert<T>(List<T> models) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDbDelete

        public virtual StringBuilder CreateDelete(BaseEntity model)
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateDelete<T>(int key) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateDelete<T>(string key) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateDelete<T>(List<T> models)
              where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateDelete<T>(Expression<Func<T, bool>> expression) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateDelete<T>(T type, Expression<Func<T, bool>> expression) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

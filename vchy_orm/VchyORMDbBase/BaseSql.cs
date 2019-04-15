using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VchyModel;
using VchyORMCommon.Enum;
using VchyORMSql.Interface;

namespace VchyORMDbBase
{
    public abstract class BaseSql : IDbInsert, IDbDelete, IDbUpdate, IDbSelect
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

        public virtual StringBuilder CreateDelete<T>(int key) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateDelete<T>(string key) where T : BaseEntity
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

        #endregion

        #region IDbUpdate

        public virtual StringBuilder CreateUpdate(BaseEntity model)
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateUpdateOnUpdateFields(BaseEntity model, params string[] fields)
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateUpdateOnNotUpdateFields(BaseEntity model, params string[] fields)
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateUpdate<T>(T model, Expression<Func<T, bool>> expression)
            where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateUpdateOnUpdateFields<T>(T model, Expression<Func<T, bool>> expression, params string[] fields)
            where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateUpdateOnNotUpdateFields<T>(T model, Expression<Func<T, bool>> expression, params string[] fields)
            where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDbSelect

        public virtual StringBuilder CreateSelect(BaseEntity model)
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateSelect<T>(int key)
            where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateSelect<T>(string key)
            where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateSelect<T>()
            where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateSelectFirst<T>(Expression<Func<T, bool>> expression)
            where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public virtual StringBuilder CreateSelectList<T>(Expression<Func<T, bool>> expression)
            where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

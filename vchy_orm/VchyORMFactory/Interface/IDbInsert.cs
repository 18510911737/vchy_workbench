using System;
using System.Collections.Generic;
using System.Text;
using VchyModel;
using VchyORMDbBase;
using VchyORMFactory.Factotry;

namespace VchyORMFactory.Interface
{
    public interface IDbInsert
    {
        int ExcuteInsert(BaseEntity model);

        int ExcuteInsert<T>(List<T> models)
            where T : BaseEntity, new();
    }
}

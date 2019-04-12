using System;
using System.Collections.Generic;
using System.Text;
using VchyModel;

namespace VchyORMFactory.Interface
{
    public interface IDbDelete
    {
        int ExcuteDelete(BaseEntity model);

        int ExcuteDelete(List<BaseEntity> models);
    }
}

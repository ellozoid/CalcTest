using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Managers
{
    /// <summary>
    /// Базовый интерфейс хранилища данных
    /// </summary>
    interface IBaseRepository<T>
    {
        object Load(long id);

        void Save(T entity);

        void Update(T entity);

        IEnumerable<T> GetAll();
    }
}

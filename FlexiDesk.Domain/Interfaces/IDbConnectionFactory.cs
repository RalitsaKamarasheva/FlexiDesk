using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiDesk.Domain.Interfaces
{
    public interface IDbConnectionFactory<T>
    {
        public T Get(string destination);
    }
}

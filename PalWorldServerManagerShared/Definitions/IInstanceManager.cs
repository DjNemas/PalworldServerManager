using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalWorldServerManagerShared.Definitions
{
    public interface IInstanceManager
    {
        public ServiceProvider CreateServices();
        public T GetInstance<T>() where T : class;
    }
}

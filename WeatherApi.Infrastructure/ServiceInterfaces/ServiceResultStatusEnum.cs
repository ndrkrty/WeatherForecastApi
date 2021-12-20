using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Infrastructure.ServiceInterfaces
{
    public enum ServiceResultStatusEnum
    {
        Success = 1,
        Error = 2,
        Warning = 3,
        Fail = 4
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.Infrastructure
{
    public static class Singleton<T>
    {
        static T instanse;

        public static T Instance
        {
            get => instanse;
            set
            {
                instanse = value;
            }
        }
    }
}

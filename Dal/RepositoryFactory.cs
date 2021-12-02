using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPK_Delivery_2_Dominik_Hruza_3IP1.Dal
{
    static class RepositoryFactory
    {
        private static readonly Lazy<IRepository> repository = new Lazy<IRepository>(() => new SqlRepository());
        public static IRepository GetRepository() => repository.Value;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Tribes.Database.Context;

namespace Tribes.Database.Dal
{
    public static class DefaultContainer
    {
        static DefaultContainer()
        {
            Container = new UnityContainer().RegisterType<IDbContext, TribesContext>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container { get; set; }
    }
}

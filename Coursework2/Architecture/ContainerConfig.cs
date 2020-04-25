using Autofac;
using Coursework2;
using Coursework2.Architecture.Interfaces;
using Coursework2.Architecture;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DataBase>().As<IDataBase>();
            builder.RegisterType<ReservationSystem>().As<IReservationSystem>();

        
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(Coursework2)))
                .Where(t => t.Namespace.Contains("Architecture"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            return builder.Build();
        }
    }
}

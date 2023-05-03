using Autofac;
using Autofac.Features.ResolveAnything;
using DbWpfApp.Data;
using DbWpfApp.Services;
using DbWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DbWpfApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var builder = new ContainerBuilder();

            //allow the Autofac container resolve unknown types
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            //SQLiteAppItemsService is Singleton
            builder.RegisterType<SQLiteAppItemsService>().As<IAppItemsRepository>().SingleInstance();

            //DataManager is Transient
            builder.RegisterType<DataManager>().InstancePerDependency();
            IContainer container = builder.Build();

            DISource.Resolver = (type) => {
                return container.Resolve(type);
            };
        }
    }
}

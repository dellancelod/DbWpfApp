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
using System.Windows.Input;
using System.Xml.Linq;

namespace DbWpfApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        IContainer Container { get; set; }
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
            Container = builder.Build();

            DISource.Resolver = Resolve;
        }
        object Resolve(Type type, object key, string name)
        {
            if (type == null)
                return null;
            if (key != null)
                return Container.ResolveKeyed(key, type);
            if (name != null)
                return Container.ResolveNamed(name, type);
            return Container.Resolve(type);
        }
    }
}

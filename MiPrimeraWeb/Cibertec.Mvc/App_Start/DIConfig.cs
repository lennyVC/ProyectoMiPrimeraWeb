using Cibertec.Repositories.Dapper.NorthWind;
using Cibertec.UnitOfWork;
using log4net;
using log4net.Core;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Cibertec.Mvc.App_Start
{
    public class DIConfig
    {
        public static void ConfigureInjector()
        {
            var conteiner = new Container();
            conteiner.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            conteiner.Register<IUnitOfWork>(() => new NorthwindUnitOfWork(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString()));
            conteiner.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            /*Inyeccion del log4net*/
            conteiner.RegisterConditional(typeof(ILog), c => typeof(Log4NetAdapter<>).MakeGenericType(c.Consumer.ImplementationType), Lifestyle.Singleton, c => true);
            conteiner.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(conteiner));
        }
    }

    public sealed class Log4NetAdapter<T>:LogImpl
    {
        public Log4NetAdapter():base(LogManager.GetLogger(typeof(T)).Logger)
        { }
    }
}
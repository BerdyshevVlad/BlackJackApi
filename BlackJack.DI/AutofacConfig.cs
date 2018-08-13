using Autofac;
using Autofac.Integration.Mvc;
using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Repositories;
using BlackJack.DAL;
using System.Reflection;
using System.Web.Mvc;

namespace BlackJack.DI
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // регистрируем споставление типов
            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).WithParameter("context", new BlackJackContext());

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}

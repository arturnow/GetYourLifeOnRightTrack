using System.Web.Mvc;
using Common;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using WasterLOB;
using WasterWebAPI.Helpers;

namespace WasterWebAPI
{
    using System.Linq;

    using WasterDAL.Repositories;

    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            var webApiContainer = container.CreateChildContainer();
            RegisterWebApiControllers(webApiContainer);
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new WebApiUnityDependencyResolver(webApiContainer);

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();   

            container.RegisterInstance<IFilterProvider>("FilterProvider", new WasterWebAPI.Filters.FilterProvider(container));

            //RegisterWebApiControllers(container);

            RegisterTypes(container);

            return container;
        }

        private static void RegisterWebApiControllers(IUnityContainer container)
        {

            var assembly = System.Reflection.Assembly.GetAssembly(typeof(Bootstrapper));

            var classes = assembly.GetTypes().Where(t => t.IsClass && t.BaseType == typeof(System.Web.Http.ApiController));

            foreach (var @class in classes)
            {
                container.RegisterType(@class);
            }

            //container.RegisterType<WasterPatternApiController, WasterPatternApiController>());
            //container.RegisterType<TrackRecordApiController, TrackRecordApiController>();
            //container.RegisterType<MessageApiController, MessageApiController>();
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IIdentityService, IdentityService>();
            container.RegisterType<IIdentityRepository, IdentityRepository>();

            container.RegisterType<IGoalService, GoalService>();
            container.RegisterType<IGoalRepository, EFGoalRepository>();

            container.RegisterType<ITaskService, TaskService>();
            container.RegisterType<ITaskRepository, EFTaskRepositry>();

            container.RegisterType<IPatternService, PatternService>();
            container.RegisterType<IPatternRepository, EFPatternRepository>();

            container.RegisterType<ITrackService, TrackService>();
            container.RegisterType<ITrackRecordRepository, EFTrackRecordRepository>();

            container.RegisterType<IMessageService, MessageService>();
            container.RegisterType<IMessageRepository, MessageRepository>();

            container.RegisterType<IPeriodService, PeriodService>();
            container.RegisterType<IPeriodRepository, PeriodRepository>();

            container.RegisterType<IWasteStatisticService, WasteStatisticService>();
            container.RegisterType<IWasteStatisticRepository, WasteStatisticRepository>();

            container.RegisterType<ISocialProfileService, SocialProfileService>();
            container.RegisterType<ISocialProfileRepository, SocialProfileRepository>();

            container.RegisterType<IUserContextProvider, ThreadUserContextProvider>();

            container.RegisterType<IPasswordHashProvider, PasswordHashProvider>();

            //container.RegisterType<WasteContext, WasteContext>();


        }


    }
}
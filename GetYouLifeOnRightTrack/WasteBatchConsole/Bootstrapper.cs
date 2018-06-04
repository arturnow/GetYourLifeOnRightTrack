using Microsoft.Practices.Unity;
using WasteBatchConsole;
using WasterLOB;

namespace WasterWebAPI
{
    using System.Linq;

    using WasterDAL;
    using WasterDAL.Repositories;

    public static class Bootstrapper
    {

        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();   

            //RegisterWebApiControllers(container);

            RegisterTypes(container);

            return container;
        }




        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IIdentityService, IdentityService>();
          //  container.RegisterType<IIdentityRepository, IdentityRepository>();
            container.RegisterType<IIdentityRepository>(new InjectionFactory(
            c =>
            {
                if (BatchProcess._context == null)
                    BatchProcess._context = new WasteContext(); ;
                var imp = new IdentityRepository(BatchProcess._context);
                return imp;
            }));


            container.RegisterType<IGoalService, GoalService>();
            container.RegisterType<IGoalRepository, EFGoalRepository>();

            container.RegisterType<ITaskService, TaskService>();
            container.RegisterType<ITaskRepository, EFTaskRepositry>();

            container.RegisterType<IPatternService, PatternService>();
            container.RegisterType<IPatternRepository, EFPatternRepository>();

            container.RegisterType<ITrackService, TrackService>();
           // container.RegisterType<ITrackRecordRepository, EFTrackRecordRepository>();
            container.RegisterType<ITrackRecordRepository>(new InjectionFactory(
                c =>
                {
                    if (BatchProcess._context == null)
                        BatchProcess._context = new WasteContext(); ;
                    var imp = new EFTrackRecordRepository(BatchProcess._context);
                    return imp;
                }));

            container.RegisterType<IMessageService, MessageService>();
            container.RegisterType<IMessageRepository, MessageRepository>();

            container.RegisterType<IUserContextProvider, ThreadUserContextProvider>();

            container.RegisterType<IPeriodService, PeriodService>();
          //  container.RegisterType<IPeriodRepository, PeriodRepository>();
            container.RegisterType<IPeriodRepository>(new InjectionFactory(
                c =>
                {
                    if (BatchProcess._context == null)
                        BatchProcess._context = new WasteContext(); ;
                    var imp = new PeriodRepository(BatchProcess._context);
                    return imp;
                }));

            container.RegisterType<IWasteStatisticService, WasteStatisticService>();
           // container.RegisterType<IWasteStatisticRepository, WasteStatisticRepository>();

            container.RegisterType<IWasteStatisticRepository>(new InjectionFactory(
         c =>
         {
             if (BatchProcess._context == null)
                 BatchProcess._context = new WasteContext(); ;
             var imp = new WasteStatisticRepository(BatchProcess._context);
             return imp;
         }));

            container.RegisterType<IBatchLogService, BatchLogService>();
        //    container.RegisterType<IBatchLogRepository, BatchLogRepository>();

            container.RegisterType<IBatchLogRepository>(new InjectionFactory(
                c =>
                {
                    if (BatchProcess._context == null)
                        BatchProcess._context = new WasteContext(); ;
                    var imp = new BatchLogRepository(BatchProcess._context);
                    return imp;
                }));
            
            container.RegisterType(typeof (BatchProcess));

            //container.RegisterType<WasteContext, WasteContext>();


        }


    }
}
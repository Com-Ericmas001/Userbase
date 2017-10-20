using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Services;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Com.Ericmas001.Userbase
{
    public static class UserbaseUnityConfig
    {
        public static IUnityContainer Container { get; private set; }

        public static void RegisterTypes(IUnityContainer container = null, string salt = null)
        {
            Container = container ?? new UnityContainer();

            Container.RegisterType<IUserbaseDbContext, UserbaseDbContext>();

            Container.RegisterType<ISendEmailService, ExceptionThrowerEmailService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IValidationService, ValidationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISecurityService, BCryptSecurityService>(new ContainerControlledLifetimeManager(), new InjectionConstructor(salt));

            Container.RegisterType<IManagementService, ManagementService>();
            Container.RegisterType<IUserConnectionService, UserConnectionService>();
            Container.RegisterType<IUserGroupingService, UserGroupingService>();
            Container.RegisterType<IUserInformationService, UserInformationService>();
            Container.RegisterType<IUserManagingService, UserManagingService>();
            Container.RegisterType<IUserObtentionService, UserObtentionService>();
            Container.RegisterType<IUserRecoveryService, UserRecoveryService>();
        }
    }
}

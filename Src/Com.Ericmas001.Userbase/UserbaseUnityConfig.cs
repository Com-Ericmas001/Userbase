using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Com.Ericmas001.Userbase
{
    public static class UserbaseUnityConfig
    {
        public static void RegisterTypes(IUnityContainer container = null, string salt = null)
        {
            container.RegisterType<IUserbaseDbContext, UserbaseDbContext>();

            container.RegisterType<ISendEmailService, ExceptionThrowerEmailService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IValidationService, ValidationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISecurityService, BCryptSecurityService>(new ContainerControlledLifetimeManager(), new InjectionConstructor(salt));

            container.RegisterType<IManagementService, ManagementService>();
            container.RegisterType<IUserConnectionService, UserConnectionService>();
            container.RegisterType<IUserGroupingService, UserGroupingService>();
            container.RegisterType<IUserInformationService, UserInformationService>();
            container.RegisterType<IUserManagingService, UserManagingService>();
            container.RegisterType<IUserObtentionService, UserObtentionService>();
            container.RegisterType<IUserRecoveryService, UserRecoveryService>();

            //this is to be easily overriden by App.Config
            container.LoadConfiguration();
        }
    }
}

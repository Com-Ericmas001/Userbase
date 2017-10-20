using System;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Unity;

namespace Com.Ericmas001.Userbase.Test.Util
{
    class UserbaseSystemUtil
    {
        public UnityContainer Container { get; }
        public IUserbaseDbContext Model { get; }
        public SendRecoveryTokenTest.DummyEmailSender EmailSender { get; }
        public UserbaseSystemUtil(Action<IUserbaseDbContext> initDb)
        {
            Container = new UnityContainer();
            UserbaseUnityConfig.RegisterTypes(Container, Values.Salt);

            Model = new DummyUserbaseDbContext();
            initDb(Model);
            Model.SaveChanges();
            Container.RegisterInstance(Model);

            EmailSender = new SendRecoveryTokenTest.DummyEmailSender();
            Container.RegisterInstance<ISendEmailService>(EmailSender);
        }
    }
}

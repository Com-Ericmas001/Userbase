using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Microsoft.Practices.Unity;

namespace Com.Ericmas001.Userbase.Test.Util
{
    class UserbaseSystemUtil
    {
        public UserbaseSystem System { get; }
        public IUserbaseDbContext Model { get; }
        public SendRecoveryTokenTest.DummyEmailSender EmailSender { get; }
        public UserbaseSystemUtil(Action<IUserbaseDbContext> initDb)
        {
            IUnityContainer container = new UnityContainer();
            System = new UserbaseSystem(container, Values.Salt);

            Model = new DummyUserbaseDbContext();
            initDb(Model);
            Model.SaveChanges();
            container.RegisterInstance(Model);

            EmailSender = new SendRecoveryTokenTest.DummyEmailSender();
            container.RegisterInstance<ISendEmailService>(EmailSender);
        }
    }
}

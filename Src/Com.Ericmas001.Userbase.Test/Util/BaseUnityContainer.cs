﻿using System;
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
        public IUnityContainer Container { get; }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Com.Ericmas001.Userbase.Test.Util
{
    class UserbaseSystemUtil
    {
        public UserbaseSystem System { get; }
        public IUserbaseDbContext Model { get; }
        public UserbaseSystemUtil(Action<IUserbaseDbContext> initDb)
        {
            IUnityContainer container = new UnityContainer();
            System = new UserbaseSystem(container, Values.Salt);

            Model = new DummyUserbaseDbContext();
            initDb(Model);
            container.RegisterInstance<IUserbaseDbContext>(Model);
        }
    }
}

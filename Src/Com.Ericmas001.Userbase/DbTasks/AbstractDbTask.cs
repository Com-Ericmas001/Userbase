namespace Com.Ericmas001.Userbase.DbTasks
{
    public abstract class AbstractDbTask
    {
        protected UserbaseDbContext Context { get; }
        protected AbstractDbTask(UserbaseDbContext context)
        {
            Context = context;
        }
    }
}

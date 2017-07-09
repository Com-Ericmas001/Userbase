using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Userbase.Test.Util
{
    public class DummyDbSet<T> : IDbSet<T> where T : class 
    {
        private readonly List<T> m_List = new List<T>();
        public IEnumerator<T> GetEnumerator()
        {
            return m_List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_List.GetEnumerator();
        }

        public Expression Expression => m_List.AsQueryable().Expression;
        public Type ElementType => m_List.AsQueryable().ElementType;
        public IQueryProvider Provider => m_List.AsQueryable().Provider;
        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Add(T entity)
        {
            m_List.Add(entity);
            return entity;
        }

        public T Remove(T entity)
        {
            m_List.Remove(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Com.Ericmas001.Userbase.Entities;
using Moq;

namespace Com.Ericmas001.Userbase.Test.Util
{
    public sealed class MockDbSet<T> : Mock<DbSet<T>> where T : class, IEntityWithId
    {
        private int nextId = 1;
        private IQueryable<T> List { get; set; }

        public MockDbSet(IQueryable<T> lst, Action<int, T> onAdd = null)
        {
            List = lst;
            RefreshCollection();
            Setup(m => m.RemoveRange(It.IsAny<IEnumerable<T>>())).Returns((IEnumerable<T> tokens) => RemoveRange(tokens));
            Setup(m => m.Add(It.IsAny<T>())).Callback((T elem) => Add(elem, onAdd));
            Setup(m => m.Remove(It.IsAny<T>())).Callback((T elem) => Remove(elem));
        }

        private void Add(T elem, Action<int, T> onAdd)
        {
            List = List.Concat(new[] { elem });
            onAdd?.Invoke(nextId, elem);
            if(elem.Id == 0)
                elem.Id = nextId++;
            RefreshCollection();
        }

        private void Remove(T elem)
        {
            List = List.Except(new[] { elem });
            RefreshCollection();
        }

        private IEnumerable<T> RemoveRange(IEnumerable<T> elems)
        {
            List = List.Except(elems);
            RefreshCollection();
            return List;
        }

        private void RefreshCollection()
        {
            As<IQueryable<T>>().Setup(m => m.Provider).Returns(List.Provider);
            As<IQueryable<T>>().Setup(m => m.Expression).Returns(List.Expression);
            As<IQueryable<T>>().Setup(m => m.ElementType).Returns(List.ElementType);
            As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(List.GetEnumerator());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Pool<T> where T : IPoolable
    {
        private List<T> available = new List<T>();
        private List<T> inUse = new List<T>();
        private Func<T> createFunc; // función que sabe cómo crear un T

        public Pool(int initialSize, Func<T> createFunc)
        {
            this.createFunc = createFunc;
            for (int i = 0; i < initialSize; i++)
            {
                available.Add(createFunc());
            }
        }

        public T Get()
        {
            T obj;
            if (available.Count > 0)
            {
                obj = available[available.Count - 1];
                available.RemoveAt(available.Count - 1);
            }
            else
            {
                obj = createFunc(); // pool dinámico
            }
            obj.Activate();
            inUse.Add(obj);
            return obj;
        }

        public void Return(T obj)
        {
            obj.Deactivate();
            inUse.Remove(obj);
            available.Add(obj);
        }
    }
}

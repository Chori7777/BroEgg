using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSDL2.Engine.Scripts
{
    public class Pool<T> where T : IPoolable //where sirve para especificar que T tengan la interfaz IPoolable
    {
        private List<T> available = new List<T>();
        private List<T> inUse = new List<T>();
        private Func<T> createFunc; // función que sabe cómo crear un T que no sabe lo que es hasta que se crea (o es un bullet o es un enemybullet)

        public Pool(int initialSize, Func<T> createFunc) //constructor del pool, anda a levelcontroller para entender Func<T>
        {
            this.createFunc = createFunc;
            for (int i = 0; i < initialSize; i++)
            {
                available.Add(createFunc());
            }
        }

        public T Get() //Metodo que crea balas si lo necesita, T puede ser una bullet enemy o una bullet
        {
            T obj;
            if (available.Count > 0) //si hay balas disponibles, las saco de available
            {
                obj = available[available.Count - 1]; //asigno la bala a la ultima de la lista
                available.RemoveAt(available.Count - 1); //saco la ultima bala de la lista de available
            }
            else
            {
                obj = createFunc(); // pool dinámico, creo una bala porque en available no habia
            }
            obj.Activate(); //se pone en activo
            inUse.Add(obj); //agrego la bala en la lista inUse que saque de la lista available
            return obj; //devuelve esa bala
        }

        public void Return(T obj) //devuelve la bala en inUse a la lista de disponible
        {
            obj.Deactivate();
            inUse.Remove(obj);
            available.Add(obj);
        }
    }
}

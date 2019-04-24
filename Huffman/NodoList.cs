using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    class NodoList<T> : Collection<Nodo<T>>
    {
        public NodoList() : base() { }

        public NodoList(int initialSize)
        {
            // numero especifico de items
            for (int i = 0; i < initialSize; i++)
                base.Items.Add(default(Nodo<T>));
        }

        public Nodo<T> FindByValue(T value)
        {
            // buscar el valor
            foreach (Nodo<T> Nodo in Items)
                if (Nodo.Value.Equals(value))
                    return Nodo;

            
            return null;
        }
    }
}

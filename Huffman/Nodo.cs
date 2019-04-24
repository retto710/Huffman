using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    class Nodo<T>
    {   // Private member-variables
        private T data;
        private NodoList<T> neighbors = null;

        public Nodo() { }
        public Nodo(T data) : this(data, null) { }
        public Nodo(T data, NodoList<T> neighbors)
        {
            this.data = data;
            this.neighbors = neighbors;
        }

        public T Value
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        protected NodoList<T> Neighbors
        {
            get
            {
                return neighbors;
            }
            set
            {
                neighbors = value;
            }
        }
    }
}

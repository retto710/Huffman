using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    class ArbolBinario<T> : Nodo<T>
    {
        public ArbolBinario() : base() { }
        public ArbolBinario(T data) : base(data, null) { }
        public ArbolBinario(T data, ArbolBinario<T> left, ArbolBinario<T> right)
        {
            base.Value = data;
            NodoList<T> children = new NodoList<T>(2);
            children[0] = left;
            children[1] = right;

            base.Neighbors = children;
        }

        public ArbolBinario<T> Left
        {
            get
            {
                if (base.Neighbors == null)
                    return null;
                else
                    return (ArbolBinario<T>)base.Neighbors[0];
            }
            set
            {
                if (base.Neighbors == null)
                    base.Neighbors = new NodoList<T>(2);

                base.Neighbors[0] = value;
            }
        }

        public ArbolBinario<T> Right
        {
            get
            {
                if (base.Neighbors == null)
                    return null;
                else
                    return (ArbolBinario<T>)base.Neighbors[1];
            }
            set
            {
                if (base.Neighbors == null)
                    base.Neighbors = new NodoList<T>(2);

                base.Neighbors[1] = value;
            }
        }
    }
}


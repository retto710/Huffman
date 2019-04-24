using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Huffman.ColaPrioridades;

namespace Huffman
{
    class ArbolHuffman
    {
        public ArbolBinario<CharFreq> Build(List<CharFreq> charFreq, int n)
        {
            PriorityQueue Q = new PriorityQueue();

            for (int i = 0; i < n; i++)
            {
                ArbolBinario<CharFreq> z = new ArbolBinario<CharFreq>(charFreq[i]);

                Q.insert(z);
            }

            Q.buildHeap();

            for (int i = 0; i < n - 1; i++)
            {
                ArbolBinario<CharFreq> x = Q.extractMin();
                ArbolBinario<CharFreq> y = Q.extractMin();
                CharFreq chFreq = new CharFreq();

                chFreq.ch = (char)((int)x.Value.ch + (int)y.Value.ch);
                chFreq.freq = x.Value.freq + y.Value.freq;

                ArbolBinario<CharFreq> z = new ArbolBinario<CharFreq>(chFreq);

                z.Left = x;
                z.Right = y;
                Q.insert(z);
            }

            return Q.extractMin();
        }

    }
}


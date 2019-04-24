using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    class ColaPrioridades
    {
        public struct CharFreq
        {
            public char ch;
            public int freq;
        }

        public class PriorityQueue
        {
            int heapSize;
            List<ArbolBinario<CharFreq>> nodeList;

            public List<ArbolBinario<CharFreq>> NodeList
            {
                get
                {
                    return nodeList;
                }
            }

            public PriorityQueue()
            {
                nodeList = new List<ArbolBinario<CharFreq>>();
            }

            public PriorityQueue(List<ArbolBinario<CharFreq>> nl)
            {
                heapSize = nl.Count;
                nodeList = new List<ArbolBinario<CharFreq>>();

                for (int i = 0; i < nl.Count; i++)
                    nodeList.Add(nl[i]);
            }

            public void exchange(int i, int j)
            {
                ArbolBinario<CharFreq> temp = nodeList[i];

                nodeList[i] = nodeList[j];
                nodeList[j] = temp;
            }

            public void heapify(int i)
            {
                int l = 2 * i + 1;
                int r = 2 * i + 2;
                int largest = -1;

                if (l < heapSize && nodeList[l].Value.ch > nodeList[i].Value.ch)
                    largest = l;
                else
                    largest = i;
                if (r < heapSize && nodeList[r].Value.ch > nodeList[largest].Value.ch)
                    largest = r;
                if (largest != i)
                {
                    exchange(i, largest);
                    heapify(largest);
                }
            }

            public void buildHeap()
            {
                for (int i = heapSize / 2; i >= 0; i--)
                    heapify(i);
            }

            public int size()
            {
                return heapSize;
            }

            public ArbolBinario<CharFreq> elementAt(int i)
            {
                return nodeList[i];
            }

            public void heapSort()
            {
                int temp = heapSize;

                buildHeap();

                for (int i = heapSize - 1; i >= 1; i--)
                {
                    exchange(0, i);
                    heapSize--;
                    heapify(0);
                }

                heapSize = temp;
            }

            public ArbolBinario<CharFreq> extractMin()
            {
                if (heapSize < 1)
                    return null;

                heapSort();

                exchange(0, heapSize - 1);
                heapSize--;

                ArbolBinario<CharFreq> node = nodeList[heapSize];

                nodeList.RemoveAt(heapSize);
                heapSize = nodeList.Count;
                return node;
            }

            public void insert(ArbolBinario<CharFreq> node)
            {
                nodeList.Add(node);
                heapSize = nodeList.Count;
                buildHeap();
            }
        }
    }
}

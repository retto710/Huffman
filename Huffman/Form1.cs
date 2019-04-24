using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Huffman.ColaPrioridades;

namespace Huffman
{
    public partial class Form1 : Form
    {
        private int leafNodes;
        public Form1()
        {
            InitializeComponent();
        }
        private void InorderTraversal(ArbolBinario<CharFreq> node)
        {
            if (node != null)
            {
                InorderTraversal(node.Left);

                CharFreq cf = node.Value;
                int ord = (int)cf.ch;

                if (node.Left == null && node.Right == null)
                {
                    leafNodes++;
                    textBox2.Text += "'" + new string(cf.ch, 1) + "' ";
                    textBox2.Text += node.Value.freq.ToString() + "\r\n";
                }

                InorderTraversal(node.Right);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            int n = s.Length;
            List<CharFreq> list = new List<CharFreq>();

            textBox2.Text = string.Empty;

            for (int i = 0; i < n; i++)
            {
                bool found = false;
                char c = s[i];
                CharFreq cf = new CharFreq();

                for (int j = 0; !found && j < list.Count; j++)
                {
                    if (c == list[j].ch)
                    {
                        found = true;
                        cf.ch = c;
                        cf.freq = 1 + list[j].freq;
                        list.RemoveAt(j);
                        list.Add(cf);
                    }
                }

                if (!found)
                {
                    cf.ch = c;
                    cf.freq = 1;
                    list.Add(cf);
                }
            }

            ArbolHuffman ht = new ArbolHuffman();
            ArbolBinario<CharFreq> root = ht.Build(list, list.Count);

            InorderTraversal(root);
            textBox2.Text += "\r\n# caracteres = " + n.ToString() + "\r\n";
            textBox2.Text += "# nodos = " + leafNodes.ToString() + "\r\n";
            textBox2.Text += "% comprimido = " +
                (100.0 - 100.0 * ((double)leafNodes) / n).ToString("F2") + "\r\n";
        
    }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            //openFileDialog1.ShowDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Limpiar textbox
                textBox1.Text = "";
                textBox1.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            }
        }
    }
}

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
        private string pesoNodo;
        private string actual;
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
                    textBox2.Text += "'" + cf.codigo + "' ";
                    textBox2.Text += "'" + cf.codigo + "' ";
                    textBox2.Text += node.Value.freq.ToString() + "\r\n";
                    
                }

                InorderTraversal(node.Right);
            }
        }
        void inorderPrintTree(ArbolBinario<CharFreq> node,char letra)
        {
            if (node.Value.ch == letra)
            {
                actual = pesoNodo;
            }
           
            if (node.Left != null)
            {
                pesoNodo += "0";
                inorderPrintTree(node.Left,letra);

            }
           

            if (node.Right != null)
            {
              
                pesoNodo += "1";
  
                inorderPrintTree(node.Right,letra);
            }
            if (pesoNodo.Length >= 1)
            {
                pesoNodo = pesoNodo.Remove(pesoNodo.Length - 1, 1);
            }
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
            //Agregar codigo a Nodos

            InorderTraversal(root);
            for (int i = 0; i < list.Count; i++)
            {
                actual = "";
                pesoNodo = "";
                CharFreq cf = new CharFreq();
                cf.ch = list[0].ch;
                cf.freq = list[0].freq;
                inorderPrintTree(root, list[0].ch);
                cf.codigo = actual;
                list.RemoveAt(0);
                list.Add(cf);
                textBox3.Text += "'" + new string(cf.ch, 1) + "' ";
                textBox3.Text += "'" + cf.codigo + "' ";
                textBox3.Text += cf.freq.ToString() + "\r\n";
            }
           
           
            textBox2.Text += "\r\n# caracteres = " + n.ToString() + "\r\n";
            textBox2.Text += "# nodos = " + leafNodes.ToString() + "\r\n";
            textBox2.Text += "% comprimido = " +
                (100.0 - 100.0 * ((double)leafNodes) / n).ToString("F2") + "\r\n";
            //Texto comprimido
            string texto="" ;
            for (int j = 0; j < s.Length; j++)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (s[j]==list[i].ch)
                    {
                        texto += list[i].codigo;
                    }
                   
                }
            }
      
            textBox4.Text = texto;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saver = new SaveFileDialog();
            DialogResult LocRes = saver.ShowDialog();
            if (LocRes == DialogResult.OK)
            {
                String final = DecToBin(textBox4.Text);
                System.IO.File.WriteAllText(saver.FileName + ".tfo", final);  
             System.IO.File.WriteAllText(saver.FileName+"diccionario" + ".tfo", textBox3.Text); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menu myForm = new Menu();
            this.Hide();
            myForm.ShowDialog();
            this.Close();
        }
        public String DecToBin(String binario)
        {
            String nuevo = "";
            String aux = binario;
            int tamano;
            while (aux.Length > 8)
            {
                nuevo += Convert.ToChar(Convert.ToInt32(aux.Substring(0, 8), 2));
                tamano = aux.Length - 8;          
                aux = aux.Substring(8, tamano);
            }
            nuevo += Convert.ToInt32(aux, 2);
            return nuevo;
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Huffman.ColaPrioridades;

namespace Huffman
{
    public partial class Form2 : Form
    {
        private int leafNodes;
        private string pesoNodo;
        private string actual;
        public Form2()
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
                    textBox2.Text += "(" + new string(cf.ch, 1) + ") ";
                    textBox2.Text += "" + cf.codigo + " ";
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
            textBox3.Text = BinToDec(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "tfo";
            openFileDialog1.Filter = "tfo files (*.tfo)|*.tfo"; ;
            string diccionario;
            string valueDic="";
            //openFileDialog1.ShowDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Limpiar textbox
                textBox1.Text = "";
                textBox1.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
                textBox2.Text = "";
                diccionario = openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.Length-4);
                diccionario = diccionario + "diccionario.tfo";
                textBox2.Text = System.IO.File.ReadAllText(diccionario);
                
                leerDiccionario();
            }
            valueDic = textBox2.Text;

          

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saver = new SaveFileDialog();
            DialogResult LocRes = saver.ShowDialog();
            if (LocRes == DialogResult.OK)
                System.IO.File.WriteAllText(saver.FileName + ".txt", textBox4.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menu myForm = new Menu();
            this.Hide();
            myForm.ShowDialog();
            this.Close();
        }
        public String BinToDec(String text)
        {
            String binario = "";
            int numItems = text.Length;
            int cont = 0;
            System.Console.WriteLine(numItems);
            //Pasar a binario de nuevo
            foreach (char word in text)
            {
                cont++;
                int sword = (int)word;
                if (cont != numItems)
                {
                    String bin = Convert.ToString(sword, 2);
                    int faltaceros = 8 - bin.Length;
                    for (int i = 0; i < faltaceros; i++)
                    {
                        bin = "0" + bin;
                    }
                    binario += bin;
                }
                else
                {
                    binario += Convert.ToString(sword, 2);
                }
            }
            return binario;
        }

        public void leerDiccionario()
        {
            int nLineas = textBox2.Lines.Length-1;
            string result = "";
            for (int i = 0; i < nLineas; i++)
            {
                String s = textBox2.Lines[i].ToString();
                String letra;
                String cantidad;
                int numeroveces;
               
                int start = s.IndexOf("(") + 1;
                int end = s.IndexOf(")", start);
                letra = s.Substring(start, end - start);
                int start2 = s.IndexOf("'") + 1;
                int end2 = s.IndexOf("'", start2);
                cantidad = s.Substring(start2, end2 - start2);
                numeroveces= Int32.Parse(cantidad);
                for (int j = 0; j < numeroveces; j++)
                {
                    result += letra;
                }
            }

            //textBox4.Text = result;
            int n = result.Length;
            List<CharFreq> list = new List<CharFreq>();

            //textBox2.Text = string.Empty;

            for (int i = 0; i < n; i++)
            {
                bool found = false;
                char c = result[i];
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
                textBox4.Text += "(" + new string(cf.ch, 1) + ") ";
                textBox4.Text += "" + cf.codigo + " ";
                textBox4.Text += "'" + cf.freq.ToString() + "' " + "\r\n";
            }
        }
    }
}

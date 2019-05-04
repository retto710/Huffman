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
            textBox3.Text = BinToDec(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "tfo";
            openFileDialog1.Filter = "tfo files (*.tfo)|*.tfo";
            //openFileDialog1.ShowDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Limpiar textbox
                textBox1.Text = "";
                textBox1.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
                textBox2.Text = "";
                string diccionario = openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.Length-4);
                diccionario = diccionario + "diccionario.tfo";
                textBox2.Text = System.IO.File.ReadAllText(diccionario);
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public struct Secuence1
        {
           public int num_of_elements;
           public int[] elements;
        }

        public struct FileHeader
        {
            //public char Identify_of_Data[3];  <-- Hard Way to do -- Marshalling and fixed arrays in struct
            public int Num_of_elements;
            public int Elements;
        }
     
        public Form1()
        {
            InitializeComponent();
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                label2.Text = fileName;
                FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file);
                label2.Text = reader.ReadToEnd();
                reader.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName, buff;
            Secuence1 sec1_to_file;
            sec1_to_file.num_of_elements = 0;
            sec1_to_file.elements =  new int[100];
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                label2.Text = fileName;
                GenerateSecuence(sec1_to_file);
                buff = Convert.ToString(sec1_to_file);
                FileStream savefile = new FileStream(fileName, FileMode.Create); //создаем файловый поток
                StreamWriter writer = new StreamWriter(savefile); //создаем «потоковый писатель» и связываем его с файловым потоком
                writer.Write(buff); //записываем в файл
                writer.Close(); //закрываем поток. Не закрыв поток, в файл ничего не запишется 
            }      
        }

        private void GenerateSecuence(Secuence1 secuence1)
        {
            for (int i = 0; i < 100; i++)
            {
                secuence1.elements[i] = i * 3 + i; 
            }
            secuence1.num_of_elements = 100;
        }
    }
}

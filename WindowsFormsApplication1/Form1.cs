// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com


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
           public int[] elements;
        }

        public struct FileHeader
        {
            public string Magic;
            public int Num_of_elements;
        }

        public const int Total_numbers_of_elements = 1000;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName;
            Secuence1 sec1_from_file;
            FileHeader fh;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                label2.Text = fileName;
                
                //создаем бинарный «потоковый читатель» и связываем его с файловым потоком
                BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)); 

                fh.Magic = reader.ReadString();
                fh.Num_of_elements = reader.ReadInt32();
                sec1_from_file.elements = new int[fh.Num_of_elements];
                for (int i = 0; i < fh.Num_of_elements; i++)
                    sec1_from_file.elements[i] =  reader.ReadInt32();
                reader.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName;
            Secuence1 sec1_to_file;
            FileHeader fh;
            fh.Magic = "Sec";
           /*
            write to file
             string
             int
             int[]
             */
            sec1_to_file.elements =  new int[100];
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                label2.Text = fileName;
                GenerateSecuence(sec1_to_file);
                fh.Num_of_elements = sec1_to_file.elements.Length;
                BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)); //создаем бинарный «потоковый писатель» и связываем его с файловым потоком
                writer.Write(fh.Magic); //записываем в файл
                writer.Write(fh.Num_of_elements);
                foreach (int el in sec1_to_file.elements)
                    writer.Write(el);
                writer.Close(); //закрываем поток. Не закрыв поток, в файл ничего не запишется 
            }      
        }

        private void GenerateSecuence(Secuence1 secuence1)
        {
            for (int i = 0; i < Total_numbers_of_elements; i++)
            {
                secuence1.elements[i] = i * 3 + i; 
            }
        }
    }
}

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
        public Form1()
        {
            InitializeComponent();

            //Custom variable initialize

            secuence_of_data.elements = new List<uint>();
            data_from_file.elements = new List<uint>();
            isDataGenerated = false;
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Strongly LOCAL variable
            string fileName;
            FileHeader fh;

            fh.Magic = "";
            fh.Number_of_elements = 0;

            //OpenFileDialog filter initialize
            openFileDialog1.InitialDirectory = "d:\\";
            openFileDialog1.FileName = "*.sec";
            openFileDialog1.Filter = "Sec files (*.sec)|*.sec|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            
            // read from file: string - uint - List<uint>
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                try
                {
                    //создаем бинарный «потоковый читатель» и связываем его с файловым потоком
                    BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open));

                    //Read FileHeader data
                    fh.Magic = reader.ReadString();
                    //Magic must be == "Sec"
                    if(fh.Magic == "Sec")
                    {
                        fh.Number_of_elements = reader.ReadUInt32();
                        if (fh.Number_of_elements > 0)
                        {
                            for (int i = 0; i < fh.Number_of_elements; i++)
                                data_from_file.elements.Add(reader.ReadUInt32());

                            //Only if reading data from file was succesfull
                            label1.Text = "Data was readed from file " + fileName;
                            label2.Text = "Number of elements: " + data_from_file.elements.Count();
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Strongly LOCAL variable
            string fileName;
            FileHeader fh;

            //SaveFileDialog filter initialize
            saveFileDialog1.InitialDirectory = "d:\\";
            saveFileDialog1.Filter = "Sec files (*.sec)|*.sec|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

           //write to file: string - uint - List<uint>
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                try
                {
                    //if data was generated -> can save data to file
                      if (isDataGenerated)
                         {
                            //создаем бинарный «потоковый писатель» и связываем его с файловым потоком
                            BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create));
                            fh.Magic = "Sec";
                            fh.Number_of_elements = (uint)secuence_of_data.elements.Count();
                            if (fh.Number_of_elements > 0)
                            {
                                //записываем в файл
                                writer.Write(fh.Magic);
                                writer.Write(fh.Number_of_elements);
                                foreach (int el in secuence_of_data.elements)
                                         writer.Write(el);
                            }
                            //закрываем поток. Не закрыв поток, в файл ничего не запишется 
                            writer.Close();
                            label1.Text = "Data was saved to file " + fileName;
                            label2.Text = "Number of elements: " + fh.Number_of_elements;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not save file to disk. Original error: " + ex.Message);
                }
            }      
        }

        private void GenerateSecuence(Secuence_of_data secuence_1, uint number_of_elements)
        {
            if (number_of_elements > 0)
            {
                for (uint i = 0; i < number_of_elements; i++)
                {
                    secuence_1.elements.Add(i * 3 + i); 
                }
                saveToolStripMenuItem.Enabled = true;
            }            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test Application.\nTest C#, WinForm's and GitHub.\nПора заняться делом.","About application.");
        }

        private void generateDataSecuenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isDataGenerated == false)
            {
                 GenerateSecuence(secuence_of_data, Total_numbers_of_elements);
                 if (secuence_of_data.elements.Count() > 0)
                {
                    //If we have data secuence - we not needed active item - "Generate" in menu
                    generateToolStripMenuItem.Enabled = false;
                    isDataGenerated = true;
                }
            }
        }
    }
}

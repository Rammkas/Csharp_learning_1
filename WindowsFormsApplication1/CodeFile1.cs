using System.Collections.Generic;

namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// This struct discribe simple data format
        /// <para>
        /// data consist from array of integer values
        /// </para>
        /// </summary>
        public struct Secuence_of_data
        {
            public List<uint> elements;
        }

        /// <summary>
        /// This struct discribe file header format where stored data
        /// <para>
        /// 1st position in file - "Magic" value 
        /// </para>
        /// 2nd position in file - number of elements
        /// <para>
        /// Data placed in file exactly after header
        /// </para>
        /// </summary>
        public struct FileHeader
        {
            /// <summary>
            /// "Magic" value must be == "Sec"
            /// </summary>
            public string Magic;
            /// <summary>
            /// number of elements of stored data in file
            /// </summary>
           public uint Number_of_elements;
        }
        
        // Global variable
        
        /// <summary>
        /// Constat number of data elements
        /// </summary>
        public const uint Total_numbers_of_elements = 1000000;

        /// <summary>
        /// if == true -> data secuence ready to save to file
        /// </summary>
        public bool isDataGenerated;
        /// <summary>
        /// Global variable for this application that consist data secuence
        /// </summary>
        public Secuence_of_data secuence_of_data;
        public Secuence_of_data data_from_file;

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.IO;


namespace Half_RLE
{
    public class HalfRLE : IAbout, IStoreMethod
    {
        public string Description { get; }

        public string Name { get; }

        public string Author { get; }

        public string Date { get; }

        public string Link { get; }

        public bool IsDefault { get; }

        public string Parameters { get; }

        public void About()
        {
            throw new NotImplementedException();
        }

        private int AmountLines { get; set; }

        public HalfRLE()
        {
            Parameters = "Lines - the amount of lines";
            Name = "Half-RLE Store Method";
            Description = "Store method like RLE but without compressing. You have byte of amount bytes and these bytes follow it.";
            Author = "Sergey Koryshev";
            Date = "May, 2018";
            Link = string.Empty;
            IsDefault = true;

        }

        public HalfRLE(string _parameters)
        {
            AmountLines = int.Parse(_parameters);
        }

        public List<byte> GetBytes(int _offset, string _pathToROM)
        {
            List<byte> result = new List<byte>();

            BinaryReader file = new BinaryReader(File.Open(_pathToROM, FileMode.Open));

            file.BaseStream.Seek(_offset, SeekOrigin.Begin);

            int i = 0;
            byte countBytes = 0;
            while(i < AmountLines)
            {
                countBytes = file.ReadByte();
                result.Add(countBytes);
                for (int j = 0; j < countBytes; j++)
                {
                    result.Add(file.ReadByte());
                }
                i++;
            }

            file.Close();

            return result;
        }

        public void InsertBytes(int _offset, string _pathToROM, List<byte> _sequence)
        {
            BinaryWriter file = new BinaryWriter(File.Open(_pathToROM, FileMode.Open));

            file.BaseStream.Seek(_offset, SeekOrigin.Begin);

            foreach (byte currentByte in _sequence)
            {
                file.Write(currentByte);
            }

            file.Close();
        }
    }
}

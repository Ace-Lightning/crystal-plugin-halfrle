using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Half_RLE;

namespace HalfRLE_tests
{
    [TestClass]
    public class HalfRLE_tests
    {
        [TestMethod]
        public void GetBytes_0x32EC1andGetBytes_rom_GetBytes_result()
        {
            int offsetText = 0x32EC1;
            int offsetCountLines = 0x30946;
            string pathToROM = @"D:\Repositories\NewCrystal\crystal-plugin-halfrle\HalfRLE_tests\tests\GetBytes_rom";
            string pathToResult = @"D:\Repositories\NewCrystal\crystal-plugin-halfrle\HalfRLE_tests\tests\GetBytes_result";
            List<byte> expected = new List<byte>();
            using (BinaryReader file = new BinaryReader(File.Open(pathToResult, FileMode.Open)))
            {
                while(file.BaseStream.Position < file.BaseStream.Length)
                {
                    expected.Add(file.ReadByte());
                }
            }

            HalfRLE c = new HalfRLE(offsetCountLines.ToString());
            List<byte> actual = c.GetBytes(offsetText, pathToROM);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}

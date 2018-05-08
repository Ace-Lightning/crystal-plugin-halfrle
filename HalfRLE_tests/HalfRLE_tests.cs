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
        public void GetBytes()
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

            CollectionAssert.AreEqual(expected, actual, "Expected sequence of the bytes is not equal to actual sequence");
        }

        [TestMethod] 
        public void InsertBytes()
        {
            int offsetText = 0x32EC1;
            int offsetCountLines = 0x30946;
            string pathToROM = @"D:\Repositories\NewCrystal\crystal-plugin-halfrle\HalfRLE_tests\tests\InsertBytes_rom";
            string pathToSequence = @"D:\Repositories\NewCrystal\crystal-plugin-halfrle\HalfRLE_tests\tests\InsertBytes_sequence";
            List<byte> expectedSequence = new List<byte>();
            byte expectedCountLines = 0;
            byte countBytes = 0;
            using (BinaryReader file = new BinaryReader(File.Open(pathToSequence, FileMode.Open)))
            {
                while (file.BaseStream.Position < file.BaseStream.Length)
                {
                    countBytes = file.ReadByte();
                    expectedCountLines++;
                    expectedSequence.Add(countBytes);
                    for (int i = 0; i < countBytes; i++)
                    {
                        expectedSequence.Add(file.ReadByte());
                    }
                }
            }

            HalfRLE c = new HalfRLE(offsetCountLines.ToString());
            c.InsertBytes(offsetText, pathToROM, expectedSequence);

            byte actualCounLines = 0;

            using (BinaryReader file = new BinaryReader(File.Open(pathToROM, FileMode.Open)))
            {
                file.BaseStream.Seek(offsetCountLines, SeekOrigin.Begin);
                actualCounLines = file.ReadByte();
            }

            List<byte> actualSequence = c.GetBytes(offsetText, pathToROM);

            Assert.AreEqual(expectedCountLines, actualCounLines);
            CollectionAssert.AreEqual(expectedSequence, actualSequence);
        }
    }
}

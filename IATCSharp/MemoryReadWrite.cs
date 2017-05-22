using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace WpfIATCSharp
{
    class MemoryReadWrite
    {
        public static void CreateOrOpen(string data)
        {
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateOrOpen("lipan", 1024000, MemoryMappedFileAccess.ReadWrite))
            {
                using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                {
                    var writer = new BinaryWriter(stream);
                    for (int i = 0; i < 500; i++)
                    {
                        writer.Write(i);
                        Debug.WriteLine("{0}位置写入流:{0}", i);
                        //Thread.Sleep(500);  
                    }
                }
            }
        }

        public static void Read()
        {
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateOrOpen("lipan", 1024000, MemoryMappedFileAccess.ReadWrite))
            {
                using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                {
                    var reader = new BinaryReader(stream);
                    for (int i = 0; i < 500; i++)
                    {
                        Debug.WriteLine("{1}位置:{0}", reader.ReadInt32(), i);
                        //Thread.Sleep(1000);  
                    }
                }
            }
        }
    }
}

using System.IO;
using System.IO.Compression;

namespace Zip
{
    class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                throw new ArgumentException("No arguments provided.");
            }

            switch(args[0])
            {
                case "-D": 
                    ZipFile.ExtractToDirectory(args[1], args[2], true);
                    Console.WriteLine("Package succesfully extracted.");
                    break;

                case "-C":
                    ZipFile.CreateFromDirectory(args[1], args[2], CompressionLevel.Fastest, bool.Parse(args[3]));
                    Console.WriteLine("Package succesfully created.");
                    break;

                case "-CE":
                    using (FileStream ZipToOpen = new FileStream(args[1], FileMode.Open))
                    {
                        using (ZipArchive Archive = new ZipArchive(ZipToOpen, ZipArchiveMode.Update))
                        {
                            ZipArchiveEntry ReadMeEntry = Archive.CreateEntry(args[2]);
                            using (StreamWriter Writer = new StreamWriter(ReadMeEntry.Open()))
                            {
                                Writer.WriteLine("Information about this package.");
                            }
                        }
                    }
                    break;

                case "-R":
                    string ZipPath = args[1];
                    using (ZipArchive Archive = ZipFile.OpenRead(ZipPath))
                    {
                        foreach (ZipArchiveEntry Entry in Archive.Entries)
                        {
                            Console.WriteLine(Entry.FullName);
                        }
                    }
                    break;


            }

        }
    }
}

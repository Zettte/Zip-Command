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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No arguments provided, please read the documentation for more information.");
                Console.ResetColor();
                Environment.Exit(1);
            }

            switch(args[0])
            {
                case "-D": 
                    ZipFile.ExtractToDirectory(args[1], args[2], true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Package succesfully extracted at {args[2]}");
                    Console.ResetColor();
                    break;

                case "-C":
                    ZipFile.CreateFromDirectory(args[1], args[2], CompressionLevel.Fastest, bool.Parse(args[3]));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Package succesfully created at {args[2]}.");
                    Console.ResetColor();
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
                    Console.WriteLine("List of the files and/or folders inside this zip file: ");
                    using (ZipArchive Archive = ZipFile.OpenRead(ZipPath))
                    {
                        foreach (ZipArchiveEntry Entry in Archive.Entries)
                        {
                            Console.WriteLine($"\t\t{Entry.FullName}");
                        }
                    }
                    break;


            }

        }
    }
}

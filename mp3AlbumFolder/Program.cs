using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp3AlbumFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No path!");
                Environment.Exit(0);
            }

            string mp3Folder = args[0];

            string[] files = Directory.GetFiles(mp3Folder, "*.mp3");
            List<string> createdAlbumFolders = new List<string>();
            FileInfo fi = null;
            string album = "";

            foreach (var file in files)
            {
                //Create filetag instance with mp3 file
                using (TagLib.File tagf = TagLib.File.Create(file))
                {
                    //Replace unsupported characters
                    album = tagf.Tag.Album.Replace(':', ' ');

                    //Create folder for the first time
                    if (!createdAlbumFolders.Contains(album))
                    {
                        createdAlbumFolders.Add(album);

                        if(!Directory.Exists(Path.Combine(mp3Folder, album)))
                        {
                            Directory.CreateDirectory(Path.Combine(mp3Folder, album));
                            Console.WriteLine(album + " folder created");
                        }
                        else
                            Console.WriteLine(album + " folder exists");
                    }

                    fi = new FileInfo(file);
                    if(!File.Exists(Path.Combine(mp3Folder, album, fi.Name)))
                    {
                        File.Move(file, Path.Combine(mp3Folder, album, fi.Name));
                        Console.WriteLine(file + " moved");
                    }
                    else
                        Console.WriteLine(file + " exists");

                }
            }

            Console.ReadLine();
        }
    }
}

using System.Collections.Generic;
using System.IO;

namespace IO
{
    public static class FileValidator
    {
        public static HashSet<string> PNG_EXTENSION = new HashSet<string> { "png" };

        public static void CheckParentDirectory(string filename)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
            {
                throw new DirectoryNotFoundException($"parent directory of file {filename} does not exist");
            }
        }

        public static void CheckExtension(string filename, HashSet<string> possibleExtensions)
        {
            string extension = Path.GetExtension(filename).Replace(".", "");

            if (!possibleExtensions.Contains(extension))
            {
                throw new BadFileExtensionError($"found {extension} extension, but expected one of the {string.Join(", ", possibleExtensions)}");
            }
        }

        public static void CheckFileExists(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"file {filename} does not exist");
            }
        }
    }
}

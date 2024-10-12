using System;
using System.Collections.Generic;
using System.Linq;

namespace Jenny.Plugins
{
    public class MergeFilesPostProcessor : IPostProcessor
    {
        public string Name => "Merge files";
        public int Order => 90;
        public bool RunInDryMode => true;

        public CodeGenFile[] PostProcess(CodeGenFile[] files)
        {
            var pathToFile = new Dictionary<string, CodeGenFile>();
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                if (!pathToFile.ContainsKey(file.FileName))
                {
                    pathToFile.Add(file.FileName, file);
                }
                else
                {
                    pathToFile[file.FileName].FileContent += $"\n{file.FileContent}";
                    pathToFile[file.FileName].GeneratorName += $", {file.GeneratorName}";
                    files[i] = null;
                }
            }

            var resultFiles = files
                .Where(file => file != null)
                .ToArray();

            Console.WriteLine($"\nMergeFilesPostProcessor:");
            Console.WriteLine(
                $"Source files: {files.Length}. Result files: {resultFiles.Length}. Merged files: {files.Length - resultFiles.Length}.\n");
            return resultFiles;
        }
    }
}

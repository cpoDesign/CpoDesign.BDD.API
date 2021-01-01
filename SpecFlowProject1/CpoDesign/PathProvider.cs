using System;
using System.IO;
using System.Reflection;

namespace SpecFlowProject1.CpoDesign
{
    public class PathProvider
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return GetProjectFolder(Path.GetDirectoryName(path));
            }
        }

        public static string GetPathForSpecificObjectType(Type objectType)
        {
            //get the full location of the assembly with DaoTests in it
            string fullPath = System.Reflection.Assembly.GetAssembly(objectType).Location;

            //get the folder that's in
            return GetProjectFolder(Path.GetDirectoryName(fullPath));
        }

        public static string GetProjectFolder(string path)
        {
            var dir = Directory.GetParent(path);
            return dir.Parent.Parent.FullName;
        }
    }

    public class TemplateTester
    {
        internal static string LoadTemplateData(string templateFile)
        {
            var text = File.ReadAllText(templateFile);

            return text;
        }

        internal static bool CheckTemplateFolderExists()
        {
            string templateDirectory = GetTemplateDirectoryPath();
            return Directory.Exists(templateDirectory);
        }


        internal static string GetTemplatePathIfExists(string templateFileName)
        {
            string templateDirectory = GetTemplateDirectoryPath();
            var filePath = Path.Combine(templateDirectory, templateFileName);

            if (File.Exists(filePath))
            {
                return filePath;
            }

            return string.Empty;
        }

        private static string GetTemplateDirectoryPath()
        {
            var directory = PathProvider.AssemblyDirectory;
            var templateDirectory = Path.Combine(directory, "Templates");
            return templateDirectory;
        }
    }
}

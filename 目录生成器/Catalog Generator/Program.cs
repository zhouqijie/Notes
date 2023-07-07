using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


namespace Catalog_Generator
{
    class Program
    {
        public static DirectoryInfo root = null;

        static void Main(string[] args)
        {
            Console.WriteLine("请输入笔记根目录：");

            string path = Console.ReadLine();
            if(!Directory.Exists(path))
            {
                Console.WriteLine("Path Not Exist");
                return;
            }

            root = new DirectoryInfo(path);
            GenCatalogHere(root);
        }

        static void GenCatalogHere(DirectoryInfo dir)
        {
            //清楚旧的目录  
            foreach(var f in dir.GetFiles())
            {
                if(f.Name.Contains("--目录--"))
                {
                    File.Delete(f.FullName);
                    Console.WriteLine("删除旧目录:" + f.Name);
                }
            }


            //文件名  
            string catelogFullName;
            bool isRoot = (dir == root);
            if (isRoot)
                catelogFullName = root.FullName + "\\index.md";
            else
                catelogFullName = dir.FullName + "\\--目录--" + dir.Name + ".md";

            //开始写入  
            StringWriter writer = new StringWriter();

            //TITLE  
            if(isRoot)
            {
                writer.WriteLine("# 目录  \n\n");
            }
            else
            {
                writer.WriteLine("# 目录  \n\n");
            }
            

            // LINK: RETURN  
            if(isRoot)
            {
                writer.WriteLine("> --zqj  \n\n");
            }
            else
            {
                if(dir.Parent.FullName == root.FullName)
                {
                    writer.WriteLine("[👈【返回】](" + "..\\index" + ")  \n\n");
                }
                else
                {
                    writer.WriteLine("[👈【返回】](" + "..\\--目录--" + dir.Parent.Name + ")  \n\n");
                }
            }



            // ITEM:  CHILD DIRS  
            foreach (var childDir in dir.GetDirectories())
            {
                if ((childDir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

                string name = childDir.Name; name = name.Replace(" ", " ");
                string relaPath = ".\\" + name + "\\--目录--" + name;

                writer.WriteLine("[📁" + name + "](" + relaPath + ")  \n");

                GenCatalogHere(childDir);
            }


            // ITEM:  FILES  
            foreach (var f in dir.GetFiles())
            {
                if (f.Name.Contains("--目录--")) continue;
                if (f.Name.Contains("index.md")) continue;
                if ((f.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

                string fNameNoExtension = f.Name.Substring(0, (f.Name.Length - f.Extension.Length));

                switch (f.Extension)
                {
                    case ".md":
                        {
                            string name = fNameNoExtension; name = name.Replace(" ", " ");
                            string relaPath = ".\\" + fNameNoExtension;
                            writer.WriteLine("[📜" + name + "](" + relaPath + ")  \n");
                        }
                        break;
                    default:
                        break;
                }
            }


            writer.WriteLine("\n\n\n\n\n\n> " + System.DateTime.Now.ToString());
            writer.Flush();

            File.WriteAllText(catelogFullName, writer.ToString());
            writer.Dispose();

            Console.WriteLine("建立目录:" + catelogFullName);
        }
    }
}

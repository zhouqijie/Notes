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
            Console.WriteLine("输入操作：(d)删除所有目录/(g)生成目录");

            string op = Console.ReadLine();

            Console.WriteLine("请输入笔记根目录：");
            string path = Console.ReadLine();
            if (path == "")
            {
                path = @"F:\Legacy\------笔记本-----";
            }

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Path Not Exist");
                return;
            }
            root = new DirectoryInfo(path);


            if (op == "d")
            {
                DeleteAllCatalog(root);
            }
            else if(op == "g")
            {
                GenCatalogAtDir(root, true);
            }
        }

        static void DeleteAllCatalog(DirectoryInfo dir)
        {
            foreach(var file in dir.GetFiles())
            {
                if(file.Name.Contains("--目录--"))
                {
                    File.Delete(file.FullName);
                    Console.WriteLine("已删除目录：" + file.FullName);
                }
            }

            foreach(var child in dir.GetDirectories())
            {
                DeleteAllCatalog(child);
            }
        }

        static void GenCatalogAtDir(DirectoryInfo dir, bool isRoot = false)
        {
            //目录  
            string mapDir = MapToCatalogPath(dir); 
            if(!Directory.Exists(mapDir))
            {
                Directory.CreateDirectory(mapDir);
            }

            //文件名  
            string catalogName;
            if (isRoot == false)
                catalogName = "--目录--" + dir.Name + ".md";
            else
                catalogName = "--目录--root.md";
            
            string catalogFullName = mapDir + "\\" + catalogName;


            //开始写入  
            StringWriter writer = new StringWriter();

            //TITLE  
            writer.WriteLine("# 目录  \n\n");


            // LINK: RETURN  
            if( isRoot == false)
            {
                if(dir.Parent.FullName == root.FullName)
                {
                    writer.WriteLine(ReplaceSeperator("[👈【返回】](/--目录--/--目录--root.md)  \n\n"));
                }
                else
                {
                    writer.WriteLine(ReplaceSeperator("[👈【返回】](" + "/--目录--" + ReplaceSeperator(GetRelativePath(dir.Parent.FullName)) + ")  \n\n"));
                }
            }



            // ITEM:  CHILD DIRS  
            foreach (var childDir in dir.GetDirectories())
            {
                if (childDir.Name.Contains("目录")) continue;
                if (childDir.Name.Contains("Images")) continue;
                if ((childDir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

                string dirName = childDir.Name; dirName = dirName.Replace(" ", " ");
                string catalogDir = MapToCatalogPath(childDir); Console.WriteLine("\ncatalogdir:" + catalogDir);
                string relativeCatalogDir = GetRelativePath(catalogDir); Console.WriteLine("\nrelativecatalogdir:" + relativeCatalogDir);
                string relaPath = ReplaceSeperator(relativeCatalogDir + "\\--目录--" + dirName + "");//no ".md" extension in jekyll  

                writer.WriteLine("[📁" + dirName + "](" + relaPath + ")  \n");

                //递归遍历子目录
                GenCatalogAtDir(childDir);
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
                            string fullNameNoExtension = f.FullName.Substring(0, (f.FullName.Length - f.Extension.Length));//no ".md" extension in jekyll  
                            string relaPath = GetRelativePath(fullNameNoExtension);  
                            writer.WriteLine("[📜" + name + "](" + ReplaceSeperator(relaPath) + ")  \n");
                        }
                        break;
                    default:
                        break;
                }
            }


            writer.WriteLine("\n\n\n\n\n\n> " + System.DateTime.Now.ToString());
            writer.Flush();

            File.WriteAllText(catalogFullName, writer.ToString());
            writer.Dispose();

            Console.WriteLine("建立目录:" + catalogFullName);
        }


        static string MapToCatalogPath(DirectoryInfo dir)
        {
            string path = dir.FullName;
            string relaPath = GetRelativePath(path);

            if (dir.FullName != root.FullName)
            {
                return root.FullName + "\\--目录--" + relaPath; ;
            }
            else
            {
                return root.FullName + "\\--目录--";
            }
        }

        static string GetRelativePath(string path)
        {
            string rootPath = root.FullName;

            if (path == rootPath)
                return "\\";
            else
                return "\\" + path.Substring(rootPath.Length + 1);
        }

        static string ReplaceSeperator(string path)
        {
            return path.Replace('\\', '/');
        }
    }
}

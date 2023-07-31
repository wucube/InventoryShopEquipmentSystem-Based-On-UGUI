using UnityEngine;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// 文件读写辅助类
/// </summary>
public class Files
{
    public static FileInfo[] LoadFiles(string path)
    {
        path = string.Format("{0}/{1}", Application.dataPath, path);

        if (Directory.Exists(path))
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            List<FileInfo> files = new List<FileInfo>();
            foreach (var file in directory.GetFiles("*"))
            {
                if (file.Name.EndsWith(".meta"))
                    continue;
                if (file.Name.StartsWith("~")) continue;

                files.Add(file);
            }
            return files.ToArray();
        }
        else
        {
            throw new System.Exception("路径不存在");
        }
    }

    public static void SaveFile(string path, string fileName, string fileContent)
    {
        path = string.Format("{0}/{1}", Application.dataPath, path);

        if (Directory.Exists(path))
        {
            path = string.Format("{0}/{1}", path, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllText(path, fileContent);
        }
        else
        {
            throw new System.Exception("路径不存在");
        }
    }
}



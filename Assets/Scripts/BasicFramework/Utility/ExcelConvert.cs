using UnityEngine;
using UnityEditor;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using System;
using System.Text;
/// <summary>
/// 使用EPPlus获取表格数据，同时导出对应的Json以及Class.
/// </summary>

/*
public class ExcelConvert
{
    /// <summary>
    /// Excel表格路径
    /// </summary>
    private const string excelPath = "../Assets/Excels";
    /// <summary>
    /// 导出的Json路径
    /// </summary>
    private const string configPath = "../Assets/Resources/Json";
    /// <summary>
    /// 导出的类路径
    /// </summary>
    private const string classPath = "../Assets/Scripts/Configs";

    /// <summary>
    /// 属性行
    /// </summary>
    private const int propertyIndex = 2;
    /// <summary>
    /// 类型行
    /// </summary>
    private const int typeIndex = 3;
    /// <summary>
    /// 值行
    /// </summary>
    private const int valueIndex = 4;



    [MenuItem("Tools/ExportExcel")]
    private static void ExportConfigs()
    {
        try
        {
            FileInfo[] files = Files.LoadFiles(excelPath);

            foreach (var file in files)
            {
                //过滤文件
                if (file.Extension != ".xlsx") continue;
                ExcelPackage excelPackage = new ExcelPackage(file);
                ExcelWorksheets worksheets = excelPackage.Workbook.Worksheets;
                //只导表1
                ExcelWorksheet worksheet = worksheets[1];

                ExportJson(worksheet, Path.GetFileNameWithoutExtension(file.FullName));
                ExportClass(worksheet, Path.GetFileNameWithoutExtension(file.FullName));

            }
            AssetDatabase.Refresh();
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    /// <summary>
    /// 导出类
    /// </summary>
    private static void ExportClass(ExcelWorksheet worksheet, string fileName)
    {
        string[] properties = GetProperties(worksheet);
        StringBuilder sb = new StringBuilder();
        sb.Append("using System;\t\n");
        sb.Append("[Serializable]\t\n");
        sb.Append($"public class {fileName}Config\n");//类名
        sb.Append("{\n");

        for (int col = 1; col <= properties.Length; col++)
        {
            string fieldType = GetType(worksheet, col);
            string fieldName = properties[col - 1];
            sb.Append($"\tpublic {fieldType} {fieldName};\n");
        }

        sb.Append("}\n\n");
        Files.SaveFile(classPath, string.Format("{0}Config.cs", fileName), sb.ToString());

    }
    /// <summary>
    /// 导出JSON
    /// </summary>
    private static void ExportJson(ExcelWorksheet worksheet, string fileName)
    {
        string str = "";
        int num = 0;
        string[] properties = GetProperties(worksheet);
        for (int col = 1; col <= properties.Length; col++)
        {
            string[] temp = GetValues(worksheet, col);
            num = temp.Length;
            foreach (var value in temp)
            {
                str += GetJsonK_VFromKeyAndValues(properties[col - 1],
                    Convert(GetType(worksheet, col), value)) + ',';
            }
        }
        //获取key:value的字符串
        str = str.Substring(0, str.Length - 1);
        str = GetJsonFromJsonK_V(str, num);
        str = GetUnityJsonFromJson(str);
        Files.SaveFile(configPath, string.Format("{0}Config.json", fileName), str);
    }

    /// <summary>
    /// 获取属性
    /// </summary>
    private static string[] GetProperties(ExcelWorksheet worksheet)
    {
        string[] properties = new string[worksheet.Dimension.End.Column];
        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
        {
            if (worksheet.Cells[propertyIndex, col].Text == "")
                throw new System.Exception(string.Format("第{0}行第{1}列为空", propertyIndex, col));
            properties[col - 1] = worksheet.Cells[propertyIndex, col].Text;
        }
        return properties;
    }

    /// <summary>
    /// 获取值
    /// </summary>
    private static string[] GetValues(ExcelWorksheet worksheet, int col)
    {
        //容量减去前三行
        string[] values = new string[worksheet.Dimension.End.Row - 3];
        for (int row = valueIndex; row <= worksheet.Dimension.End.Row; row++)
        {
            values[row - valueIndex] = worksheet.Cells[row, col].Text;
        }
        return values;
    }

    /// <summary>
    /// 获取类型
    /// </summary>
    private static string GetType(ExcelWorksheet worksheet, int col)
    {
        return worksheet.Cells[typeIndex, col].Text;
    }

    /// <summary>
    /// 通过类型返回对应值
    /// </summary>
    private static string Convert(string type, string value)
    {
        string res = "";
        switch (type)
        {
            case "int": res = value; break;
            case "int32": res = value; break;
            case "int64": res = value; break;
            case "long": res = value; break;
            case "float": res = value; break;
            case "double": res = value; break;
            case "string": res = $"\"{value}\""; break;
            default:
                throw new Exception($"不支持此类型: {type}");
        }
        return res;
    }

    /// <summary>
    /// 返回key:value
    /// </summary>
    private static string GetJsonK_VFromKeyAndValues(string key, string value)
    {
        return string.Format("\"{0}\":{1}", key, value);
    }

    /// <summary>
    ///获取[key:value]转换为{key:value,key:value},再变成[{key:value,key:value},{key:value,key:value}]
    /// </summary>
    private static string GetJsonFromJsonK_V(string json, int valueNum)
    {
        string str = "";
        string[] strs;
        List<string> listStr = new List<string>();
        strs = json.Split(',');
        listStr.Clear();
        for (int j = 0; j < valueNum; j++)
        {
            listStr.Add("{" + string.Format("{0},{1}", strs[j], strs[j + valueNum]) + "}");
        }
        str = "[";
        foreach (var l in listStr)
        {
            str += l + ',';
        }
        str = str.Substring(0, str.Length - 1);
        str += ']';
        return str;
    }

    /// <summary>
    /// 适应JsonUtility.FromJson函数的转换格式
    /// </summary>
    private static string GetUnityJsonFromJson(string json)
    {
        return "{" + "\"datas\":" + json + "}";
    }

}
*/

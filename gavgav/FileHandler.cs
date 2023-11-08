using gavgav;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace gavgav
{

class FileHandler
{
    private string filePath;

    public FileHandler(string path)
    {
        filePath = path;
    }

    private string GetFileExtension()
    {
        return Path.GetExtension(filePath).TrimStart('.').ToLower();
    }

    public Figure LoadFile()
    {
            string extension = GetFileExtension();
            switch (extension)
            {
                case "txt":
                    return LoadTxt();
                case "json":
                    return LoadJson();
                case "xml":
                    return LoadXml();
                default:
                    Console.WriteLine("Неподдерживаемый формат файла.");
                    return null;
            }
        }
        public void SaveFile(Figure figure, string extension)
        {
            switch (extension)
            {
                case "txt":
                    SaveAsTxt(figure);
                    break;
                case "json":
                    SaveAsJson(figure);
                    break;
                case "xml":
                    SaveAsXml(figure);
                    break;
                default:
                    Console.WriteLine("Неподдерживаемый формат файла.");
                    break;
            }
        }
        private void SaveAsTxt(Figure figure)
    {
        string[] lines = { figure.Name, figure.Width.ToString(), figure.Height.ToString() };
        File.WriteAllLines(filePath, lines);
    }

    private void SaveAsJson(Figure figure)
    {
        string json = JsonConvert.SerializeObject(figure);
        File.WriteAllText(filePath, json);
    }

    private void SaveAsXml(Figure figure)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Figure));
        using (TextWriter writer = new StreamWriter(filePath))
        {
            xmlSerializer.Serialize(writer, figure);
        }
    }

    private Figure LoadTxt()
    {
        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length >= 3)
        {
            string name = lines[0];
            if (double.TryParse(lines[1], out double width) && double.TryParse(lines[2], out double height))
            {
                return new Figure(name, width, height);
            }
        }
        return null;
    }

    private Figure LoadJson()
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<Figure>(json);
    }

    private Figure LoadXml()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Figure));
        using (TextReader reader = new StreamReader(filePath))
        {
            return (Figure)xmlSerializer.Deserialize(reader);
        }
    }
}
}

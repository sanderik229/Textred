using Newtonsoft.Json;
using System;
using System.IO;

public class Figure
{
    public string Name { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}

public class FileIO
{
    public Figure LoadFigure(string filePath)
    {
        string fileExtension = Path.GetExtension(filePath);

        if (fileExtension == ".txt")
        {
            return LoadTxtFigure(filePath);
        }
        else if (fileExtension == ".json")
        {
            return LoadJsonFigure(filePath);
        }
        else if (fileExtension == ".xml")
        {
            return LoadXmlFigure(filePath);
        }
        else
        {
            throw new NotSupportedException("Неподдерживаемый формат файла");
        }
    }

    private Figure LoadTxtFigure(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length < 3)
            throw new InvalidDataException("Недопустимый формат файла .TXT");

        return new Figure
        {
            Name = lines[0],
            Width = double.Parse(lines[1]),
            Height = double.Parse(lines[2])
        };
    }

    private Figure LoadJsonFigure(string filePath)
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string json = sr.ReadToEnd();
            return JsonConvert.DeserializeObject<Figure>(json);
        }
    }

    private Figure LoadXmlFigure(string filePath)
    {
        throw new NotSupportedException("Десериализация XML не реализована.");
    }

    public void SaveFigure(string filePath, Figure figure)
    {
        string fileExtension = Path.GetExtension(filePath);

        if (fileExtension == ".txt")
        {
            SaveTxtFigure(filePath, figure);
        }
        else if (fileExtension == ".json")
        {
            SaveJsonFigure(filePath, figure);
        }
        else if (fileExtension == ".xml")
        {
            SaveXmlFigure(filePath, figure);
        }
        else
        {
            throw new NotSupportedException("Неподдерживаемый формат файла");
        }
    }

    private void SaveTxtFigure(string filePath, Figure figure)
    {
        File.WriteAllText(filePath, $"{figure.Name}\n{figure.Width}\n{figure.Height}");
    }

    private void SaveJsonFigure(string filePath, Figure figure)
    {
        string json = JsonConvert.SerializeObject(figure, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    private void SaveXmlFigure(string filePath, Figure figure)
    {
        throw new NotSupportedException("Сериализация XML не реализована.");
    }
}

public class TextEditor
{
    private Figure currentFigure;
    private FileIO fileIO;

    public TextEditor()
    {
        fileIO = new FileIO();
    }

    public void LoadFile(string filePath)
    {
        currentFigure = fileIO.LoadFigure(filePath);
        Console.WriteLine("Файл успешно загружен.");
    }

    public void EditFigure()
    {
        Console.WriteLine("Редактирование рисунка:");
        Console.WriteLine($"Name: {currentFigure.Name}");
        Console.WriteLine($"Width: {currentFigure.Width}");
        Console.WriteLine($"Height: {currentFigure.Height}");

        Console.WriteLine("Введите новое имя (или нажмите Enter, чтобы сохранить текущее значение):");
        string name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name))
        {
            currentFigure.Name = name;
        }
        Console.WriteLine("Введите новую ширину (или нажмите Enter, чтобы сохранить текущее значение):");
        string widthInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(widthInput))
        {
            if (double.TryParse(widthInput, out double width))
            {
                currentFigure.Width = width;
            }
            else
            {
                Console.WriteLine("Неверный ввод значения ширины. Ширина не изменена.");
            }
        }
        Console.WriteLine("Введите новую высоту (или нажмите Enter, чтобы сохранить текущее значение):");
        string heightInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(heightInput))
        {
            if (double.TryParse(heightInput, out double height))
            {
                currentFigure.Height = height;
            }
            else
            {
                Console.WriteLine("Неверный ввод высоты. Высота не изменена.");
            }
        }

        Console.WriteLine("Рисунок успешно отредактирован.");
    }

    internal void SaveFile(string? saveFilePath)
    {
        throw new NotImplementedException();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Консольное приложение текстового редактора");
        TextEditor textEditor = new TextEditor();

        Console.WriteLine("Введите путь к файлу для загрузки:");
        string filePath = Console.ReadLine();

        try
        {
            textEditor.LoadFile(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return;
        }

        bool running = true;
        while (running)
        {
            Console.WriteLine("Нажмите F1 для сохранения, Escape для выхода или любую другую клавишу для редактирования рисунка.");
            ConsoleKey key = Console.ReadKey().Key;

            if (key == ConsoleKey.F1)
            {
                Console.WriteLine("Введите путь к файлу для сохранения:");
                string saveFilePath = Console.ReadLine();
                try
                {
                    textEditor.SaveFile(saveFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else if (key == ConsoleKey.Escape)
            {
                running = false;
            }
            else
            {
                textEditor.EditFigure();
            }
        }

        Console.WriteLine("Заркылось.");
    }
}
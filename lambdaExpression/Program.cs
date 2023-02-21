using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    class BackpackItem
    {
        public string Name { get; set; }
        public double Volume { get; set; }
        public BackpackItem(string n, double v)
        {
            n = Name;
            v = Volume;
        }
    }
    class Backpack
    {
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public string Material { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public double OccupiedVolume { get; set; }
        public List<BackpackItem> Items { get; set; } = new List<BackpackItem>();

        public event EventHandler<BackpackItem> AddedItem;

        public Backpack(string color, string manufacturer, string material, double weight, double volume)
        {
            Color = color;
            Manufacturer = manufacturer;
            Material = material;
            Weight = weight;
            Volume = volume;
        }

        public void AddItem(BackpackItem item)
        {
            if (OccupiedVolume + item.Volume > Volume)
            {
                throw new Exception("Невозможно добавить объект: превышен объём рюкзака");
            }

            Items.Add(item);
            OccupiedVolume += item.Volume;

            AddedItem?.Invoke(this, item);
        }
    }

    delegate int[] GetRainbowColorRGB(string Name);
    static void Main(string[] args)
    {
        #region Test 1
        GetRainbowColorRGB getRainbowColorRGB = delegate (string Name)
        {
            int[] massRGB = new int[3];
            switch (Name.ToLower())
            {
                case "красный":
                    massRGB = new int[] { 255, 0, 0 };
                    break;
                case "оранжевый":
                    massRGB = new int[] { 255, 165, 0 };
                    break;
                case "желтый":
                    massRGB = new int[] { 255, 255, 0 };
                    break;
                case "зеленый":
                    massRGB = new int[] { 0, 128, 0 };
                    break;
                case "голубой":
                    massRGB = new int[] { 0, 0, 255 };
                    break;
                case "синий":
                    massRGB = new int[] { 75, 0, 130 };
                    break;
                case "фиолетовый":
                    massRGB = new int[] { 238, 130, 238 };
                    break;
                default:
                    throw new ArgumentException("Недопустимое название цвета.");
            }
            return massRGB;
        };

        string colorName = "зеленый";
        int[] rgb = getRainbowColorRGB(colorName);

        Console.WriteLine($"Значение RGB для {colorName} = ({rgb[0]}, {rgb[1]}, {rgb[2]}).");
        #endregion
        Console.WriteLine("---------------------------------------------");
        #region Test 2
        var backpack = new Backpack("Синий", "Deuter", "Нейлон", 1.5, 25);

        backpack.AddedItem += (sender, item) =>
        {
            Console.WriteLine($"Объект {item.Name} добавлен в рюкзак");
        };

        var item1 = new BackpackItem("Книга", 1);
        var item2 = new BackpackItem("Флешка", 0.5);
        var item3 = new BackpackItem("Вода", 2);

        try
        {
            backpack.AddItem(item1);
            backpack.AddItem(item2);
            backpack.AddItem(item3);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadLine();

        #endregion
        Console.WriteLine("---------------------------------------------");
        #region Test 3
        int[] array = {-1,12,-5,-3,6,8,7,6,5,8,7,8,-45,434};
        int count = array.Count(x => x % 7 == 0);
        count -= 1;
        Console.WriteLine(count);
        #endregion
        Console.WriteLine("---------------------------------------------");
        #region Test 4
        int positiv = array.Count(x => x > 0);
        Console.WriteLine(count);
        #endregion
        Console.WriteLine("---------------------------------------------");
        #region Test 5
        var uniqueNegatives = array.Where(x => x < 0).Distinct();
        foreach (var number in uniqueNegatives)
        {
            Console.Write($"{number} ");
        }
        Console.WriteLine();
        #endregion
        Console.WriteLine("---------------------------------------------");
        #region Test 6
        string text = "Меня зовут Даша и я сделала эту домашку :)";
        string word = "Даша";
        var containsWord = text.Split(' ').Any(w => w.ToLower() == word.ToLower());
        Console.WriteLine("Текст содержит слово '{0}': {1}", word, containsWord);
        #endregion
    }

}

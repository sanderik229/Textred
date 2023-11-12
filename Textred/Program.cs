using Newtonsoft.Json;
using Textred;
string[] favoriteColour = new string[] { "Красный", "Розовый", "Оранжевый" };

Human sofia = new Human();
sofia.Name = "София Алексеевна";
sofia.Age = 60;
sofia.MyFavoriteColour = favoriteColour;

Human jenya = new Human();
jenya.Name = "Евгений";
jenya.Age = 16;
jenya.MyFavoriteColour = new string[] { "Темно-зеленый", "Темно-синий" };

Human sasha = new Human();
sasha.Name = "Александр";
sasha.Age = 17;
sasha.MyFavoriteColour = new string[] { "Синий", "Желтый" };

List<Human> humans = new List<Human>();
humans.Add(jenya);
humans.Add(sasha);
humans.Add(sofia);

string json = JsonConvert.SerializeObject(humans);
File.WriteAllText("Macintosh HD/Users/levonbabaan/Desktop/Result.json", json);
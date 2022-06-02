using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SkillBox_Домашнее_задание__7
{
    struct Repository
    {
        public Worker[] workers;

        private string path;

        public int index;

        string[] titles;

        /// <summary>
        /// Метоз для напоминания сохранения данных пользовтелю
        /// </summary>
        public void ActionBeforeClose()
        {
            Console.Write("Сохранить Сотрудников?(да/нет)");
            string answer = Console.ReadLine();
            if (answer == "да")
            {
                Save("data.txt");
                Environment.Exit(0);
            }
            else if (answer == "нет")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Неизвестная команда!!!");
                ActionBeforeClose();
            }
        }
        /// <summary>
        /// Метод для управления всеми процессами программы
        /// </summary>
        public void Menu()
        {
            string answer = Console.ReadLine();
            Console.Clear();
            switch (answer)                 ///переключатель функций программы
            {
                case "Print":
                    PrintDbToConsole();
                    Menu();
                    break;
                case "AddNew":
                    Add(new Worker(index+1));
                    Menu();
                    break;
                case "Delete":
                    Delete();
                    Menu();
                    break;
                case "Sort":
                    SortArray();
                    Menu();
                    break;
                case "Edit":
                    EditingDataBase();
                    Menu();
                    break;
                case "DateRange":
                    LoadDateRange();
                    Menu();
                    break;
                case "Save":
                    Save("data.txt");
                    Menu();
                    break;
                case "Close":
                    ActionBeforeClose();
                    break;
                default:
                    Console.WriteLine(
                                      "AddNew - Добавить Нового Сотрудника \n" +
                                      "Close - Закрыть программу \n" +
                                      "Edit - Редактировать сотрудника\n" +
                                      "DateRange - Сортировать сотрудников по диапазону дат \n" +
                                      "Delete - Удалить Сотрудника \n" +
                                      "Print - Отобразить Базу Данных \n" +
                                      "Save - Сохранить Базу \n" +
                                      "Sort - Сортировать сотрудников по диапазону дат \n"
                                      );
                    Menu();
                    break;

            }
        }
        /// <summary>
        /// Констрктор
        /// </summary>
        /// <param name="Path">Путь в файлу с данными</param>
        public Repository(string Path)
        {
            this.path = Path; // Сохранение пути к файлу с данными
            this.index = 0; // текущая позиция для добавления сотрудника в workers
            this.titles = new string[9]
            {"ID","Дата добавления","Фамилия","Имя","Отчество","Возраст","Рост","Дата рождения","Место рождения" }; // инициализаия массива заголовков   

            this.workers = new Worker[1]; // инициализаия массива сотрудников.    | изначально предпологаем, что данных нет

            this.Load(); // Загрузка данных
            this.Menu();

        }

        /// <summary>
        /// Метод увеличения текущего хранилища
        /// </summary>
        /// <param name="Flag">Условие увеличения</param>
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.workers, this.workers.Length * 2);
            }
        }

        /// <summary>
        /// Метод добавления сотрудника в хранилище
        /// </summary>
        /// <param name="ConcreteWorker">Сотрудник</param>
        public void Add(Worker ConcreteWorker)
        {

            this.Resize(index >= this.workers.Length);
            this.workers[index] = ConcreteWorker;
            this.index++;

        }
        /// <summary>
        /// Метод редактирования сотрудника
        /// </summary>
        public void EditingDataBase()
        {
            Console.Write("Id Сотрудника: ");
            int ID = Convert.ToInt32(Console.ReadLine());//иницализация нужного пользователю сотрудника
            if (ID != 0 && ID < this.workers.Length)//Верхняя и нижняя граница  для поиска по ID
            {
                this.workers[ID - 1] = new Worker(ID);//Замена существующего сотрудника на нового
            }
            else
            {
                Console.WriteLine($"Вы вышли из диапазона!!!");//Выход из границ ID
                EditingDataBase();
            }

        }

        /// <summary>
        /// Метод для удаления сотрудника
        /// </summary>
        public void Delete()
        {
            Console.Write("Id Сотрудника: ");
            int ID = Convert.ToInt32(Console.ReadLine());//иницализация нужного пользователю сотрудника

            if (ID != 0 && ID < this.workers.Length)
            {
                Array.Clear(this.workers, ID - 1, 1);//Очистка массива с нужного индекса на 1 пункт длины массива
            }
            else
            {
                Console.WriteLine($"Вы вышли из диапазона!!!");//Выход из границ ID
                Delete();
            }

        }



        /// <summary>
        /// Метод сортировки сотрудников по диапазону дат рождения
        /// </summary>
        public void LoadDateRange()
        {
            Console.Write($"Начало диапазона: ");
            DateTime begin = Convert.ToDateTime(Console.ReadLine());//Ввод начала диапазона
            Console.Write($"Конец диапазона: ");
            DateTime end = Convert.ToDateTime(Console.ReadLine());//Ввод конца диапазона
            int count = 0;
            var DateRangeWorkers = from p in this.workers //Сортировка по сотрудников по дате рождения
                                   orderby p.DateOfBirth
                                   select p;

            PrintTitle();               //Вывод на консоль названия полей

            foreach (var p in DateRangeWorkers)             //Перебор массива с сотрудниками сотрудники идут по возрастающей по дате рождения
            {
                if (begin <= p.DateOfBirth && end >= p.DateOfBirth)     // если д.р. попадает в диапазон дат 
                {
                    Console.WriteLine($"{p.ID,2} {p.AddDateTime,16} {p.LastName,15} {p.FirstName,12} {p.MiddleName,15}" +      ///Вывод на консоль сотрудника
                                $" {p.Age,9} {p.Height,8} {p.DateOfBirth.ToShortDateString(),10} {p.PlaceOfBirth,5}");
                    count++;
                }
                else continue;  //Игнор сотрудника
            }
            if (count == 0)
            {
                Console.SetCursorPosition((Console.WindowWidth) / 2, Console.CursorTop);
                Console.WriteLine($"Нет совпадений!!!");
            }
        }
    
        /// <summary>
        /// Сортировка по заданному полю 
        /// </summary>
        public void SortArray()
        {  
            Console.WriteLine($"Поле сортировки : ID,Дата Рождения,Фамилия,Имя,отчество," +
                                  $" Возраст,Рост,Дата Рождения,Место Рождения");
            string item =  Convert.ToString(Console.ReadLine());
            switch (item)                       ////Выбор необходимого поля для сортировки
            {
                case"ID":
                    item = "p.ID";
                    break;
                case "Дата Добавление":
                    item = "p.AddDateTime";
                    break;
                case "Фамилия":
                    item = "p.LastName";
                    break;
                case"Имя":
                    item = "p.FirstName";
                    break;
                case "Отчество":
                    item = "p.MiddleName";
                    break;
                case "Возраст":
                    item = "p.Age";
                    break;
                case "Рост":
                    item = "p.Height";
                    break;
                case "Дата Рождения":
                    item = "p.DateOfBirth";
                    break;
                case "Место Рождения":
                    item = "p.PlaceOfBirth";
                    break;
                default:
                    Console.WriteLine($"Неизвестные Поля!!!");
                    Console.ReadLine();
                    Console.Clear();
                    SortArray();
                    break;
            }
            Console.Write($"Сортировать по Возрастанию или Убыванию(true/false)?");  ///Отображение списка по убывающей/возрастающей
            bool value = Convert.ToBoolean(Console.ReadLine());

            PrintTitle();

            if (value)                                          ///сортировка по возрастающей
            {
               var  sortedWorkers1 = from p in this.workers    
                                     orderby item
                                     select p;
                foreach (var p in sortedWorkers1)
                {
                    Console.WriteLine($"{p.ID,2} {p.AddDateTime,16} {p.LastName,15} {p.FirstName,12} {p.MiddleName,15}" +
                                      $" {p.Age,9} {p.Height,8} {p.DateOfBirth,10} {p.PlaceOfBirth,5}");
                }
            }
            else
            {
              var  sortedWorkers1 = this.workers.OrderByDescending(p => item);/// Сортировка по убывающей

                foreach (var p in sortedWorkers1)
                {
                    Console.WriteLine($"{p.ID,2} {p.AddDateTime,16} {p.LastName,15} {p.FirstName,12} {p.MiddleName,15}" +
                                      $" {p.Age,9} {p.Height,8} {p.DateOfBirth.ToShortDateString(),10} {p.PlaceOfBirth,5}");
                }
            }    
            }

        /// <summary>
        /// Метод загрузки данных
        /// </summary>
        private void Load()
        {
            if (!(File.Exists(this.path)))//Проверка файла на существование
            {
                using (File.Create(this.path)) ;//Создание файла
            }
            else
            {
                using (StreamReader sr = new StreamReader(this.path))///  работа с файлом
                {
                    titles = sr.ReadLine().Split('#');              /// инициализация названия полей(разделите #)

                    while (!sr.EndOfStream)                         /// инициализация сотрудников
                    {
                        string[] args = sr.ReadLine().Split('#');
                        Add(new Worker(Convert.ToInt32(args[0]), Convert.ToDateTime(args[1]),/// добвление в массив нового сотрудника по  очереди
                                       args[2], args[3], args[4], Convert.ToInt32(args[5]),
                                       Convert.ToByte(args[6]), Convert.ToDateTime(args[7]), args[8]));
                    }
                }
            }
        }

        /// <summary>
        /// Метод сохранения данных
        /// </summary>
        /// <param name="Path">Путь к файлу сохранения</param>
        public void Save(string Path)
        {
            string temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}",
                                            this.titles[0], this.titles[1], this.titles[2],
                                            this.titles[3], this.titles[4], this.titles[5],
                                            this.titles[6], this.titles[7], this.titles[8]);

            File.WriteAllText(Path, $"{temp}\n");       /// сохранение названия полей

            for (int i = 0; i < this.index; i++)        /// сохранение сотрудников
            {
                if (!(this.workers[i].IsDefault()))     /// если id !=0 
                {
                    temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}",
                                        this.workers[i].ID,
                                        this.workers[i].AddDateTime,
                                        this.workers[i].LastName,
                                        this.workers[i].FirstName,
                                        this.workers[i].MiddleName,
                                        this.workers[i].Age,
                                        this.workers[i].Height,
                                        this.workers[i].DateOfBirth.ToShortDateString(),
                                        this.workers[i].PlaceOfBirth);
                    File.AppendAllText(Path, $"{temp}\n");         /// сохранение сотрудников 
                }
                else continue;
            }
            Console.WriteLine($"База данных сохранена");
        }
       
        /// <summary>
        /// Вывод данных в консоль
        /// </summary>
        public void PrintDbToConsole()
        {
            if (this.index.Equals(0))               /// если отстуствуют сотрудники
            {
                Console.WriteLine("Файл пуст!!!");
            }
            else
            {
                PrintTitle();                   ///Вывод на консоль названия полей

                for (int i = 0; i < index; i++)
                {
                    if (!(this.workers[i].IsDefault()))
                    {

                        Console.WriteLine(this.workers[i].PrintAllWorker());
                    }
                    else continue;

                }
            }
        }
        /// <summary>
        /// Метод для отображения на консоли названия полей
        /// </summary>
        public void PrintTitle()
        {
            Console.WriteLine($"{this.titles[0],2} {this.titles[1],16} {this.titles[2],15} {this.titles[3],12} {this.titles[4],15}" +
                                 $" {this.titles[5],9} {this.titles[6],8} {this.titles[7],10} {this.titles[8],5}");
        }

        /// <summary>
        /// Количество сотрудников в хранилище
        /// </summary>
        public int Count { get { return this.index; } }


    }
}

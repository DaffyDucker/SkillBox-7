using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SkillBox_Домашнее_задание__7
{
    struct Worker
    {
        /// <summary>
        /// Метод для поиска по диапазону дат
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public bool DatesRange(DateTime begin, DateTime end)
        {
            DateTime concreteDate = this.DateOfBirth;

            if (begin <= end && (begin <= concreteDate))
            {
                if (end > concreteDate)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///  метод для поиска дефолтного занчения id
        /// </summary>
        /// <returns></returns>
        public bool IsDefault()
        {
            if (this.id == 0)
            {
                return true;
            }
            else return false;
        }
        #region Функции программы
        /// <summary>
        /// Вывод на консоль пользователя
        /// </summary>
        /// <returns></returns>
        public string PrintAllWorker()
        {
        
            return $"{this.id,2} {this.addDateTime,15} {this.lastName,12} {this.firstName,13} {this.middleName,13}"+
                $"{this.age,10} {this.height,9} {this.dateOfBirth.ToShortDateString(),12} {this.placeOfBirth,10}";
        }
        #endregion
      
        /// <summary>
        /// Переключатель  для добавления нового пользователя
        /// </summary>
        /// <param name="Item"></param>
        public void Message(string Item)
        {
            switch (Item)
            {
                case "ID":
                    Console.WriteLine("Id Сотрудника: "+ID);
                    break;
                case "Name":
                    Console.Write("Имя Сотрудника: ");
                    break;
                case "Middle":
                    Console.Write("Отчество Сотрудника: ");
                    break;
                case "LastName":
                    Console.Write("Фамилия Сотрудника: ");
                    break;
                case "Age":
                    Console.WriteLine("Возраст Сотрудника: "+age);
                    break;
                case "Height":
                    Console.Write("Рост Сотрудника: ");
                    break;
                case "DateOfBirth":
                    Console.Write("Дата Рождения Сотрудника (в формате ДД.ММ.ГГГГ): ");
                    break;
                case "PlaceOfBirth":
                    Console.Write("Место Рождения Сотрудника: г. ");
                    break;

            }
        }
        #region Свойства
        public int ID { get { return this.id; } set { this.id = value; } }
        public DateTime AddDateTime { get { return this.addDateTime; } set { this.addDateTime = value; } }
        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }
        public string MiddleName { get { return this.middleName; } set { this.middleName = value; } }
        public string LastName { get { return this.lastName; } set { this.lastName = value; } }
        public int Age { get { return this.age; } set { this.age = value; } }
        public byte Height { get { return this.height; } set { this.height = value; } }
        public DateTime DateOfBirth { get { return this.dateOfBirth; } set {
                                                                            DateTime low = DateTime.Now.AddYears(-14);
                                                                            DateTime high = DateTime.Now.AddYears(-65);

                                                                            if (value > high && value < low) this.dateOfBirth = value;

                                                                            else
                                                                            {
                                                                                Console.WriteLine("Возраст не может быть менее 14 лет и более 65 лет");
                                                                                Message("DateOfBirth");
                                                                                this.DateOfBirth = DateTime.Parse(Console.ReadLine());
                                                                            }
            } }
        public string PlaceOfBirth { get { return this.placeOfBirth; } set { this.placeOfBirth = value; } }
        #endregion

        #region Поля
        private int id;
        private DateTime addDateTime;
        private string firstName;
        private string middleName;
        private string lastName;
        private int age;
        private byte height;
        private DateTime dateOfBirth;
        private string placeOfBirth;
        #endregion

        #region Конструктор
        public Worker(int ID, DateTime AddDateTime, string LastName,
                       string FirstName,string MiddleName, int Age,
                       byte Height, DateTime DateOfBirth, string PlaceOfBirth)
        {
            this.id = ID;
            this.addDateTime = AddDateTime;
            this.lastName = LastName;
            this.firstName = FirstName;
            this.middleName = MiddleName;
            this.age = Age;
            this.height = Height;
            this.dateOfBirth = DateOfBirth;
            this.placeOfBirth = PlaceOfBirth;
        }


        public Worker(int ID) :
            this()
        {

            Message("ID");
            this.id = ID;


            Message("LastName");
            this.LastName = Convert.ToString(Console.ReadLine());

            this.addDateTime = DateTime.Now;
            Message("Name");
            this.firstName = Console.ReadLine();
            Message("Middle");
            this.middleName = Console.ReadLine();
           
            Message("DateOfBirth");

             this.DateOfBirth = DateTime.Parse(Console.ReadLine());

            int age = addDateTime.Year - this.DateOfBirth.Year;

            this.age = age ;
            Message("Age");

            Message("Height");
            this.height = Convert.ToByte(Console.ReadLine());

            Message("PlaceOfBirth");
            this.placeOfBirth = Console.ReadLine();
        
            #endregion

        }
    }
}

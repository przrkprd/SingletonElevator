using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonElevator
{
    // Класс лифта.
    // Singleton гарантирует, что лифт будет только один.
    class Elevator
    {
        // Храним единственный экземпляр лифта.
        private static Elevator instance;

        // Закрытый конструктор запрещает создавать лифт через new.
        private Elevator()
        {
            CurrentFloor = 1;
        }

        // Получение единственного экземпляра лифта.
        public static Elevator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Elevator();
                }

                return instance;
            }
        }

        // Текущий этаж лифта.
        public int CurrentFloor { get; private set; }

        // Перемещение лифта на указанный этаж.
        public void MoveToFloor(int floor)
        {
            CurrentFloor = floor;

            Console.WriteLine(
                "Лифт переместился на этаж " + CurrentFloor);
        }
    }

    // Класс помещения.
    class Room
    {
        public string Name { get; set; }

        public Room(string name)
        {
            Name = name;
        }
    }

    // Класс этажа.
    // Каждый этаж содержит список помещений.
    class Floor
    {
        public int Number { get; set; }

        public List<Room> Rooms { get; set; }

        public Floor(int number)
        {
            Number = number;
            Rooms = new List<Room>();
        }

        // Добавление помещения на этаж.
        public void AddRoom(string name)
        {
            Rooms.Add(new Room(name));
        }

        // Вывод информации об этаже.
        public void ShowInfo()
        {
            Console.WriteLine("Этаж " + Number);

            foreach (Room room in Rooms)
            {
                Console.WriteLine("  Помещение: " + room.Name);
            }
        }
    }

    // Класс здания.
    // Здание также реализовано как Singleton.
    class Building
    {
        // Единственный экземпляр здания.
        private static Building instance;

        // Закрытый конструктор запрещает создавать другие здания.
        private Building()
        {
            Floors = new List<Floor>();
        }

        // Получение единственного здания.
        public static Building Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Building();
                }

                return instance;
            }
        }

        // Список этажей здания.
        public List<Floor> Floors { get; private set; }

        // Получаем единственный лифт.
        public Elevator Elevator
        {
            get { return Elevator.Instance; }
        }

        // Добавляем этаж в здание.
        public void AddFloor(int number)
        {
            Floors.Add(new Floor(number));
        }

        // Выводим информацию обо всём здании.
        public void ShowInfo()
        {
            Console.WriteLine("\nИнформация о здании:");

            foreach (Floor floor in Floors)
            {
                floor.ShowInfo();
            }

            Console.WriteLine(
                "Текущий этаж лифта: " + Elevator.CurrentFloor);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Получаем единственное здание.
            Building building = Building.Instance;

            // Добавляем этажи.
            building.AddFloor(1);
            building.AddFloor(2);
            building.AddFloor(3);

            // Добавляем помещения на этажи.
            building.Floors[0].AddRoom("Холл");
            building.Floors[0].AddRoom("Охрана");

            building.Floors[1].AddRoom("Кабинет 201");
            building.Floors[1].AddRoom("Кабинет 202");

            building.Floors[2].AddRoom("Спортзал");

            // Получаем единственный лифт.
            Elevator elevator = building.Elevator;

            // Перемещаем лифт.
            elevator.MoveToFloor(3);

            // Проверяем, что здание остаётся единственным.
            Building anotherBuilding = Building.Instance;

            // Проверяем, что лифт остаётся единственным.
            Elevator anotherElevator = Elevator.Instance;

            Console.WriteLine(
                "\nЗдание одно: " +
                ReferenceEquals(building, anotherBuilding));

            Console.WriteLine(
                "Лифт один: " +
                ReferenceEquals(elevator, anotherElevator));

            // Выводим состояние здания.
            building.ShowInfo();

            Console.ReadKey();
        }
    }
}

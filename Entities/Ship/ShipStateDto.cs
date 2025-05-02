namespace Anno1800.Entities.Ship
{
    public class ShipStateDto
    {
        public string Name { get; set; } = "Лютик";
        public ShipType Type { get; set; }  // Военный, торговый и т.д.
        public int AttackPower { get; set; } // Атака
        public int Defense { get; set; } // Защита
        public int Speed { get; set; } // Скорость передвижения
        public int CargoCapacity { get; set; } // Вместимость трюма
        public List<Resource> Resources { get; set; } = new(); // Груз

        // Тип корабля
        public enum ShipType
        {
            Military, // Военный
            Trading,  // Торговый
            Exploration // Исследовательский
        }

        // Ресурсы на борту
        public class Resource
        {
            public string Name { get; set; } = "Дерево";
            public int Quantity { get; set; }
        }
    }
}
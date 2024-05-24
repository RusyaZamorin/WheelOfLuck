using WheelOfLuck.Enums;
using WheelOfLuck.Interfaces;

namespace WheelOfLuck.Sample.Bonuses
{
    public class CollectItemBonus : IBonus
    {
        private ItemsCollector itemsCollector;
        private string itemName;
        
        public string Name { get; }
        public string Description => Name;
        public BonusType Type => BonusType.NonConsumable;
        public double Weight { get; }

        public void Activate()
        {
            itemsCollector.CollectItem(itemName);
        }

        public CollectItemBonus(string name, string itemName, double weight, ItemsCollector itemsCollector)
        {
            this.itemName = itemName;
            this.itemsCollector = itemsCollector;
            Name = name;
            Weight = weight;
        }
    }
}
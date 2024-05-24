using WheelOfLuck.Enums;
using WheelOfLuck.Interfaces;

namespace WheelOfLuck.Sample.Bonuses
{
    public class SpecialMoneyBonus : IBonus
    {
        private Money money;
        private int value;

        public string Name { get; }
        public string Description => $"{value} gems";
        public BonusType Type => BonusType.Consumable;
        public double Weight { get; }

        public void Activate() => 
            money.AddSpecial(value);

        public SpecialMoneyBonus(string name, int value, double weight, Money money)
        {
            this.value = value;
            this.money = money;
            Name = name;
            Weight = weight;
        }
    }
}
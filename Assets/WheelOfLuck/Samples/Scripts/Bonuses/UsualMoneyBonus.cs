using WheelOfLuck.Enums;
using WheelOfLuck.Interfaces;

namespace WheelOfLuck.Sample.Bonuses
{
    public class UsualMoneyBonus : IBonus
    {
        private Money money;
        private int value;

        public string Name { get; }
        public string Description => $"{value}$";
        public BonusType Type => BonusType.Consumable;
        public double Weight { get; }

        public UsualMoneyBonus(string name, int value, double weight, Money money)
        {
            this.value = value;
            this.money = money;
            Name = name;
            Weight = weight;
        }

        public void Activate() => 
            money.AddUsual(value);
    }
}
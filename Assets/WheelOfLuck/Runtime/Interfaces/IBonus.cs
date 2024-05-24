using WheelOfLuck.Enums;

namespace WheelOfLuck.Interfaces
{
    public interface IBonus
    {
        string Name { get; }
        string Description { get; }
        BonusType Type { get; }
        double Weight { get; }

        void Activate();
    }
}
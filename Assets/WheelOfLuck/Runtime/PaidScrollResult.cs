using WheelOfLuck.Enums;
using WheelOfLuck.Interfaces;

namespace WheelOfLuck
{
    public class PaidScrollResult
    {
        public IBonus Bonus;
        public ScrollFailType FailType;
        
        public bool Success => FailType == ScrollFailType.None;

        public PaidScrollResult(IBonus bonus, ScrollFailType failType = ScrollFailType.None)
        {
            Bonus = bonus;
            FailType = failType;
        }
    }
}
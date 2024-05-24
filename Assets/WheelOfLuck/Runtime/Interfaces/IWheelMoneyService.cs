using WheelOfLuck.Enums;

namespace WheelOfLuck.Interfaces
{
    public interface IWheelMoneyService
    {
        bool BuyScroll(int cost, MoneyType moneyType);
    }
}
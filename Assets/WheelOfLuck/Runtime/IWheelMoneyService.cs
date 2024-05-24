using WheelOfLuck.Enums;

namespace WheelOfLuck
{
    public interface IWheelMoneyService
    {
        bool BuyScroll(int cost, MoneyType moneyType);
    }
}
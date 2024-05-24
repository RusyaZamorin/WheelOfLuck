using UnityEngine;
using UnityEngine.UI;
using WheelOfLuck.Enums;

namespace WheelOfLuck.Sample
{
    public class SampleBootstrap : MonoBehaviour
    {
        [SerializeField] private Money money;
        [SerializeField] private WheelSettingsData wheelSettingsData;
        [SerializeField] private WheelPresenter wheelPresenter;

        [SerializeField] private Button scrollFreeButton;
        [SerializeField] private Button scrollPaidUsualButton;
        [SerializeField] private Button scrollPaidSpecialButton;

        private LuckWheel luckWheel;

        private void Awake()
        {
            luckWheel = new LuckWheel(wheelSettingsData.GetSettings(), wheelPresenter, money);

            scrollFreeButton.onClick.AddListener(FreeScroll);
            scrollPaidUsualButton.onClick.AddListener(PaidByUsualMoney);
            scrollPaidSpecialButton.onClick.AddListener(PaidBySpecialMoney);
        }

        private async void FreeScroll()
        {
            var receivedBonus = await luckWheel.FreeScroll();
            Debug.Log($"Free scroll result: {receivedBonus.Name}");
        }

        private async void PaidByUsualMoney()
        {
            var paidScrollResult = await luckWheel.PaidScroll();
            Debug.Log(paidScrollResult.Success
                ? $"Paid scroll success. Bonus: {paidScrollResult.Bonus.Name}"
                : $"Paid scroll fail. FailType: {paidScrollResult.FailType}");
        }

        private async void PaidBySpecialMoney()
        {
            var paidScrollResult = await luckWheel.PaidScroll(MoneyType.Special);
            Debug.Log(paidScrollResult.Success
                ? $"Paid scroll success. Bonus: {paidScrollResult.Bonus.Name}"
                : $"Paid scroll fail. FailType: {paidScrollResult.FailType}");
        }
    }
}
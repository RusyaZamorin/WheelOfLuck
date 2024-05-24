using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WheelOfLuck.Enums;
using WheelOfLuck.Interfaces;
using WheelOfLuck.Sample.Bonuses;

namespace WheelOfLuck.Sample
{
    public class SampleBootstrap : MonoBehaviour
    {
        [SerializeField] private Money money;
        [SerializeField] private ItemsCollector itemsCollector;
        [SerializeField] private WheelSettingsData wheelSettingsData;
        [SerializeField] private WheelPresenter wheelPresenter;

        [SerializeField] private Button scrollFreeButton;
        [SerializeField] private Button scrollPaidUsualButton;
        [SerializeField] private Button scrollPaidSpecialButton;
        [SerializeField] private Button reGenerateButton;

        private LuckWheel luckWheel;

        private void Awake()
        {
            scrollFreeButton.onClick.AddListener(FreeScroll);
            scrollPaidUsualButton.onClick.AddListener(PaidByUsualMoney);
            scrollPaidSpecialButton.onClick.AddListener(PaidBySpecialMoney);
            reGenerateButton.onClick.AddListener(() => luckWheel.Generate());
            
            var bonuses = new List<IBonus>
            {
                new UsualMoneyBonus("UsualMoney5", 5, 0.8, money),
                new UsualMoneyBonus("UsualMoney10", 10, 0.9, money),
                new UsualMoneyBonus("UsualMoney25", 25, 0.7, money),
                new UsualMoneyBonus("UsualMoney50", 50, 0.4, money),
                new UsualMoneyBonus("UsualMoney100", 100, 0.1, money),
                new UsualMoneyBonus("UsualMoney1000", 1000, 0.01, money),

                new SpecialMoneyBonus("Gem1", 1, 0.3, money),
                new SpecialMoneyBonus("Gem2", 2, 0.1, money),
                new SpecialMoneyBonus("Gem10", 2, 0.01, money),

                new CollectItemBonus("Item1", "Item1", 0.4, itemsCollector),
                new CollectItemBonus("Item2", "Item2", 0.2, itemsCollector),
                new CollectItemBonus("Item3", "Item3", 0.1, itemsCollector),
            };
            var settings = wheelSettingsData.GetSettings(bonuses);
            
            luckWheel = new LuckWheel(settings, wheelPresenter, money);
            
            luckWheel.Generate();
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
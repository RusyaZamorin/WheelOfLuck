using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using WheelOfLuck.Enums;
using WheelOfLuck.Interfaces;

namespace WheelOfLuck
{
    public class LuckWheel
    {
        private readonly Random random = new();

        private IWheelMoneyService moneyService;
        private WheelSettings settings;
        private IWheelPresenter wheelPresenter;
        private bool isScrolling;
        
        private int numberOfScroll;
        private int numberOfFreeScroll;
        private List<IBonus> actualBonuses = new();
        private List<IBonus> receivedNonConsumableBonuses = new();


        public int NumberOfScroll => numberOfScroll;
        public List<IBonus> Content => actualBonuses;

        private bool AllNonConsumablesIsReceived => settings.Bonuses
            .Where(b => b.Type == BonusType.NonConsumable)
            .All(b => receivedNonConsumableBonuses.Contains(b));

        public event Action OnScroll;
        public event Action<ScrollFailType> OnScrollFailed;


        public LuckWheel(WheelSettings settings, IWheelPresenter wheelPresenter, IWheelMoneyService moneyService)
        {
            this.wheelPresenter = wheelPresenter;
            this.settings = settings;
            this.moneyService = moneyService;
        }

        public void Generate()
        {
            var preset = settings.GetPresetFor(numberOfScroll);
            if (preset != null)
            {
                actualBonuses = preset.ContentNames.Select(n => settings.GetBonusByName(n)).ToList();
            }
            else
            {
                actualBonuses.Clear();
                if (AllNonConsumablesIsReceived)
                {
                    actualBonuses.AddRange(GetRandomBonusesByType(BonusType.Consumable, settings.Capacity));
                }
                else
                {
                    actualBonuses.AddRange(GetRandomBonusesByType(BonusType.Consumable));
                    actualBonuses.AddRange(GetRandomBonusesByType(BonusType.NonConsumable));
                }
            }

            wheelPresenter.Generate(actualBonuses);
        }

        public UniTask<IBonus> FreeScroll()
        {
            numberOfFreeScroll++;
            return Scroll();
        }

        public async UniTask<PaidScrollResult> PaidScroll(MoneyType moneyType = MoneyType.Usual)
        {
            if (numberOfFreeScroll < settings.CountFreeScrolls)
                return new PaidScrollResult(await FreeScroll());

            if (moneyService.BuyScroll(settings.ScrollCosts[moneyType], moneyType))
                return new PaidScrollResult(await Scroll());

            OnScrollFailed?.Invoke(ScrollFailType.NotEnoughMoney);
            return new PaidScrollResult(null, ScrollFailType.NotEnoughMoney);
        }

        private async UniTask<IBonus> Scroll()
        {
            var preset = settings.GetPresetFor(numberOfScroll);
            var resultBonus = preset != null
                ? settings.GetBonusByName(preset.ResultBonusName)
                : RandomizeBonus(actualBonuses);

            await wheelPresenter.Scroll(resultBonus, settings.ScrollingSpeed);
            numberOfScroll++;
            OnScroll?.Invoke();

            resultBonus.Activate();

            if (settings.GetPresetFor(numberOfScroll) != null)
            {
                Generate();
                return resultBonus;
            }

            if (resultBonus.Type == BonusType.NonConsumable)
            {
                receivedNonConsumableBonuses.Add(resultBonus);
                Generate();
            }

            return resultBonus;
        }

        private List<IBonus> GetRandomBonusesByType(BonusType bonusType) =>
            GetRandomBonusesByType(bonusType, settings.CountBonusesByTypes[bonusType]);

        private List<IBonus> GetRandomBonusesByType(BonusType bonusType, int count)
        {
            var bonuses = settings.Bonuses
                .Where(b =>
                {
                    if (bonusType == BonusType.NonConsumable)
                        return b.Type == BonusType.NonConsumable && !receivedNonConsumableBonuses.Contains(b);

                    return b.Type == bonusType;
                })
                .ToList();
            
            return GetRandomBonusesFrom(bonuses, count);
        }

        private List<IBonus> GetRandomBonusesFrom(List<IBonus> bonuses, int count)
        {
            var result = new List<IBonus>();
            if (bonuses.Count == 1)
            {
                result.Add(bonuses.First());
                return result;
            }

            return bonuses.OrderBy(b => Guid.NewGuid()).ToList().GetRange(0,count);
        }

        private IBonus RandomizeBonus(List<IBonus> bonuses)
        {
            var totalWeight = bonuses.Sum(b => b.Weight);
            var randomValue = random.NextDouble() * totalWeight;

            return bonuses.FirstOrDefault(b => (randomValue -= b.Weight) < 0);
        }
    }
}
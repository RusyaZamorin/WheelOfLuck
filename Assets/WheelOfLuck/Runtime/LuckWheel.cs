using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using WheelOfLuck.Enums;

namespace WheelOfLuck
{
    public class LuckWheel
    {
        private readonly Random random = new();

        private IWheelMoneyService moneyService;
        private WheelSettings settings;
        private IWheelPresenter wheelPresenter;

        private int numberOfScroll;
        private int numberOfFreeScroll;
        private List<IBonus> actualBonuses = new();
        private List<IBonus> receivedConsumableBonuses = new();


        public int NumberOfScroll => numberOfScroll;
        public List<IBonus> Content => actualBonuses;


        public event Action OnGenerated;
        public event Action OnFreeScroll;
        public event Action OnPaidScroll;
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
            actualBonuses.Clear();
            actualBonuses.AddRange(GetRandomBonusesByType(BonusType.Consumable));
            actualBonuses.AddRange(GetRandomBonusesByType(BonusType.NonConsumable));

            wheelPresenter.Generate(actualBonuses);

            OnGenerated?.Invoke();
        }

        public UniTask<IBonus> FreeScroll()
        {
            OnFreeScroll?.Invoke();
            numberOfFreeScroll++;
            return Scroll();
        }

        public async UniTask<PaidScrollResult> PaidScroll(MoneyType moneyType = MoneyType.Usual)
        {
            if (numberOfFreeScroll < settings.CountFreeScrolls)
                return new PaidScrollResult(await FreeScroll());

            if (moneyService.BuyScroll(settings.ScrollCosts[moneyType], moneyType))
            {
                OnPaidScroll?.Invoke();
                return new PaidScrollResult(await Scroll());
            }

            OnScrollFailed?.Invoke(ScrollFailType.NotEnoughMoney);
            return new PaidScrollResult(null, ScrollFailType.NotEnoughMoney);
        }

        private async UniTask<IBonus> Scroll()
        {
            var resultBonus = GetRandomBonusFrom(actualBonuses);
            await wheelPresenter.Scroll(resultBonus, settings.ScrollingSpeed);
            resultBonus.Activate();

            numberOfScroll++;
            OnScroll?.Invoke();

            if (resultBonus.Type == BonusType.Consumable)
            {
                receivedConsumableBonuses.Add(resultBonus);
                ReplaceReceivedConsumable(resultBonus);
            }

            return resultBonus;
        }

        private void ReplaceReceivedConsumable(IBonus bonus)
        {
            var bonuses = settings.Bonuses
                .Where(b => b.Type == BonusType.Consumable && !receivedConsumableBonuses.Contains(b))
                .ToList();
            
            var replacedItemIndex = actualBonuses.IndexOf(bonus);
            actualBonuses[replacedItemIndex] = GetRandomBonusesFrom(bonuses, 1).First();
            
            wheelPresenter.UpdateBonuses(actualBonuses);
        }

        private List<IBonus> GetRandomBonusesByType(BonusType bonusType)
        {
            var bonuses = settings.Bonuses
                .Where(b =>
                {
                    if (bonusType == BonusType.Consumable)
                        return b.Type == BonusType.Consumable && !receivedConsumableBonuses.Contains(b);

                    return b.Type == bonusType;
                })
                .ToList();

            return GetRandomBonusesFrom(bonuses, settings.CountBonusesByTypes[bonusType]);
        }

        private List<IBonus> GetRandomBonusesFrom(List<IBonus> bonuses, int count)
        {
            var result = new List<IBonus>();

            if (bonuses.Count == 1)
            {
                result.Add(bonuses.First());
                return result;
            }

            for (var i = 0; i < count; i++)
                result.Add(GetRandomBonusFrom(bonuses));

            return result;
        }

        private IBonus GetRandomBonusFrom(List<IBonus> bonuses)
        {
            var totalWeight = bonuses.Sum(b => b.Weight);
            var randomValue = random.NextDouble() * totalWeight;

            var randomBonus = bonuses.FirstOrDefault(bonus => randomValue < bonus.Weight);
            return randomBonus;
        }
    }
}
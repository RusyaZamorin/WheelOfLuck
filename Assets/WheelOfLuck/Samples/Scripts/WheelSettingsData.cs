using System.Collections.Generic;
using UnityEngine;
using WheelOfLuck.Enums;
using WheelOfLuck.Interfaces;

namespace WheelOfLuck.Sample
{
    [CreateAssetMenu(fileName = nameof(WheelSettingsData), 
        menuName = nameof(WheelOfLuck)+ "/"+ nameof(Sample) + "/" + nameof(WheelSettingsData), order = 0)]

    public class WheelSettingsData : ScriptableObject
    {
        [SerializeField] private float scrollingSpeed;
        [SerializeField] private int capacity;
        [SerializeField] private int countConsumableBonuses;
        [SerializeField] private int countNonConsumableBonuses;
        [SerializeField] private int countFreeScrolls;
        [SerializeField] private int spinCostUsual;
        [SerializeField] private int spinCostSpecial;
        [SerializeField] private List<ScrollPreset> presets;

        public WheelSettings GetSettings(List<IBonus> bonuses)
        {
            var countBonusesByTypes = new Dictionary<BonusType, int>()
            {
                [BonusType.Consumable] = countConsumableBonuses,
                [BonusType.NonConsumable] = countNonConsumableBonuses,
            };

            var scrollCosts = new Dictionary<MoneyType, int>()
            {
                [MoneyType.Usual] = spinCostUsual,
                [MoneyType.Special] = spinCostSpecial,
            };
            
            return new WheelSettings(
                scrollingSpeed,
                capacity,
                countBonusesByTypes, 
                bonuses,
                countFreeScrolls,
                scrollCosts,
                presets);
        }
    }
}
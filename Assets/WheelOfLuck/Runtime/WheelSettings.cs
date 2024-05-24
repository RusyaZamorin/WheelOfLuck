using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WheelOfLuck.Enums;
using WheelOfLuck.Interfaces;

namespace WheelOfLuck
{
    public class WheelSettings
    {
        public List<IBonus> Bonuses;
        public int Capacity;
        public Dictionary<BonusType, int> CountBonusesByTypes;
        public int CountFreeScrolls;
        public Dictionary<MoneyType, int> ScrollCosts;
        public List<ScrollPreset> Presets;
        public float ScrollingSpeed;

        public WheelSettings(
            float scrollingSpeed,
            int capacity,
            Dictionary<BonusType, int> countBonusesByTypes,
            List<IBonus> bonuses,
            int countFreeScrolls,
            Dictionary<MoneyType, int> scrollCosts,
            List<ScrollPreset> presets)
        {
            ScrollingSpeed = scrollingSpeed;
            Capacity = capacity;
            CountBonusesByTypes = countBonusesByTypes;
            Bonuses = bonuses;
            CountFreeScrolls = countFreeScrolls;
            ScrollCosts = scrollCosts;
            Presets = presets;

            CheckCapacity();
        }

        public ScrollPreset GetPresetFor(int numberOfScroll) => 
            numberOfScroll < Presets.Count ? Presets[numberOfScroll] : null;

        private void CheckCapacity()
        {
            if (CountBonusesByTypes.Sum(i => i.Value) > Capacity)
                Debug.LogError("Sum of CountBonusesByTypes is greater than wheel Capacity. Change wheel settings");
        }
    }
}
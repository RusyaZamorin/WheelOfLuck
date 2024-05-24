using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WheelOfLuck.Enums;
using WheelOfLuck.Interfaces;

namespace WheelOfLuck.Sample
{
    public class Money : MonoBehaviour, IWheelMoneyService
    {
        [SerializeField] private TMP_Text usualMoneyField;
        [SerializeField] private TMP_Text specialMoneyField;

        private Dictionary<MoneyType, int> moneyCount;
        
        public bool BuyScroll(int cost, MoneyType moneyType)
        {
            if (moneyCount[moneyType] >= cost)
            {
                moneyCount[moneyType] -= cost;
                UpdateField(moneyType);
                return true;
            }

            return false;

        }

        private void UpdateField(MoneyType moneyType)
        {
            var moneyField = moneyType switch
            {
                MoneyType.Usual => usualMoneyField,
                MoneyType.Special => specialMoneyField,
            };
            moneyField.text = moneyCount[moneyType].ToString();
        }
    }
}

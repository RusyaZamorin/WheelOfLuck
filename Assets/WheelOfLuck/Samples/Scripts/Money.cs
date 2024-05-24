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

        private Dictionary<MoneyType, int> moneyCount = new()
        {
            [MoneyType.Usual] = 100,
            [MoneyType.Special] = 5,
        };


        private void Awake()
        {
            UpdateField(MoneyType.Usual);
            UpdateField(MoneyType.Special);
        }

        public void AddUsual(int count)
        {
            moneyCount[MoneyType.Usual] += count;
            UpdateField(MoneyType.Usual);
        }

        public void AddSpecial(int count)
        {
            moneyCount[MoneyType.Special] += count;
            UpdateField(MoneyType.Special);
        }

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
            
            var postfix = moneyType switch
            {
                MoneyType.Usual => "$",
                MoneyType.Special => " gem",
            };
            
            moneyField.text = $"{moneyCount[moneyType]}{postfix}";
        }
    }
}

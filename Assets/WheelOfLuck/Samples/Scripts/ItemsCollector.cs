using TMPro;
using UnityEngine;

namespace WheelOfLuck.Sample
{
    public class ItemsCollector : MonoBehaviour
    {
        [SerializeField] private TMP_Text itemsField;

        private void Awake()
        {
            itemsField.text = "CollectedItems:";
        }

        public void CollectItem(string itemName)
        {
            itemsField.text += $"\n{itemName}";
        }
    }
}
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WheelOfLuck.Interfaces;

namespace WheelOfLuck.Sample
{
    public class WheelPresenter : MonoBehaviour, IWheelPresenter
    {
        public void Generate(List<IBonus> bonuses)
        {
            
        }

        public void UpdateBonuses(List<IBonus> bonuses)
        {
            
        }

        public async UniTask Scroll(IBonus result, float speed)
        {
            
        }
    }
}
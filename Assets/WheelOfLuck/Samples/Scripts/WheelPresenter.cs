using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WheelOfLuck.Interfaces;

namespace WheelOfLuck.Sample
{
    public class WheelPresenter : MonoBehaviour, IWheelPresenter
    {
        [SerializeField] private SpinWheelManager spin;
        
        public void Generate(List<IBonus> bonuses)
        {
            spin.items = bonuses.Select(b =>
            {
                var item = new SpinWheelManager.SpinItem
                {
                    text = b.Description,
                    color = Random.ColorHSV()
                };
                return item;
            }).ToList();
            
            spin.AutoGenerateSpin();
        }

        public async UniTask Scroll(IBonus result, float speed)
        {
            spin.DoSpin(spin.items.FindIndex(i => i.text == result.Description));
            
            await UniTask.WaitUntil(() => spin.IsSpinFinished);
        }
    }
}
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace WheelOfLuck.Interfaces
{
    public interface IWheelPresenter
    {
        void Generate(List<IBonus> bonuses);
        
        UniTask Scroll(IBonus result, float speed);
    }
}
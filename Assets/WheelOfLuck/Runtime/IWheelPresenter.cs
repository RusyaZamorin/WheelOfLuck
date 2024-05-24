﻿using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace WheelOfLuck
{
    public interface IWheelPresenter
    {
        void Generate(List<IBonus> bonuses);

        void UpdateBonuses(List<IBonus> bonuses);

        UniTask Scroll(IBonus result, float speed);
    }
}
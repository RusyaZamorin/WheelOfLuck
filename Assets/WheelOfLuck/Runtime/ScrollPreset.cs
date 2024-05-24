using System;
using System.Collections.Generic;
using WheelOfLuck.Interfaces;

namespace WheelOfLuck
{
    [Serializable]
    public class ScrollPreset
    {
        public IBonus Result;
        public List<IBonus> Content;
    }
}
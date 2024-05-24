using System;
using System.Collections.Generic;

namespace WheelOfLuck
{
    [Serializable]
    public class ScrollPreset
    {
        public IBonus Result;
        public List<IBonus> Content;
    }
}
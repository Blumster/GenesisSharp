using System;
using System.Collections.Generic;

namespace Genesis.Shared.Structures
{
    public class WeatherContainer
    {
        public String Effect;
        public List<String> Environments = new List<String>();
        public List<WeatherInfo> Weathers = new List<WeatherInfo>();
    }
}

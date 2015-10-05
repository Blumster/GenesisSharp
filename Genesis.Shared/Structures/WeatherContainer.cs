using System.Collections.Generic;

namespace Genesis.Shared.Structures
{
    public class WeatherContainer
    {
        public string Effect;
        public List<string> Environments = new List<string>();
        public List<WeatherInfo> Weathers = new List<WeatherInfo>();
    }
}

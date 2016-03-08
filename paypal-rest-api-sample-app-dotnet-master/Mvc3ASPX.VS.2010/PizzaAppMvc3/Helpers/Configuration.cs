using System.Collections.Generic;

namespace PizzaAppMvc3
{
    public static class Configuration
    {
        public static Dictionary<string, string> GetConfiguration()
        {
            Dictionary<string, string> configurationMap = new Dictionary<string, string>();
            configurationMap.Add("mode", "sandbox");
            return configurationMap;
        }    
    }
}
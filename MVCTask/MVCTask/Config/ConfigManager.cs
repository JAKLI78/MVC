using System.Configuration;
using MVCTask.Interface;

namespace MVCTask.Config
{
    public class ConfigManager : IConfig
    {
        public string GetSittingsValueByKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
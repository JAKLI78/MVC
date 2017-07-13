﻿using MVCTask.Config;
using MVCTask.Interface;
using StructureMap.Configuration.DSL;

namespace MVCTask.Registers
{
    public class ConfigManagerRegister : Registry
    {
        public ConfigManagerRegister()
        {
            For<IConfig>().Use<ConfigManager>();
        }
    }
}
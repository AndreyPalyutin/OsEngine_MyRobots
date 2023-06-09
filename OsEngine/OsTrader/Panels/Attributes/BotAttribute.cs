﻿﻿namespace OsEngine.OsTrader.Panels.Attributes
{
    /// <summary>
    /// Attribute for applying bot to terminal
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class BotAttribute : System.Attribute
    {
        public string Name { get; }

        public BotAttribute(string name)
        {
            Name = name;
        }
    }
}
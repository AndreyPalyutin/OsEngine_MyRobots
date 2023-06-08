using OsEngine.Entity;
using OsEngine.OsTrader.Panels.Tab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OsEngine.Entity;
using OsEngine.OsTrader.Panels;
using OsEngine.OsTrader.Panels.Tab;
using OsEngine.Logging;
using ru.micexrts.cgate.message;

namespace OsEngine.Robots
{
    public class VM : INotifyPropertyChanged
    {
        public VM(MyRobot robot)
        {
            _robot = robot;
        }
        MyRobot _robot;

        public int CountCandles
        {
            get
            { 
                return ((StrategyParameterInt)_robot.Parameters[5]).ValueInt;
            }
           
            set
            {
                ((StrategyParameterInt)_robot.Parameters[5]).ValueInt = value;
               
                OnPropertyChanged(nameof(CountCandles));
            }
        }
       


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class SpeakingClock
    {
        public static string GoodDay(int hour)
        {
            string greeting = "";

            if (hour >= 0 && hour < 12)
            {
                greeting = "Good morning!";
            }
            else if (hour >= 12 && hour < 17)
            {
                greeting = "Good afternoon!";
            }
            else
            {
                greeting = "Good evening!";
            }

            return $"{greeting} It's {hour} o'clock.";
        }
    }
}

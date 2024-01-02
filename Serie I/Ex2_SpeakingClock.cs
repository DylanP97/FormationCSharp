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

            if (hour >= 6 && hour < 12)
            {
                greeting = "Good morning!";
            }
            else if (hour == 12)
            {
                greeting = "Time for lunch!";
            }
            else if (hour >= 13 && hour < 17)
            {
                greeting = "Good afternoon!";
            }
            else if (hour >= 17 && hour < 22)
            {
                greeting = "Good evening!";
            }
            else
            {
                greeting = "Good night...";
            }

            return $"It's {hour} o'clock. {greeting}";
        }
    }
}

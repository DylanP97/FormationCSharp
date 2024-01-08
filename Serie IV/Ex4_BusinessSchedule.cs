using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_IV
{
    public class BusinessSchedule
    {
        private SortedDictionary<DateTime, DateTime> schedule = new SortedDictionary<DateTime, DateTime>();

        public bool IsEmpty()
        {
            return schedule.Count == 0;
        }

        public void SetRangeOfDates(DateTime begin, DateTime end)
        {
            Console.WriteLine("The range of dates from " + begin + " to " + end + " has been selected.");
            // You can set the range by clearing existing meetings outside the specified range
            var meetingsToRemove = schedule.Where(kv => kv.Key < begin || kv.Value > end).ToList();
            foreach (var meeting in meetingsToRemove)
            {
                schedule.Remove(meeting.Key);
            }
        }

        private KeyValuePair<DateTime, DateTime> ClosestElements(DateTime beginMeeting)
        {
            // You can find the closest elements by searching for the closest key to beginMeeting
            DateTime closestStart = DateTime.MinValue;
            DateTime closestEnd = DateTime.MaxValue;

            foreach (var meeting in schedule)
            {
                if (meeting.Key <= beginMeeting && meeting.Value >= beginMeeting)
                {
                    closestStart = meeting.Key;
                    closestEnd = meeting.Value;
                    break;
                }
                else if (meeting.Key > beginMeeting)
                {
                    closestStart = meeting.Key;
                    closestEnd = meeting.Value;
                    break;
                }
            }

            return new KeyValuePair<DateTime, DateTime>(closestStart, closestEnd);
        }

        public bool AddBusinessMeeting(DateTime date, TimeSpan duration)
        {
            DateTime endDateTime = date + duration;
            schedule.Add(date, endDateTime);
            return true;
        }

        public bool DeleteBusinessMeeting(DateTime date, TimeSpan duration)
        {
            DateTime endDateTime = date + duration;

            if (schedule.ContainsKey(date) && schedule[date] == endDateTime)
            {
                schedule.Remove(date);
                return true;
            }

            return false;
        }

        public int ClearMeetingPeriod(DateTime begin, DateTime end)
        {
            var meetingsToRemove = schedule.Where(kv => kv.Key >= begin && kv.Value <= end).ToList();

            foreach (var meeting in meetingsToRemove)
            {
                schedule.Remove(meeting.Key);
            }

            return meetingsToRemove.Count;
        }

        public void DisplayMeetings()
        {
            {
                if (schedule.Count == 0)
                {
                    Console.WriteLine("No meetings scheduled for the selected period.");
                }
                else
                {
                    Console.WriteLine("Business Meetings for the selected period:");
                    foreach (var meeting in schedule)
                    {
                        Console.WriteLine($"Start Time: {meeting.Key}, End Time: {meeting.Value}");
                    }
                }
            }
        }
    }
}

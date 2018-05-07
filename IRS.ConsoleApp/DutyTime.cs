using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.ConsoleApp
{
    public class DutyTime
    {
        private String time;

        private List<User> freeUsers = new List<User>();

        private List<User> dutyUsers = new List<User>();

        public DutyTime() { }

        public DutyTime(string time)
        {
            this.time = time;
        }

        public List<User> GetFreeUsers()
        {
            return freeUsers;
        }

        public List<User> GetDutyUsers()
        {
            return dutyUsers;
        }

        public string GetTime()
        {
            return time;
        }
    }
}

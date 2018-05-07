using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.ConsoleApp
{
    public class User
    {
        private String name;
        private int num;

        public User() { }
        public User(String name,int num)
        {
            this.name = name;
            this.num = num;
        }

        public int GetNum()
        {
            return num;
        }

        public string GetName()
        {
            return name;
        }
    }
}

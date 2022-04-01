using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public class CheckingMonth
    {
        private readonly List<string> months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        public int Year { get; set; }
        public int Month { get; set; }
        public float MonthTarget { get; set; }
        public float MonthChecking { get; set; }
        public string Date
        {
            get
            {
                return Year + " " + months[Month - 1];
            }
        }




    }
}

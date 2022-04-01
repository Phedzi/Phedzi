using Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Checker
{
    public class CheckingTable
    {
        public CheckingMonth Month { get; set; }
        public List<CheckingModel> Checkings { get; set; }
    }
}

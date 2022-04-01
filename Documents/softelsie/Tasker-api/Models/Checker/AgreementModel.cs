using Models.Common;
using System;
using System.Collections.Generic;
/*
Agreement is between two or more individuals/companies
it consist of one or more Tasks
*/
namespace Models.Checker
{
    public class AgreementModel : BaseModel
    {
        public string Name { get; set; }
        public AgreementTypeModel AgreementType { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool NeedsAprooval { get; set; }
        public bool IsReopenable { get; set; }
        public List<CheckingModel> taks { get; set; }
        public int possibleTaskCount { get; set; }
        public UserModel Owner { get; set; }
        public UserModel Assignee { get; set; }
    }
}

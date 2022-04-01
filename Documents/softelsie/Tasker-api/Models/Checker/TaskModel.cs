using Models.Common;
using System;
using System.Collections.Generic;
/*
    on creating , one canstate  how long will this 
 */

namespace Models.Checker
{
   public class TaskModel : BaseModel
    {
        public float AmountDue { get; set; }
        public string AmountDueFriendlyName { get; set; }
        public float AmountPaid { get; set; }
        public string AmountPaidFriendlyName { get; set; }
        public float AmountDeposited { get; set; }
        public string AmountDepositedFriendlyName { get; set; }
        public int Weight { get; set; }
        public string Comment { get; set; }
        public List<TrackActionModel> Actions { get; set; }
        public StatusModel Status { get; set; }
        public ColorModel _Color { get; set; }
        public AgreementModel Agreement { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateFriendlyName { get; set; }
    }
}

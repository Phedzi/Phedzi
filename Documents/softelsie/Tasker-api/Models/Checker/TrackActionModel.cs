using Models.Common;
/*
  Actions can be : Payment, ApprovePayment,RequestLoan,CancelPayment,ReOpenTask...
 */

namespace Models.Checker
{
    public class TrackActionModel : BaseModel
    {
        public string Action { get; set; }
        public float Amount { get; set; }
        public CheckingModel Task { get; set; }
    }
}

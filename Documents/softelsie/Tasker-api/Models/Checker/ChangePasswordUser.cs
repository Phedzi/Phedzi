using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Checker
{
    public class ChangePasswordUser
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}

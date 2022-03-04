using System;
namespace Models.Model
{
    public class ChangePasswordUser
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

using Models.Common;
using System;
using System.Collections.Generic;

namespace Models.Checker
{
   public class CheckingModel : TaskModel
    {
        public float Maintanace { get; set; }

        public CheckingModel Clone()
        {
            var task = (CheckingModel)this.MemberwiseClone();

            return task;
        }
    }
}

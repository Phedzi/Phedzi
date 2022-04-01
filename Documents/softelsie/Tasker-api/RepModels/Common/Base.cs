using System;
using System.Collections.Generic;
using System.Text;

namespace RepModels.Common
{
    public abstract class Base
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}

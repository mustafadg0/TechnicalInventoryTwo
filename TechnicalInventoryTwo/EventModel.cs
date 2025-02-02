using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalInventoryTwo
{
    public class Event
    {
        public int Id { get; set; }
        public string? Priority { get; set; } // "Düşük", "Orta", "Yüksek"
        public DateTime Timestamp { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourWPF
{
    class HarbourSpace
    {
        public int SpaceId { get; set; }
        public List<Boat> ParkedBoats { get; set; }

        public HarbourSpace(int id)
        {
            SpaceId = id;
            ParkedBoats = new List<Boat>();
        }
    }
}

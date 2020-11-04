using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourWPF
{
    class DockSpace
    {
        public int SpaceId { get; set; }
        public List<Boat> ParkedBoats { get; set; }

        public DockSpace(int id)
        {
            SpaceId = id;
            ParkedBoats = new List<Boat>();
        }
    }
}

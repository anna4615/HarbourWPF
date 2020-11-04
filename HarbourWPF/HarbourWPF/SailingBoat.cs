﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HarbourWPF
{
    class SailingBoat : Boat
    {
        public int Length { get; set; }

        public SailingBoat(string id, int weight, int maxSpeed, int daysStaying, int daysSinceArrival, int length)
            : base(weight, maxSpeed, daysStaying, daysSinceArrival)
        {
            Type = "Segelbåt";
            IdNumber = id;
            Length = length;
        }
        public static void CreateSailingBoat(List<Boat> boats)
        {
            string id = "S-" + GenerateID();
            int weight = Utils.random.Next(800, 6000 + 1);
            int maxSpeed = Utils.random.Next(1, 12 + 1);
            int daysStaying = 4;
            int daysSinceArrival = 0;
            int length = Utils.random.Next(10, 60 + 1);

            boats.Add(new SailingBoat(id, weight, maxSpeed, daysStaying, daysSinceArrival, length));
        }

        public override string ToString()
        {
            return base.ToString() + $"\tLängd:\t\t{Math.Round(Utils.ConvertFeetToMeter(Length), 1)} meter";
        }

        public override string TextToFile(int index)
        {
            return base.TextToFile(index) + $"{Length}";
        }

        public static bool ParkSailingBoatInHarbour(Boat boat, DockSpace[] dock1, DockSpace[] dock2)
        {
            bool boatParked;

            while (true)
            {
                int selectedSpace;

                (selectedSpace, boatParked) = FindDoubleSpaceBetweenOccupiedSpaces(dock1);
                if (boatParked)
                {
                    dock1[selectedSpace].ParkedBoats.Add(boat);
                    dock1[selectedSpace + 1].ParkedBoats.Add(boat);
                    break;
                }

                (selectedSpace, boatParked) = FindDoubleSpaceBetweenOccupiedSpaces(dock2);
                if (boatParked)
                {
                    dock2[selectedSpace].ParkedBoats.Add(boat);
                    dock2[selectedSpace + 1].ParkedBoats.Add(boat);
                    break;
                }

                (selectedSpace, boatParked) = FindFirstTwoFreeSpaces(dock1);
                if (boatParked)
                {
                    dock1[selectedSpace].ParkedBoats.Add(boat);
                    dock1[selectedSpace + 1].ParkedBoats.Add(boat);
                    break;
                }

                (selectedSpace, boatParked) = FindFirstTwoFreeSpaces(dock2);
                if (boatParked)
                {
                    dock2[selectedSpace].ParkedBoats.Add(boat);
                    dock2[selectedSpace + 1].ParkedBoats.Add(boat);
                    break;
                }

                break;
            }

            return boatParked;
        }

        private static (int selectedSpace, bool spaceFound) FindDoubleSpaceBetweenOccupiedSpaces(DockSpace[] dock)
        {
            int selectedSpace = 0;
            bool spaceFound = false;

            // Om index 0 och 1 är ledigt och index 2 upptaget
            if (dock[0].ParkedBoats.Count == 0 && dock[1].ParkedBoats.Count == 0
                && dock[2].ParkedBoats.Count > 0)
            {
                selectedSpace = 0;
                spaceFound = true;
            }

            //Annars, hitta två lediga platser intill varandra med upptagna platser runtom
            if (spaceFound == false)
            {
                var q = dock
                    .FirstOrDefault(h => h.ParkedBoats.Count == 0
                    && h.SpaceId > 0
                    && h.SpaceId < dock.Length - 2
                    && dock[h.SpaceId + 1].ParkedBoats.Count == 0
                    && dock[h.SpaceId - 1].ParkedBoats.Count > 0
                    && dock[h.SpaceId + 2].ParkedBoats.Count > 0);

                if (q != null)
                {
                    selectedSpace = q.SpaceId;
                    spaceFound = true;
                }

                // Annars, om två sista index är ledigt och index innan upptaget
                if (spaceFound == false)
                {
                    if (dock[dock.Length - 2].ParkedBoats.Count == 0
                        && dock[dock.Length - 1].ParkedBoats.Count == 0
                        && dock[dock.Length - 3].ParkedBoats.Count > 0)
                    {
                        selectedSpace = dock.Length - 2;
                        spaceFound = true;
                    }
                }
            }

            return (selectedSpace, spaceFound);
        }

        private static (int selectedSpace, bool spaceFound) FindFirstTwoFreeSpaces(DockSpace[] dock)
        {
            int selectedSpace = 0;
            bool spaceFound = false;

            // Hitta första två lediga intill varandra            
            var q = dock
                   .FirstOrDefault(h => h.ParkedBoats.Count == 0
                   && h.SpaceId < dock.Length - 1
                   && dock[h.SpaceId + 1].ParkedBoats.Count == 0);

            if (q != null)
            {
                selectedSpace = q.SpaceId;
                spaceFound = true;
            }

            return (selectedSpace, spaceFound);
        }
    }
}

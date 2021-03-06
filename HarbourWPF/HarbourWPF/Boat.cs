﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HarbourWPF
{
    class Boat
    {
        public string Type { get; set; }
        public string IdNumber { get; set; }
        public int Weight { get; set; }
        public int MaximumSpeed { get; set; }
        public int DaysStaying { get; set; }
        public int DaysSinceArrival { get; set; }

        public Boat(int weight, int maxSpeed, int daysStaying, int daysSinceArrival)
        {
            Weight = weight;
            MaximumSpeed = maxSpeed;
            DaysStaying = daysStaying;
            DaysSinceArrival = daysSinceArrival;
        }

        public override string ToString()
        {
            return $"{Type}  \t{IdNumber}\t{Weight}\t{Math.Round(Utils.ConvertKnotToKmPerHour(MaximumSpeed), 0)}";
        }

        public static string GenerateID()
        {
            string id = "";

            for (int i = 0; i < 3; i++)
            {
                int number = Utils.random.Next(26);
                char c = (char)('A' + number);  // A-Z
                id += c;
            }

            return id;
        }

        public virtual string TextToFile(int spaceId)
        {
            return $"{spaceId};{IdNumber};{Weight};{MaximumSpeed};{Type};{DaysStaying};{DaysSinceArrival};";
        }
    }
}

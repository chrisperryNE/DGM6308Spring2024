using System;
using System.Xml.Linq;

namespace ZooManager
{
    public class Occupant
    {
        public string emoji;
        public string species;

        public Point location;

        public void ReportLocation()
        {
            Console.WriteLine($"I am at {location.x},{location.y}");
        }
        virtual public void OccupantActivate()
        {
            Console.WriteLine($"Occupant {emoji} at {location.x},{location.y} activated");
        }

    }
}

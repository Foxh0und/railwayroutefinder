using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailywayRouteFinder
{
    public class Track
    {
        private Char fDestination;
        private uint fDistance;

        public Track( Char aDestination, uint aDistance )
        {
            fDestination = aDestination;
            fDistance = aDistance;
        }

        public Char Destination
        {
            get
            {
                return fDestination;
            }
        }

        public uint Distance
        {
            get
            {
                return fDistance;
            }
        }
    }
}

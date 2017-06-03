using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailywayRouteFinder
{
    class Route
    {
        private List<Track> fTracks;
        private uint fDistance;
        private Char fOrigin;

        public Route( Char aOrigin )
        {
            fTracks = new List<Track>();
            fDistance = 0;
            fOrigin = aOrigin;
        }

        public Char End
        {
            get
            {
                if( fTracks.Any() )
                    return fTracks.Last().Destination;
                throw ( new Exception( "No Route Constructed" ) );
            }
        }

        public uint Distance
        {
            get
            {
                if ( fTracks.Any() )
                    return fDistance;
                throw ( new Exception( "No Route Constructed" ) );
            }
        }

        public uint Stops
        {
            get
            {
                return (uint) fTracks.Count; 
            }
        }

        public void addTrack( Track aTrack ) //No Protection, can add track to path even if it doesn't exist.
        {
            fTracks.Add( aTrack );
            fDistance += aTrack.Distance;
        }

        public override string ToString()
        {
            String lResult = fOrigin.ToString();

            foreach( Track track in fTracks )
            {
                lResult += " -> " + track.Destination;
            }

            lResult += ". Distance: " + fDistance + ".";
            return lResult;
        }

        public static Route operator +(Route aLeft, Route aRight)
        {
            Route lResult = new Route( aLeft.fOrigin );

            lResult.fTracks.AddRange( aLeft.fTracks );
            lResult.fTracks.AddRange( aRight.fTracks );

            lResult.fDistance = aLeft.fDistance + aRight.fDistance;

            return lResult;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailywayRouteFinder
{
    public class Station
    {
        private Char fLabel;
        private List<Track> fTracks;

        public Station( Char aLabel )
        {
            fLabel = aLabel;
            fTracks = new List<Track>();
        }

        private int getTrackIndex( Char aDestination )
        {
            if ( getTrackExistence( aDestination ) )
                return fTracks.FindIndex( x => x.Destination == aDestination );
            throw ( new Exception( "Track does not exist!" ) );

        }

        public bool getTrackExistence( Char aDestination )
        {
            return fTracks.Any( x => x.Destination == aDestination );
        }

        public void addNewTrack( Char aStation, uint aDistance )
        {
            fTracks.Add( new Track( aStation, aDistance ) );
        }

        public char Label
        {
            get
            {
                return fLabel;
            }
        }

        public Track getTrack( Char aDestination )
        {
            if ( getTrackExistence( aDestination ) )
            {
                foreach ( Track track in fTracks )
                {
                    if ( track.Destination == aDestination )
                        return track;
                }
            }
            throw ( new Exception( "Track does not exist!" ) );
        }

        public String getAllOutboundDestinations()
        {
            String lResult = "";

            foreach( Track track in fTracks )
                lResult += track.Destination;
            return lResult;
        }
    }
}

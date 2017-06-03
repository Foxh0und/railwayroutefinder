using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailywayRouteFinder
{
    public class Railway
    {
        private List<Station> fStations;

        public Railway()
        {
            fStations = new List<Station>();
        }

        private bool getStationExistence( Char aStationLabel ) 
        {
            return fStations.Any( x => x.Label.Equals( aStationLabel ) );
        }

        private int getStationIndex( Char aStationLabel )
        {
            if ( getStationExistence( aStationLabel ) )
                return fStations.FindIndex( x => x.Label == aStationLabel );
            throw ( new Exception( "Station does not exist!" ) );
        }

        public Station getStation( Char aStationLabel )
        {
            if ( getStationExistence( aStationLabel ) )
                return fStations[getStationIndex( aStationLabel )];

            throw ( new Exception( "Station does not exist!" ) );
        }

        public Track getTrack( Char aOrigin, Char aDestination )
        {
            if ( getStationExistence( aOrigin ) || getStationExistence( aDestination ) )
            {
                if( fStations[getStationIndex( aOrigin )].getTrackExistence( aDestination )  )
                   return fStations[getStationIndex( aOrigin )].getTrack( aDestination );
                throw ( new Exception( "No track exists exist!" ) );
            }

            throw ( new Exception( "Stations do not exist!" ) );
        }

        public bool addNewStation( Char aStationLabel )
        {
            if ( getStationExistence( aStationLabel ) ) // Only if the station does not exist can we add it.
                return false;

            fStations.Add( new Station( aStationLabel ) );
            return true;
        }

        public bool addNewTrack( Char aOrigin, Char aDestination, uint aDistance )
        {
            if ( getStationExistence( aOrigin ) && getStationExistence( aDestination ) )
            {
                fStations[getStationIndex( aOrigin )].addNewTrack( aDestination, aDistance );
                return true;
            }

            throw ( new Exception( "Stations do not exist!" ) );
        }
    }
}

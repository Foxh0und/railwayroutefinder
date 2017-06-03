using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailywayRouteFinder
{
    public class Conductor
    {
        private Railway fRailway;
        private BluePrint fBluePrint;

        public Conductor( BluePrint aBluePrint )
        {
            fRailway = new Railway();
            fBluePrint = aBluePrint;
            constructRailway();
        }

        public void askQuestions()
        {
            foreach( KeyValuePair<String,QuestionType> question in fBluePrint.Questions )
            {
                switch(question.Value)
                {
                    case QuestionType.DirectPath:
                        {
                            askforPlannedRoute( question.Key );
                            break;
                        }
                    case QuestionType.MaximumStops:
                        {
                            askForPathsWithMaximumStops( question.Key );
                            break;
                        }
                    case QuestionType.ExactStops:
                        {
                            askForPathsWithExactStops( question.Key );
                            break;
                        }
                    case QuestionType.ShortestPath:
                        {
                            askForShortestRoute( question.Key );
                            break;
                        }
                    case QuestionType.MaximumDistance:
                        {
                            askForRoutesWithMaximumDistance( question.Key );
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void constructRailway()
        {
            foreach( String track in fBluePrint.Specifications )
            {
                //Add the station if they do not exist(Checking if they exist first).
                fRailway.addNewStation( track[0] );
                fRailway.addNewStation( track[1] );

                uint lTrackDistance = Convert.ToUInt16( track.Substring(2) );

                fRailway.addNewTrack( track[0], track[1], lTrackDistance );
            }
        }

        private void askforPlannedRoute( string aQuestion )
        {
            Route lResult = getPlannedRoute( aQuestion );
            String lPlan = String.Join<char>( "-", aQuestion );

            Console.WriteLine( "The distance of the route {0}?", lPlan );

            if ( lResult != null )
                Console.WriteLine( lResult.ToString() );
            else
                Console.WriteLine( "Route does not exist!" );

            Console.WriteLine( "\n" ); Console.WriteLine( "\n" );
        }

        private void askForShortestRoute( string aQuestion )
        {
            Char lOrigin = aQuestion[0];
            Char lDestination = aQuestion[1];

            Console.WriteLine( "The length of the shortest route (in terms of distance to travel) from {0} to {1}?",
                                lOrigin, lDestination );

            Route lResult = findShortestRoute( lOrigin, lDestination );

            if ( lResult != null )
                Console.WriteLine( lResult.ToString() );
            else
                Console.WriteLine( "Route does not exist!" );

            Console.WriteLine( "\n" );
        }

        private void askForPathsWithMaximumStops( string aQuestion )
        {
            Char lOrigin = aQuestion[0];
            Char lDestination = aQuestion[1];
            uint lMaxStops = Convert.ToUInt16( aQuestion.Substring( 2 ) );

            Console.WriteLine( "The number of trips starting at {0} and ending at {1} with exactly {2} stops",
                                lOrigin, lDestination, lDestination );

            List<Route> lResult = findPathsWithMaximumStops( lOrigin, lDestination, lMaxStops );

            if( lResult.Any() )
            {
                Console.WriteLine( "There were {0} possible routes found.", lResult.Count );
                foreach ( Route route in lResult )
                    Console.WriteLine( "{0} . Number of Stops: {1}", route.ToString(), route.Stops );
            }
            else
                Console.WriteLine( "No Possible Routes" );

            Console.WriteLine( "\n" );
        }

        private void askForPathsWithExactStops( string aQuestion )
        {
            Char lOrigin = aQuestion[0];
            Char lDestination = aQuestion[1];
            uint lExactStops = Convert.ToUInt16( aQuestion.Substring( 2 ) );

            Console.WriteLine( "The number of trips starting at {0} and ending at {1} with exactly {2} stops",
                                lOrigin, lDestination, lExactStops );

            List<Route> lResult = findPathsWithExactStops( lOrigin, lDestination, lExactStops );

            if ( lResult.Any() )
            {
                Console.WriteLine( "There were {0} possible routes found.", lResult.Count );
                foreach ( Route route in lResult )
                    Console.WriteLine( "{0} . Number of Stops: {1}", route.ToString(), route.Stops );
            }
            else
                Console.WriteLine( "No Possible Routes" );

            Console.WriteLine( "\n" );
        }

        private void askForRoutesWithMaximumDistance( string aQuestion)
        {
            Char lOrigin = aQuestion[0];
            Char lDestination = aQuestion[1];
            uint lMaxDistance = Convert.ToUInt16( aQuestion.Substring( 2 ) );

            Console.WriteLine( "The number of different routes from {0} to {1} with a distance of less than {2}",
                    lOrigin, lDestination, lMaxDistance );
            List<Route> lResult = findPathsWithMaximumDistance( lOrigin, lDestination, lMaxDistance );

            if ( lResult.Any() )
            {
                Console.WriteLine( "There were {0} possible routes found.", lResult.Count );
                foreach ( Route route in lResult )
                    Console.WriteLine( route.ToString() );
            }
            else
                Console.WriteLine( "No Possible Routes" );
        }

        private Route getDirectRoute( Char aOrigin, Char aDestination ) // make private
        {
            Route lResult = new Route( aOrigin );
            try
            {
                lResult.addTrack( fRailway.getTrack( aOrigin, aDestination ) );
                return lResult;
            }
            catch
            {
                return null;
            }
        }

        private Route getPlannedRoute( string aRouteList ) //might make private, depends if shit's calling it outside of here, might just run in here.
        {
            if ( aRouteList.Length < 2 )
                throw ( new Exception( "At least two stations must be specified!" ) );

            Route lResult = new Route( aRouteList[0] );
            for ( int i = 1; i < aRouteList.Length; i++ )
            {
                Route lTemp = getDirectRoute( aRouteList[i - 1], aRouteList[i] );

                if ( lTemp != null )
                    lResult += lTemp;
                else
                    return null;
            }
            return lResult;
        }

        private Route findShortestRoute( Char aOrigin, Char aDestination ) //maybe private idk
        {
            List<Route> lOpenList = new List<Route>();

            Station lCurrent = fRailway.getStation( aOrigin );
            String lCurrentOutboundDestinations = lCurrent.getAllOutboundDestinations();

            foreach ( Char station in lCurrentOutboundDestinations )
                lOpenList.Add( getDirectRoute( aOrigin, station ) );

            while ( lOpenList.Any() )
            {
                lOpenList = lOpenList.OrderBy( x => x.Distance ).ToList();

                lCurrent = fRailway.getStation( ( lOpenList.First().End ) );

                if ( lCurrent.Label == aDestination )
                    return lOpenList.First();

                lCurrentOutboundDestinations = lCurrent.getAllOutboundDestinations();

                foreach ( Char station in lCurrentOutboundDestinations )
                {
                    lOpenList.Add( lOpenList.First() + getDirectRoute( lCurrent.Label, station ) );
                }

                lOpenList.RemoveAt( 0 );
            }
            return null;
        }
        private List<Route> findPathsWithMaximumStops( Char aOrigin, Char aDestination, uint aMaxium )
        {
            List<Route> lOpenList = new List<Route>();
            List<Route> lResults = new List<Route>();

            Station lCurrent = fRailway.getStation( aOrigin );
            String lCurrentOutboundDestinations = lCurrent.getAllOutboundDestinations();

            foreach ( Char station in lCurrentOutboundDestinations )
                lOpenList.Add( getDirectRoute( aOrigin, station ) );

            while ( lOpenList.Any() )
            {
                lOpenList = lOpenList.OrderBy( x => x.Stops ).ToList(); //might be order by desc

                if ( lOpenList.First().Stops > aMaxium )
                    break;

                lCurrent = fRailway.getStation( ( lOpenList.First().End ) );

                if ( lCurrent.Label == aDestination )
                    lResults.Add( lOpenList.First() );

                lCurrentOutboundDestinations = lCurrent.getAllOutboundDestinations();

                foreach ( Char station in lCurrentOutboundDestinations )
                {
                    lOpenList.Add( lOpenList.First() + getDirectRoute( lCurrent.Label, station ) );
                }

                lOpenList.RemoveAt( 0 );
            }

            return lResults;
        }

        private List<Route> findPathsWithExactStops( Char aOrigin, Char aDestination, uint aStopCount )
        {
            List<Route> lOpenList = new List<Route>();
            List<Route> lResults = new List<Route>();

            Station lCurrent = fRailway.getStation( aOrigin );
            String lCurrentOutboundDestinations = lCurrent.getAllOutboundDestinations();

            foreach ( Char station in lCurrentOutboundDestinations )
                lOpenList.Add( getDirectRoute( aOrigin, station ) );

            while ( lOpenList.Any() )
            {
                lOpenList = lOpenList.OrderBy( x => x.Stops ).ToList(); //might be order by desc

                if ( lOpenList.First().Stops > aStopCount )
                    break;

                lCurrent = fRailway.getStation( ( lOpenList.First().End ) );

                if ( lCurrent.Label == aDestination && lOpenList.First().Stops == aStopCount )
                    lResults.Add( lOpenList.First() );

                lCurrentOutboundDestinations = lCurrent.getAllOutboundDestinations();

                foreach ( Char station in lCurrentOutboundDestinations )
                {
                    lOpenList.Add( lOpenList.First() + getDirectRoute( lCurrent.Label, station ) );
                }

                lOpenList.RemoveAt( 0 );
            }

            return lResults;
        }

        private List<Route> findPathsWithMaximumDistance( Char aOrigin, Char aDestination, uint aMaximumDistance )
        {
            List<Route> lOpenList = new List<Route>();
            List<Route> lResults = new List<Route>();

            Station lCurrent = fRailway.getStation( aOrigin );
            String lCurrentOutboundDestinations = lCurrent.getAllOutboundDestinations();

            foreach ( Char station in lCurrentOutboundDestinations )
                lOpenList.Add( getDirectRoute( aOrigin, station ) );

            while ( lOpenList.Any() )
            {
                lOpenList = lOpenList.OrderBy( x => x.Distance ).ToList();

                if ( lOpenList.First().Distance >= aMaximumDistance )
                    break;

                lCurrent = fRailway.getStation( ( lOpenList.First().End ) );

                if ( lCurrent.Label == aDestination )
                    lResults.Add( lOpenList.First() );

                lCurrentOutboundDestinations = lCurrent.getAllOutboundDestinations();

                foreach ( Char station in lCurrentOutboundDestinations )
                {
                    lOpenList.Add( lOpenList.First() + getDirectRoute( lCurrent.Label, station ) );
                }

                lOpenList.RemoveAt( 0 );
            }

            return lResults;
        }
    }
}


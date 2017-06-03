using System;
using RailywayRouteFinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RailwayRouteUnitTest
{
    [TestClass]
    public class RailwayTests
    {
        [TestMethod]
        public void addNewStation()
        {
            Railway lTestRailway = new Railway();

            Assert.IsTrue( lTestRailway.addNewStation( 'A' ) );
            Assert.IsFalse( lTestRailway.addNewStation( 'A' ) ); //Cannot add station that already exists.
        }

        [TestMethod]
        public void getStation()
        {
            Railway lTestRailway = new Railway();

            lTestRailway.addNewStation( 'A' );

            Station lTestStation = lTestRailway.getStation( 'A' );
        }

        [TestMethod]
        [ExpectedException( typeof( Exception ) )]
        public void getStationException()
        {
            Railway lTestRailway = new Railway();
            Station lTestStation = lTestRailway.getStation( 'A' ); //station doesn't exist
        }

        [TestMethod]
        public void getTrack()
        {
            Railway lTestRailway = new Railway();

            lTestRailway.addNewStation( 'A' );
            lTestRailway.addNewStation( 'B' );

            lTestRailway.addNewTrack( 'A', 'B', 5 );

            Track lTestTrack = lTestRailway.getTrack( 'A', 'B' );
        }

        [TestMethod]
        [ExpectedException( typeof( Exception ) )]
        public void getTrackOriginException()
        {
            Railway lTestRailway = new Railway();

            lTestRailway.addNewStation( 'B' );

            Track lTestTrack = lTestRailway.getTrack( 'A', 'B' );

        }
        [TestMethod]
        [ExpectedException( typeof( Exception ) )]
        public void getTrackDestinationException()
        {
            Railway lTestRailway = new Railway();

            lTestRailway.addNewStation( 'A' );

            Track lTestTrack = lTestRailway.getTrack( 'A', 'B' );

        }
        [TestMethod]
        [ExpectedException( typeof( Exception ) )]
        public void getTrackDoesNotExistException()
        {
            Railway lTestRailway = new Railway();

            lTestRailway.addNewStation( 'A' );
            lTestRailway.addNewStation( 'B' );

            Track lTestTrack = lTestRailway.getTrack( 'A', 'B' );

        }

        [TestMethod]
        public void addTrack()
        {
            Railway lTestRailway = new Railway();
            lTestRailway.addNewStation( 'A' );
            lTestRailway.addNewStation( 'B' );

            Assert.IsTrue( lTestRailway.addNewTrack( 'A', 'B', 5 ) );
        }

        [TestMethod]
        [ExpectedException( typeof( Exception ) )]
        public void addTrackOriginException()
        {
            Railway lTestRailway = new Railway();
            lTestRailway.addNewStation( 'B' );

            Assert.IsTrue( lTestRailway.addNewTrack( 'A', 'B', 5 ) );
        }

        [TestMethod]
        [ExpectedException( typeof( Exception ) )]
        public void addTrackDestinationException()
        {
            Railway lTestRailway = new Railway();
            lTestRailway.addNewStation( 'A' );

            Assert.IsTrue( lTestRailway.addNewTrack( 'A', 'B', 5 ) );
        }

    }
}

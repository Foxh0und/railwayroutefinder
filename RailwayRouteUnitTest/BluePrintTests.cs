using System;
using RailywayRouteFinder;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RailwayRouteUnitTest
{
    [TestClass]
    public class BluePrintTests
    {
        [TestMethod]
        public void Specification()
        {
            BluePrint lTestBluePrint = new BluePrint( "testinput.txt" );
            Assert.AreEqual( 6, lTestBluePrint.Specifications.Count );

            List<String> lExpectedSpecs = new List<String>();

            lExpectedSpecs.Add( "AB5" );
            lExpectedSpecs.Add( "BC3" );
            lExpectedSpecs.Add( "BA2" );
            lExpectedSpecs.Add( "AC2" );
            lExpectedSpecs.Add( "CB2" );
            lExpectedSpecs.Add( "CA3" );

            for(int i = 0; i < 6; i++ )
                Assert.AreEqual( lExpectedSpecs[i], lTestBluePrint.Specifications[i] );

        }

        [TestMethod]
        public void Questions()
        {
            BluePrint lTestBluePrint = new BluePrint( "testinput.txt" );
            Assert.AreEqual( 5, lTestBluePrint.Questions.Count );

            List<String> lExpectedQuestions = new List<string>();
            List<QuestionType> lExpectedQuestionTypes = new List<QuestionType>();

            lExpectedQuestions.Add( "ABC" );
            lExpectedQuestions.Add( "AC" );
            lExpectedQuestions.Add( "CC3" );
            lExpectedQuestions.Add( "AC4" );
            lExpectedQuestions.Add( "CC30" );

            lExpectedQuestionTypes.Add( QuestionType.DirectPath );
            lExpectedQuestionTypes.Add( QuestionType.ShortestPath );
            lExpectedQuestionTypes.Add( QuestionType.MaximumStops );
            lExpectedQuestionTypes.Add( QuestionType.ExactStops );
            lExpectedQuestionTypes.Add( QuestionType.MaximumDistance );

            List<String> lActualQuestions = new List<String>();
            List<QuestionType> lActualQuestionTypes = new List<QuestionType>();

            foreach( KeyValuePair<String,QuestionType> actualQuestion in lTestBluePrint.Questions )
            {
                lActualQuestions.Add( actualQuestion.Key );
                lActualQuestionTypes.Add( actualQuestion.Value );
            }
            for ( int i = 0; i < 5; i++ )
            {
                Assert.AreEqual( lExpectedQuestions[i], lActualQuestions[i] );
                Assert.AreEqual( lExpectedQuestionTypes[i], lActualQuestionTypes[i] );
            }
        }
    }

}/* INPUT FILE 
    Graph: AB5, BC3, AB2, AC2, CB2, CA3;
    Not a Graph: DE27, C45,AB2, ED2, ZY2, CA3;
    1. The distance of the route A-B-C.
    2. This question is rubbish.
    3. The distance of the route A-B but doesn't end in period so should be ignored
    8. The length of the shortest route (in terms of distance to travel) from A to C.
    6. The number of trips starting at C and ending at C with a maximum of 3 stops.  In the sample data below, there are two such trips: C-D-C (2 stops). and C-E-B-C (3 stops).
    7. The number of trips starting at A and ending at C with exactly 4 stops.  
    9. The number of different routes from C to C with a distance of less than 30. 
*/

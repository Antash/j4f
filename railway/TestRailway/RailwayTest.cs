using railway;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestRailway
{
    
    
    /// <summary>
    ///This is a test class for RailwayTest and is intended
    ///to contain all RailwayTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RailwayTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for IfCrashExists
        ///</summary>
        [TestMethod()]
        public void IfCrashExistsTest()
        {
            bool actual, expected;
            var m = new Mesh();
            m.Add(new Line(1, 5, 2));
            m.Add(new Line(5, 7, 5));
            m.Add(new Line(7, 4, 6));
            m.Add(new Line(4, 3, 1));
            m.Add(new Line(4, 6, 8));
            m.Add(new Line(7, 6, 3));
            m.Add(new Line(1, 2, 3));
            m.Add(new Line(3, 2, 3));
            m.Add(new Line(1, 3, 6));
            m.Add(new Line(5, 6, 2));

            Railway target = new Railway(m);
            target.ClearPaths();
            // Scenario 1. Crash happens at the station
            target.AddPath(new uint[] { 1, 5, 7, 4, 3 });
            target.AddPath(new uint[] { 3, 4, 7, 6 });
            target.AddPath(new uint[] { 4, 6, 5, 1, 3 });
            target.AddPath(new uint[] { 2, 1, 3, 4, 7 });

            expected = true;
            actual = target.IfCrashExists();
            Assert.AreEqual(expected, actual);

            target.ClearPaths();
            // Scenario 2. Crash happens at the line
            target.AddPath(new uint[] { 3, 4, 7, 6 });
            target.AddPath(new uint[] { 4, 6, 7, 5 });
            target.AddPath(new uint[] { 2, 1, 3, 4, 7 });

            actual = target.IfCrashExists();
            Assert.AreEqual(expected, actual);

            target.ClearPaths();
            // Scenario 3.1 There are no crashes
            target.AddPath(new uint[] { 3, 4, 7, 6 });
            target.AddPath(new uint[] { 5, 6, 5, 1, 3 });
            target.AddPath(new uint[] { 2, 1, 3, 4, 7 });

            expected = false;
            actual = target.IfCrashExists();
            Assert.AreEqual(expected, actual);

            target.ClearPaths();
            // Scenario 3.2 There are no crashes (trans passing same line together)
            target.AddPath(new uint[] { 7, 6, 4, 3 });
            target.AddPath(new uint[] { 5, 6, 4, 7 });

            actual = target.IfCrashExists();
            Assert.AreEqual(expected, actual);
        }
    }
}

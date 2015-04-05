using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}
        [TestMethod()]
        public void TestThatCarGetsCorrectLocationFromDatabase()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            String carLocation = "Rose Hulman";
            String anotherCarLocation = "ISU";
            Expect.Call(mockDatabase.getCarLocation(3)).Return(carLocation);
            Expect.Call(mockDatabase.getCarLocation(6)).Return(anotherCarLocation);
            mocks.ReplayAll();
            Car target = new Car(15);
            target.Database = mockDatabase;
            string result;
            result = target.getCarLocation(3);
            Assert.AreEqual(carLocation, result);
            result = target.getCarLocation(6);
            Assert.AreEqual(anotherCarLocation, result);
            mocks.VerifyAll();
        }
        [TestMethod()]
        public void TestThatCarGetsMileageProperly()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            Int32 Miles = 150;
            Expect.Call(mockDatabase.Miles).PropertyBehavior();
            mocks.ReplayAll();
            mockDatabase.Miles = Miles;
            var target = new Car(15);
            target.Database = mockDatabase;
            int resultMiles = target.Mileage;
            Assert.AreEqual(resultMiles, Miles);
            mocks.VerifyAll();
        }
        [TestMethod()]
        public void TestObjectMotherBMW()
        {
            Car BMW = ObjectMother.BMW();
            Assert.AreEqual(100*0.8, BMW.getBasePrice());
            Assert.AreEqual("BMW i3", BMW.Name);
        }
	}
}

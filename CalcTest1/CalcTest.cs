﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcTest1
{
    [TestClass]
    public class CalcTest
    {
        //Double.NaN
        //4 функции добавить
        [TestMethod]
        public void TestSum()
        {
            //Arrnge
            var test = new CalcLibrary.Calc();

            //Act
            var result = test.Sum(1, 2);
            var result1 = test.Sum(1, 0);
            var result2 = test.Sum(0, 2);
            var result3 = test.Sum(0, 0);

            //Assert
            Assert.AreEqual(result, 3);
            Assert.AreEqual(result1, 1);
            Assert.AreEqual(result2, 2);
            Assert.AreEqual(result3, 0);
        }
    }
}

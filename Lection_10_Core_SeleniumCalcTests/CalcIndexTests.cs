using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Lection_10_Core_SeleniumCalcTests
{
    public class Tests
    {
        private const string LeftNumberId = "leftNumber";
        private const string RightNumberId = "rightNumber";
        private const string ResultId = "result";
        private const string OperandId = "operand";
        private const string CalcButtonId = "calculate";
        private const string TargetHtml = "file:///C:/Users/Sviatoslav_Samoilenk/Desktop/FrontFolder/Index.html";
        private IWebDriver _webDriver;
        private Random _random;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _webDriver = new ChromeDriver();
            _random = new Random();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _webDriver.Dispose();
        }

        [TestCaseSource(typeof(MathDataClass))]
        public void Calculate_WhenBNotZero_ShouldCalcEquation(
            string operandSelectorValue,
            Func<int,int, double> predicate,
            double delta)
        {
            var a = _random.Next(-100, 100);
            var b = _random.Next(1, 100);
            _webDriver.Navigate().GoToUrl(TargetHtml);
            var leftNumber = _webDriver.FindElement(By.Id(LeftNumberId));
            var rightNumber = _webDriver.FindElement(By.Id(RightNumberId));
            var operand = _webDriver.FindElement(By.Id(OperandId));
            var result = _webDriver.FindElement(By.Id(ResultId));
            SelectElement selectElement = new SelectElement(operand);
            var calcButton = _webDriver.FindElement(By.Id(CalcButtonId));

            leftNumber.SendKeys(a.ToString());
            rightNumber.SendKeys(b.ToString());
            selectElement.SelectByText(operandSelectorValue);
            calcButton.Click();

            Assert.AreEqual(Convert.ToDouble(predicate(a, b)),
                Convert.ToDouble(result.GetAttribute("value")), delta);
        }

        [Test]
        public void CalculateDivide_WhenBZero_ShouldCannotDivideByZero()
        {
            var a = _random.Next(-100, 100);
            var b = 0;
            _webDriver.Navigate().GoToUrl(TargetHtml);
            var leftNumber = _webDriver.FindElement(By.Id(LeftNumberId));
            var rightNumber = _webDriver.FindElement(By.Id(RightNumberId));
            var operand = _webDriver.FindElement(By.Id(OperandId));
            var result = _webDriver.FindElement(By.Id(ResultId));
            SelectElement selectElement = new SelectElement(operand);
            var calcButton = _webDriver.FindElement(By.Id(CalcButtonId));

            leftNumber.SendKeys(a.ToString());
            rightNumber.SendKeys(b.ToString());
            selectElement.SelectByText("/");
            calcButton.Click();

            Assert.AreEqual("Can not divide by zero!",
                result.GetAttribute("value"));
        }
    }
}
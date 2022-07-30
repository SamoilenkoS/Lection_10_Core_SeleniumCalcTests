using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection_10_Core_SeleniumCalcTests
{
    class MathDataClass : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { "+", new Func<int,int, double>(Plus), 0};
            yield return new object[] { "-", new Func<int, int, double>(Minus), 0 };
            yield return new object[] { "*", new Func<int, int, double>(Multiply), 0 };
            yield return new object[] { "/", new Func<int, int, double>(Divide), 0.01 };
        }

        private double Plus(int a, int b) => a + b;
        private double Minus(int a, int b) => a - b;
        private double Multiply(int a, int b) => a * b;
        private double Divide(int a, int b) => (double)a / b;
    }
}

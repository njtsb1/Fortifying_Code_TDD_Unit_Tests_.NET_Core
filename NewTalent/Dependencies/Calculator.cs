using System;
using System.Collections.Generic;

namespace NewTalent.Dependencies
{
    public class Calculator
    {
        private List<string> _history;

        public Calculator()
        {
            _history = new List<string>();
        }

        public int Add(int num1, int num2)
        {
            int result = num1 + num2;
            SaveToHistory($"{num1} + {num2} = {result}");
            return result;
        }

        public int Subtract(int num1, int num2)
        {
            int result = num1 - num2;
            SaveToHistory($"{num1} - {num2} = {result}");
            return result;
        }

        public int Multiply(int num1, int num2)
        {
            int result = num1 * num2;
            SaveToHistory($"{num1} * {num2} = {result}");
            return result;
        }

        public int Divide(int num1, int num2)
        {
            if (num2 == 0)
            {
                throw new DivideByZeroException("Division by zero is not allowed.");
            }

            int result = num1 / num2;
            SaveToHistory($"{num1} / {num2} = {result}");
            return result;
        }

        public List<string> GetHistory()
        {
            // Always returns the last 3 elements from the history list
            if (_history.Count > 3)
            {
                return _history.GetRange(_history.Count - 3, 3);
            }
            return new List<string>(_history);
        }

        private void SaveToHistory(string operation)
        {
            _history.Add(operation);
        }
    }
}

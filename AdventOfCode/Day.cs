using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public abstract class Day
    {
        private readonly int _dayNumber;
        private readonly string _textInput;

        public Day()
        {
            var lengthOfAbstractClassName = this.GetType().BaseType.Name.Length;
            var numberAtTheEndOfTheClassName = this.GetType().Name.Remove(startIndex: 0, count: lengthOfAbstractClassName);
            _dayNumber = int.Parse(numberAtTheEndOfTheClassName);
            _textInput = UrlCaller.GetTextInput(_dayNumber).TrimEnd('\n');
        }

        private List<object> _answerParts = new List<object>();
        public void GiveAnswer()
        {
            CalculAnswers(_textInput);
            for (int i = 0; i < _answerParts.Count; i++)
                Console.WriteLine($"Day {_dayNumber} part {i + 1} answer is : {_answerParts[i].ToString()}");
        }

        public abstract void CalculAnswers(string textInput);

        protected void AnswerIs(object answer)
        {
            _answerParts.Add(answer);
        }
    }
}

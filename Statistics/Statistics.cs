using System;
using System.Collections.Generic;

namespace Statistics
{
    public class StatsComputer
    {
        public Stats CalculateStatistics(List<double> numbers) {

            Double max = Double.NaN, min = Double.NaN, average = Double.NaN;
            if (numbers.Count!=0)
            {
                max = numbers[0]; min = numbers[0]; average = 0;
            }
            
            foreach (var item in numbers)
            {
                max = item >= max ? item : max;
                min = item <= min ? item : min;
                average += item;
            }
            average = average / numbers.Count;
            return new Stats
                {
                average = average,
                max = max,
                min = min
            };
        }
    }

    public class Stats
    {
        public Double average;
        public Double max;
        public Double min;
    }
    public class StatsAlerter
    {
        private float maxThreshold;
        private IAlerter[] alerters;
        private readonly StatsComputer statsComputer = new StatsComputer();
        public StatsAlerter(float maxThreshold, IAlerter[] alerters)
        {
            this.maxThreshold = maxThreshold;
            this.alerters = alerters;
        }

        public void checkAndAlert(List<double> values)
        {
            Stats stats = statsComputer.CalculateStatistics(values);
            if (stats.max > maxThreshold)
            {
                foreach (var item in alerters)
                {
                    item.Alert();
                }
            }
        }
    }

    public interface IAlerter
    {
        void Alert();
    }

    public class LEDAlert : IAlerter
    {
        public bool ledGlows;
        public LEDAlert()
        {
        }

        public void Alert()
        {
            this.ledGlows = true;
        }
    }

    public class EmailAlert : IAlerter
    {
        public bool emailSent;
        public EmailAlert()
        {
        }

        public void Alert()
        {
            this.emailSent = true;
        }
    }
}

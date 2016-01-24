namespace SmileDetectionControl
{
    using System.Collections.Generic;
    using System.Linq;

    public class DetectionStatistics
    {

        public List<double> noSmileStates = new List<double>();

        public List<double> smileStates = new List<double>();

        public double AverageNoSmile;

        public double AverageSmile;

        public double tresholdPercent = 50;

        /// <summary>
        /// Gets the treshold.
        /// </summary>
        public double Treshold
        {
            get
            {
                var difference = this.AverageSmile - this.AverageNoSmile;
                var result = this.AverageSmile - ((difference * this.tresholdPercent) / 100);
                return result;
            }
        }

        /// <summary>
        /// The save no smile statistics.
        /// </summary>
        public void SaveNoSmileStatistics()
        {
            //TODO czy jest pusta
            this.AverageNoSmile = this.noSmileStates.Average();
            this.AverageNoSmile *= 0.9;
            this.noSmileStates = new List<double>();
        }

        /// <summary>
        /// The save smile statistics.
        /// </summary>
        public void SaveSmileStatistics()
        {
            this.AverageSmile = this.smileStates.Average();
            this.AverageSmile *= 1.1;
            this.smileStates = new List<double>();
        }
    }
}

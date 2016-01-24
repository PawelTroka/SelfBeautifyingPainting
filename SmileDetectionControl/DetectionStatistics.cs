namespace SmileDetectionControl
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The detection statistics.
    /// </summary>
    public class DetectionStatistics
    {
        /// <summary>
        /// The treshold percent lock.
        /// </summary>
        private readonly object thresholdPercentLock = new object();

        /// <summary>
        /// The smile data lock.
        /// </summary>
        private readonly object smileDataLock = new object();

        /// <summary>
        /// The no smile data lock.
        /// </summary>
        private readonly object noSmileDataLock = new object();

        /// <summary>
        /// The treshold percent.
        /// </summary>
        private int tresholdPercent = 50;

        /// <summary>
        /// The average no smile.
        /// </summary>
        private double averageNoSmile;

        /// <summary>
        /// The average smile.
        /// </summary>
        private double averageSmile;

        /// <summary>
        /// The no smile states.
        /// </summary>
        private List<double> noSmileStates;

        /// <summary>
        /// The smile states.
        /// </summary>
        private List<double> smileStates;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetectionStatistics"/> class.
        /// </summary>
        public DetectionStatistics()
        {
            this.noSmileStates = new List<double>();
            this.smileStates = new List<double>();
        }

        /// <summary>
        /// Gets or sets the treshold percent.
        /// </summary>
        public int ThresholdPercent
        {
            get
            {
                lock (this.thresholdPercentLock)
                {
                    return this.tresholdPercent;
                }
            }

            set
            {
                lock (this.thresholdPercentLock)
                {
                    this.tresholdPercent = value;
                }
            }
        }

        /// <summary>
        /// Gets the treshold.
        /// </summary>
        public double Treshold
        {
            get
            {
                var difference = this.averageSmile - this.averageNoSmile;
                var result = this.averageSmile - ((difference * this.ThresholdPercent) / 100);
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the smile states.
        /// </summary>
        public List<double> SmileStates
        {
            get
            {
                lock (this.smileDataLock)
                {
                    return this.smileStates;
                }
            }

            set
            {
                lock (this.smileDataLock)
                {
                    this.smileStates = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the no smile states.
        /// </summary>
        public List<double> NoSmileStates
        {
            get
            {
                lock (this.noSmileDataLock)
                {
                    return this.noSmileStates;
                }
            }

            set
            {
                lock (this.noSmileDataLock)
                {
                    this.noSmileStates = value;
                }
            }
        }

        /// <summary>
        /// The save no smile statistics.
        /// </summary>
        public void SaveNoSmileStatistics()
        {
            this.averageNoSmile = this.noSmileStates.Average();
            this.averageNoSmile *= 0.9;
            this.noSmileStates = new List<double>();
        }

        /// <summary>
        /// The save smile statistics.
        /// </summary>
        public void SaveSmileStatistics()
        {
            this.averageSmile = this.smileStates.Average();
            this.averageSmile *= 1.1;
            this.smileStates = new List<double>();
        }
    }
}

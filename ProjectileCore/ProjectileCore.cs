using System;

namespace Projectile.Core
{
    public static partial class ProjectileCore
    {
        private const double G = 9.8;

        /// <summary>
        /// Calculates the vertical distance travelled by the projectile from the origin at a specified time.
        /// </summary>
        /// <param name="Vo">Initial velocity of the object, measured in metres per seconds (m/s).</param>
        /// <param name="θo">Initial angle of the projectile, measured in radians (rad).</param>
        /// <param name="time">Immediate time to be used for calculation, measured in seconds (s).</param>
        /// <returns></returns>
        public static double CalculateY(double Vo, double θo, double time)
        {
            return (Vo * Math.Sin(θo) * time) - (0.5 * G * Math.Pow(time, 2));
        }

        /// <summary>
        /// Calculates the horizontal distance travelled by the projectile from the origin at a specified time.
        /// </summary>
        /// <param name="Vo">Initial velocity of the object, measured in metre per second (m/s).</param>
        /// <param name="θo">Initial angle of the projectile, measured in radians (rad).</param>
        /// <param name="time">Immediate time to be used for calculation, measured in seconds (s).</param>
        /// <returns></returns>
        public static double CalculateX(double Vo, double θo, double time)
        {
            // Since Math.Cos(Convert.ToRadians(θo)) gives a wrong answer [when θo in degrees = 90, 270, 450, ...] due to float errors...
            if (θo % ProjectileCore.Convert.ToRadians(180) == Convert.ToRadians(90))
                return Vo * 0 * time;
            else
                return Vo * Math.Cos(θo) * time;
        }

        /// <summary>
        /// Calculates the total time to be taken by a projectile to start from origin and land again at X = 0.
        /// </summary>
        /// <param name="Vo">Initial velocity of the object, measured in metre per second (m/s).</param>
        /// <param name="θo">Initial angle of the projectile, measured in radians (rad).</param>
        /// <returns>Total time of flight in seconds.</returns>
        public static double TimeOfFlight(double Vo, double θo)
        {
            return (2 * Vo * Math.Sin(θo)) / G;
        }

        /// <summary>
        /// Calculates all the positions of the projectile within a time of flight. Outputs arrays of the X coordinates and 
        /// the Y coordinate.
        /// </summary>
        /// <param name="Vo">Initial velocity of the projectile, measured in metre per second (m/s).</param>
        /// <param name="θo">Initial angle of the projectile, measured in radians (rad).</param>
        /// <param name="timeOfFlight">Total duration of flight, measured in seconds (s).</param>
        /// <param name="interval">Interval between each calculation.</param>    
        /// <param name="XPath">An array of the X coordinates during the time of flight with the given interval. This should be left unassigned.</param>
        /// <param name="YPath">An array of the Y coordinates during the time of flight with the given interval. This should be left unassigned.</param>
        public static void GetPath(double Vo, double θo, double timeOfFlight, double interval, out double[] XPath, out double[] YPath)
        {
            double[] dummyTime = new double[0];
            GetPath(Vo, θo, timeOfFlight, interval, out XPath, out YPath, out dummyTime);
        }

        /// <summary>
        /// Calculates all the positions of the projectile within a time of flight. Outputs arrays of the X coordinates and 
        /// the Y coordinate.
        /// </summary>
        /// <param name="Vo">Initial velocity of the projectile, measured in metre per second (m/s).</param>
        /// <param name="θo">Initial angle of the projectile, measured in radians (rad).</param>
        /// <param name="timeOfFlight">Total duration of flight, measured in seconds (s).</param>
        /// <param name="interval">Interval between each calculation.</param>    
        /// <param name="XPath">An array of the calculated X coordinates during the time of flight with the given interval. This should be left unassigned.</param>
        /// <param name="YPath">An array of the calculated Y coordinates during the time of flight with the given interval. This should be left unassigned.</param>
        /// <param name="Time">An array of .</param>
        public static void GetPath(double Vo, double θo, double timeOfFlight, double interval, out double[] XPath, out double[] YPath, out double[] Time)
        {
            XPath = new double[global::System.Convert.ToInt32(Math.Round(timeOfFlight / interval) + 1)];
            YPath = new double[global::System.Convert.ToInt32(Math.Round(timeOfFlight / interval) + 1)];
            Time = new double[global::System.Convert.ToInt32(Math.Round(timeOfFlight / interval) + 1)];
            int index = 0;

            for (double i = 0; i <= timeOfFlight; i = i + interval)
            {
                XPath[index] = CalculateX(Vo, θo, i);
                YPath[index] = CalculateY(Vo, θo, i);
                Time[index] = i;
                index++;
            }
        }

        /// <summary>
        /// Calculates the horizontal distance of a projectile when it returns to its initial height.
        /// </summary>
        /// <param name="Vo"></param>
        /// <param name="θo"></param>
        /// <returns></returns>
        public static double Range(double Vo, double θo)
        {
            return Math.Pow(Vo, 2) * Math.Sin(2 * θo) / G;
        }
        
    }
}

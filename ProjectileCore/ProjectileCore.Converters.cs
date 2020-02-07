using System;

namespace Projectile.Core
{
    public static partial class ProjectileCore
    {
        public static class Convert
        {
            public static double ToRadians(double degAngle)
            {
                return Math.PI * degAngle / 180;
            }

            public static double ToDegree(double radAngle)
            {
                return radAngle * 180 / Math.PI;
            }
        }
    }
}

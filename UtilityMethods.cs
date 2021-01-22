using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatSharp;

namespace LibraryEngine
{
    public static class UtilityMethods
    {
        /// <summary>
        /// Calculates slope between two points.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static int CalcSlope(int x1, int y1, int x2, int y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        public static float CalcSlope(float x1, float x2, float y1, float y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        public static double CalcSlope(double x1, double x2, double y1, double y2)
        {
            return (y2-y1)/(x2-x1);
        }

        /// <summary>
        /// Calculates an angle into a (normalized) vector.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        /// <summary>
        /// Calculates an angle based on a passed vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static double VectorToAngle(Vector2 vector)
        {
            return Math.Atan2(vector.Y, vector.X);
        }

        /// <summary>
        /// Serializes any object into FlatBuffer format (don't use this pls)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static byte[] Serialize(object target)
        {
            int maxBytesNeeded = FlatBufferSerializer.Default.GetMaxSize(target);
            byte[] buffer = new byte[maxBytesNeeded];
            int bytesWritten = FlatBufferSerializer.Default.Serialize(target, buffer);
            // keep in mind bytesWritten ig
            return buffer;
        }


    }
}

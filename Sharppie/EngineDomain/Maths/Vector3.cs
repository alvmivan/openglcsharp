using System;

namespace EngineDomain.Maths
{
    // ReSharper disable InconsistentNaming for unity code compatibility
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        /// <summary>
        ///   <para>Calculate a position between the points specified by current and target, moving no farther than the distance specified by maxDistanceDelta.</para>
        /// </summary>
        /// <param name="origin">The position to move from.</param>
        /// <param name="destination">The position to move towards.</param>
        /// <param name="maxDist">Distance to move current per call.</param>
        /// <returns>
        ///   <para>The new position.</para>
        /// </returns>
        public static Vector3 MoveTowards(
            Vector3 origin,
            Vector3 destination,
            float maxDist)
        {
            var dirX = destination.x - origin.x;
            var dirY = destination.y - origin.y;
            var dirZ = destination.z - origin.z;
            // to double and then float again to avoid loose precision 
            var sqrLen = (dirX * (double) dirX + dirY * (double) dirY + dirZ * (double) dirZ);
            if (Math.Abs(sqrLen) < 0.000000000001 || maxDist >= 0.0 && sqrLen <= maxDist * maxDist)
                return destination;
            var len = (float) Math.Sqrt(sqrLen);
            return new Vector3(origin.x + dirX / len * maxDist, origin.y + dirY / len * maxDist, origin.z + dirZ / len * maxDist);
        }
        
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }
        
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x; case 1: return y; case 2: return z; default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break; case 1: y = value; break; case 2: z = value; break; default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }
        
        /// <summary>
        ///   <para>Cross Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3((float) (lhs.y * (double) rhs.z - lhs.z * (double) rhs.y), (float) (lhs.z * (double) rhs.x - lhs.x * (double) rhs.z), (float) (lhs.x * (double) rhs.y - lhs.y * (double) rhs.x));
        }

        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            return x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2;
            // ReSharper restore NonReadonlyMemberInGetHashCode
        }
        
        
        /// <summary>
        ///   <para>Returns true if the given vector is exactly equal to this vector.</para>
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is Vector3 other1 && Equals(other1);
        }

        public bool Equals(Vector3 other)
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            return x == (double) other.x && y == (double) other.y && z == (double) other.z;
            // ReSharper restore CompareOfFloatsByEqualityOperator
        }
        
        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return (float) (lhs.x * (double) rhs.x + lhs.y * (double) rhs.y + lhs.z * (double) rhs.z);
        }

        
        
        
        
        
    }
}
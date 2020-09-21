using System;
using System.Globalization;
using UnityEngine;

namespace Libs
{
    public static class Helpers
    {
        public static Vector3 String2Vector(string s)
        {
            string[] parts = s.Split(',');
            if (parts.Length != 3)
            {
                return Vector3.zero;
            }

            float[] floats = new float[3];
            for (int i = 0; i < parts.Length; i++)
            {
                floats[i] = float.Parse(
                    parts[i].Trim(),
                    CultureInfo.InvariantCulture
                );
            }

            return new Vector3(floats[0], floats[1], floats[2]);
        }

        public static Vector3 Forward2Rotation(Vector3 forward)
        {
            float r = 90 + (forward.z * 360);
            return new Vector3(0, 0, r);
        }
    }
}

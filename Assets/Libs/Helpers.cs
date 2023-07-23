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

        public static Quaternion Forward2Rotation(Vector3 forward)
        {
            float angle = Mathf.Atan2(forward.x, forward.y) * Mathf.Rad2Deg *-1;

            return Quaternion.Euler(0, 0, angle);
        }
    }
}

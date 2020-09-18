using System;
using System.Globalization;
using UnityEngine;

namespace Libs
{
    public static class Helpers
    {
        public static Vector3 s2v(string s)
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

            // Note: swapped y&z for Source -> Unity
            return new Vector3(floats[0], floats[2], floats[1]);
        }
    }
}

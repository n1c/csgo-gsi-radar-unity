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

        // https://github.com/lexogrine/hud-manager/blob/4a2906b34e5a4bea3ac65ee408124e9ca549650d/boltobserv/modules/gsi.js#L75
        public static Quaternion Forward2Rotation(Vector3 forward)
        {
            float angle = forward.x > 0
                ? 90 + (forward.y * -1 * 90)
                : 270 + (forward.y * 90)
                ;

            return Quaternion.Euler(0, 0, angle);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Libs
{
    public class MapDimensions
    {
        private const float OVERVIEW_SIZE = 1024f;
        public Rect _dims;

        public MapDimensions(string map)
        {
            if (!MapsData.Dimensions.ContainsKey(map))
            {
                throw new KeyNotFoundException($"No MapDimensions data for {map}");
            }

            _dims = MapsData.Dimensions[map];
        }

        private float ConvertX(float x)
        {
            float offset = -_dims.xMin;
            float lengthX = offset + _dims.xMax;
            float result = (x + offset) / lengthX;
            result *= OVERVIEW_SIZE;
            return result;
        }

        private float ConvertY(float y)
        {
            float offset = -_dims.yMin;
            float length_y = offset + _dims.yMax;
            float result = (y + offset) / length_y;
            return OVERVIEW_SIZE - result * OVERVIEW_SIZE;
        }

        public Vector3 ScaleVector(Vector3 incoming)
        {
            return new Vector3(
                ConvertX(incoming.x),
                ConvertY(incoming.y),
                incoming.z
            );
        }
    }

    public class MapsData
    {
        public static Dictionary<string, Rect> Dimensions = new Dictionary<string, Rect>()
        {
            /*
            { "de_dust2", new Dimension {
                X = -2476,
                Y = 3239,
                Scale = 4.4f,
            } },
            { "de_inferno", new Dimension {
                X = -2087,
                Y = 3870,
                Scale = 4.9f,
            } },
            */
			{ "de_mirage", Rect.MinMaxRect(-3217, -3401, 1912, 1682) },
            /*
            { "de_nuke", new Dimension {
                X = -3453,
                Y = 2887,
                Scale = 7f,
            } },
            { "de_train", new Dimension {
                X = -2477,
                Y = 2392,
                Scale = 4.7f,
            } },
            { "de_vertigo", new Dimension {
                X = -3168,
                Y = 1762,
                Scale = 4.0f,
            } },
            */
        };
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Libs
{
    public class MapDimensions
    {
        public MapsData.Dimension _dims;

        public MapDimensions(string map)
        {
            if (!MapsData.Dimensions.ContainsKey(map))
            {
                throw new KeyNotFoundException($"No MapDimensions data for {map}");
            }

            _dims = MapsData.Dimensions[map];
        }

        public Vector3 ScaleVector(Vector3 incoming)
        {
            Vector3 resized = new Vector3(
                (float)(_dims.Y - incoming.y) / _dims.Scale,
                (float)(incoming.x - _dims.X) / _dims.Scale,
                incoming.z
            );

            return Quaternion.Euler(0, 0, -90) * resized;
        }
    }

    public class MapsData
    {
        public struct Dimension
        {
            public float X;
            public float Y;
            public float Scale;
        }

        public static Dictionary<string, Dimension> Dimensions = new Dictionary<string, Dimension>()
        {
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
            { "de_mirage", new Dimension {
                X = -3230,
                Y = 1713,
                Scale = 5f,
            } },
            { "de_nuke", new Dimension {
                X = -3453,
                Y = 2887,
                Scale = 7f,
            } },
            { "de_overpass", new Dimension {
                X = -4831,
                Y = 1781,
                Scale = 5.2f,
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
        };
    }
}

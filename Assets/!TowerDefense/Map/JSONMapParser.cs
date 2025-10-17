using System;
using System.Collections.Generic;
using UnityEngine;

public class JSONMapParser
{
    //public MapConfig ParseJSONMap()
    //{

    //}

    public static List<Vector2Int> ParsePath(Vector2Int start, string path)
    {
        List<Vector2Int> computedPath = new();

        Vector2Int currComputedStep = new(start.x, start.y);

        foreach (var step in path)
        {
            computedPath.Add(currComputedStep);

            switch (step)
            {
                case 'U':
                    currComputedStep.y++;
                    break;
                case 'L':
                    currComputedStep.x--;
                    break;
                case 'D':
                    currComputedStep.y--;
                    break;
                case 'R':
                    currComputedStep.x++;
                    break;
                       
                default:
                    throw new ArgumentException($"Invalid direction");
            }
        }

        return computedPath;
    }
}

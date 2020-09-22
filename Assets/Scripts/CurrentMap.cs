using Libs;
using System.IO;
using UnityEngine;

public class CurrentMap : MonoBehaviour
{
    public MapDimensions Dims;

    public void Changed(string map)
    {
        map = Path.GetFileName(map);
        Dims = new Libs.MapDimensions(map);

        Sprite mapSprite = Resources.Load<Sprite>("overviews/" + map);
        gameObject.GetComponent<SpriteRenderer>().sprite = mapSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using Libs;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Sprite _ctTexture = default;
    [SerializeField] private Sprite _tTexture = default;
    [SerializeField] private GameObject _isMain = default;

    public void SetData(MapDimensions mapDimensions, PayloadModels.Player p, bool isMain)
    {
        Vector3 position = Helpers.String2Vector(p.position);
        Vector3 rotation = Helpers.String2Vector(p.forward);

        position = mapDimensions.ScaleVector(position);
        rotation = Helpers.Forward2Rotation(rotation);

        // @TODO: Before we figure out Nuke etc, just set z to 0
        position.z = 0;

        // @TODO: Check health and draw as dead if required?
        gameObject.GetComponent<SpriteRenderer>().sprite = Sprite(p);
        gameObject.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));

        if (isMain)
        {
            _isMain.SetActive(true);
        }
    }

    private Sprite Sprite(PayloadModels.Player player)
    {
        return player.team == "CT" ? _ctTexture : _tTexture;
    }
}

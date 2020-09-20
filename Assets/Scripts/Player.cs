﻿using System.Collections;
using System.Collections.Generic;
using Libs;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Sprite _ctTexture = default;
    [SerializeField] Sprite _tTexture = default;

    public void SetData(MapDimensions mapDimensions, PayloadModels.Player p, bool isMain = false)
    {
        Vector3 position = Helpers.String2Vector(p.position);
        Vector3 rotation = Helpers.String2Vector(p.forward);
        position = mapDimensions.ScaleVector(position);

        // @TODO: Before we figure out Nuke etc, just set z to 0
        position.z = 0;

        gameObject.GetComponent<SpriteRenderer>().sprite = Sprite(p);
        gameObject.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));

        if (isMain)
        {
            // @TODO: Draw the view indicator things.
        }
    }

    private Sprite Sprite(PayloadModels.Player player)
    {
        return player.team == "CT" ? _ctTexture : _tTexture;
    }
}

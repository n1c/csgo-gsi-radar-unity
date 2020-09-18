using Libs;
using PayloadModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    [SerializeField] private GameObject _playerGameObject = default;
    [SerializeField] private GameObject _mapGameObject = default;

    private string _currentMap;

    public void NewPayload(Payload payload)
    {
        /*
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(p);
        }
        */

        if (_currentMap != payload.map.name)
        {
            Debug.Log("New map! " + payload.map.name);
            _currentMap = payload.map.name;
        }

        DrawPlayer(payload.player, true);
    }

    private void DrawPlayer(Player p, bool isMain = false)
    {
        if (p.position == null)
        {
            return;
        }

        Debug.Log($"DrawPlayer: {p.position} + {Helpers.s2v(p.position)}");

        Vector3 playerPosition = Helpers.s2v(p.position);
        Instantiate(_playerGameObject, playerPosition, Quaternion.identity);

        if (isMain)
        {
            // @TODO: Draw FOV?
        }
    }
}

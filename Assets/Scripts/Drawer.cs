using Libs;
using PayloadModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    [SerializeField] private GameObject _playerGameObject = default;
    [SerializeField] private GameObject _mapGameObject = default;

    private Listener _listener;
    private string _currentMap;
    private Libs.MapDimensions _mapDimensions;

    private void Start()
    {
        _listener = GameObject.Find("Listener").GetComponent<Listener>();
        _listener.NewPayload += HandlePayload;
    }

    private void HandlePayload(object _, Listener.NewPayloadEventArgs e)
    {
        Debug.Log("Payload: " + e.Payload);

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(p);
        }

        if (_currentMap != e.Payload.map.name)
        {
            Debug.Log("New map! " + e.Payload.map.name);
            _currentMap = e.Payload.map.name;
            _mapDimensions = new Libs.MapDimensions(_currentMap);

            Texture2D mapTexture = Resources.Load<Texture2D>("csgo-overviews/overviews/" + _currentMap);
            if (mapTexture == null)
            {
                Debug.Log("Failed to find Map texture for " + _currentMap);
            }
            else
            {
                _mapGameObject.GetComponent<MeshRenderer>().material.mainTexture = mapTexture;
            }
        }

        // DrawPlayer(e.Payload.player, true);
        foreach (KeyValuePair<string, Player> kv in e.Payload.allplayers)
        {
            // Only draw the allplayers on the _same team_ as player.
            if (kv.Value.team == e.Payload.player.team)
            {
                DrawPlayer(kv.Value, kv.Key == e.Payload.player.steamid);
            }
        }
    }

    private void DrawPlayer(Player p, bool isMain = false)
    {
        if (p.position == null)
        {
            return;
        }

        Vector3 playerPosition = Helpers.String2Vector(p.position);
        playerPosition = _mapDimensions.ScaleVector(playerPosition);
        Instantiate(_playerGameObject, playerPosition, Quaternion.identity);

        if (isMain)
        {
            // @TODO: Draw FOV?
        }
    }
}

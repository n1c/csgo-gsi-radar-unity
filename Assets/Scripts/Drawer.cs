using Libs;
using PayloadModels;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        _listener = gameObject.GetComponent<Listener>();
        _listener.NewPayload += HandlePayload;
    }

    private void HandlePayload(object _, Listener.NewPayloadEventArgs e)
    {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(p);
        }

        if (e.Payload.player.activity == "menu")
        {
            return;
        }

        if (_currentMap != e.Payload.map.name)
        {
            _currentMap = Path.GetFileName(e.Payload.map.name);
            _mapDimensions = new Libs.MapDimensions(_currentMap);

            Sprite mapSprite = Resources.Load<Sprite>("overviews/" + _currentMap);
            _mapGameObject.GetComponent<SpriteRenderer>().sprite = mapSprite;
        }

        if (e.Payload.allplayers == null)
        {
            return;
        }

        foreach (KeyValuePair<string, PayloadModels.Player> kv in e.Payload.allplayers)
        {
            // Only draw the allplayers on the _same team_ as player.
            if (kv.Value.team == e.Payload.player.team)
            {
                DrawPlayer(_mapDimensions, kv.Value, kv.Key == e.Payload.player.steamid);
            }
        }
    }

    private void DrawPlayer(MapDimensions mapDimensions, PayloadModels.Player p, bool isMain)
    {
        if (p.position == null)
        {
            return;
        }

        GameObject _newPlayer = Instantiate(_playerGameObject);
        _newPlayer.GetComponent<Player>().SetData(mapDimensions, p, isMain);
    }
}

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

        DrawPlayer(e.Payload.player, true);
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

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
    private Dictionary<string, GameObject> _players = new Dictionary<string, GameObject>();

    private void Start()
    {
        _listener = gameObject.GetComponent<Listener>();
        _listener.NewPayload += HandlePayload;
    }

    private void HandlePayload(object _, Listener.NewPayloadEventArgs e)
    {
        if (e.Payload.player.activity == "menu")
        {
            return;
        }

        if (_currentMap != e.Payload.map.name)
        {
            _currentMap = e.Payload.map.name;
            _mapGameObject.GetComponent<CurrentMap>().Changed(e.Payload.map.name);

            _players = new Dictionary<string, GameObject>();
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(p);
            }
        }

        if (e.Payload.allplayers == null)
        {
            return;
        }

        foreach (KeyValuePair<string, PayloadModels.Player> kv in e.Payload.allplayers)
        {
            PayloadModels.Player LoopPlayer = kv.Value;
            PayloadModels.Player MainPlayer = e.Payload.player;
            string LoopPlayerSteamID = kv.Key;
            bool SameTeam = LoopPlayer.team == MainPlayer.team;

            // Only draw the allplayers on the _same team_ as player.
            // If we have a loop player, that is not on the spec player's team
            if (_players.ContainsKey(LoopPlayerSteamID) && !SameTeam)
            {
                // @TODO: Display a death icon?
                Destroy(_players[LoopPlayerSteamID]);
                _ = _players.Remove(LoopPlayerSteamID);
            }

            if (!_players.ContainsKey(LoopPlayerSteamID) && SameTeam)
            {
                _players[LoopPlayerSteamID] = Instantiate(_playerGameObject);
            }

            if (_players.ContainsKey(LoopPlayerSteamID))
            {
                _players[LoopPlayerSteamID].GetComponent<Player>().SetData(LoopPlayer, LoopPlayerSteamID == MainPlayer.steamid);
            }
        }
    }
}

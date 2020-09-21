using System;
using System.Collections.Generic;

namespace PayloadModels
{
    [Serializable]
    public struct Payload
    {
        public Auth auth;
        public Map map;
        public Player player;
        public Dictionary<string, Player> allplayers;
        public Dictionary<string, Grenade> grenades;
        public Bomb bomb;
    }

    [Serializable]
    public struct Auth
    {
        public string app;
    }

    [Serializable]
    public struct Map
    {
        public string name;
    }

    [Serializable]
    public struct Player
    {
        public string steamid;
        public string name;
        public string team;
        public string position;
        public string forward;
        public string activity;
    }

    [Serializable]
    public struct Grenade
    {
        public string owner;
        public string position;
        public string velocity;
        public float lifetime;
        public string type;
    }

    [Serializable]
    public struct Bomb
    {
        public string state;
        public string position;
        public string player;
    }
}

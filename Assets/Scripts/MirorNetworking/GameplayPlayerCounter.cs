using Mirror;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Gameplay
{
    public class GameplayPlayerCounter : NetworkBehaviour
    {
        [SyncVar] int _maxPlayer = 2;
        [SyncVar] int _playerReady = 0;
        public List<Sprite> _iconList = new List<Sprite> ();


        public void UpdatePlayerCount()
        {
            // Get Game Players
            var players = FindObjectsOfType<GamePlayer>();

            if (players.Length == _maxPlayer)
            {
                var roomPlayers = FindObjectsOfType<Assets.Scripts.Room.RoomPlayer>();
                Array.Reverse(players);
                Array.Reverse(roomPlayers);

                // Set up icons
                for (int i = 0; i < roomPlayers.Length; i++)
                {
                    players[i].GetComponent<SpriteRenderer>().sprite = _iconList[roomPlayers[i].GetIcon()];
                }
            }
        }

        void Start()
        {
            if (isServer)
            {
                var manager = NetworkManager.singleton as Assets.Scripts.Room.RoomManager;
                _maxPlayer = manager.maxConnections;
            }
        }
    }
}
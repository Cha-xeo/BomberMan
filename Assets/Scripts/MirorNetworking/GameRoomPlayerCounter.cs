using System;
using System.Collections;
using UnityEngine;
using Mirror;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Assets.Scripts.Room
{
    public class GameRoomPlayerCounter : NetworkBehaviour
    {
        [SyncVar] int _minPlayer;
        [SyncVar] int _maxPlayer;
        [SyncVar] int _Timer = 8;
        [SerializeField] TMPro.TextMeshProUGUI _playerCountText;
        [SerializeField] TMPro.TextMeshProUGUI _timerText;

        [Header("UI")]
        // Player List
        public Transform roomPlayerListTransform;
        public GameObject roomPlayerPrefab;
        List<GameObject> _roomPlayerList = new List<GameObject>();

        public void UpdatePlayerCount()
        {
            var players = FindObjectsOfType<RoomPlayer>();

            // Update UI
            UpdatePlayerList(players);

            bool canStart = players.Length >= _minPlayer;
            _playerCountText.color = canStart ? Color.black : Color.red;
            _playerCountText.text = string.Format("{0}:{1}", players.Length, _maxPlayer);
            if (players.Length == _maxPlayer && isServer)
            {
                StartCoroutine(StartCountDown());
                // start countdown
            }
        }

        // Update the display according to the number of players in the room
        void UpdatePlayerList(RoomPlayer[] players)
        {
            // Clear List
            foreach (var player in _roomPlayerList)
            {
                Destroy(player.gameObject);
            }
            _roomPlayerList.Clear();

            // ### Update List ### //
            Array.Reverse(players);
            
            // Get used icons
            List<int> usedIdx = new List<int>();
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetIcon() != -1)
                    usedIdx.Add(players[i].GetIcon());
            }

            // Update display
            for (int i = 0; i < players.Length; i++)
            {
                GameObject newRoomPlayer = Instantiate(roomPlayerPrefab, roomPlayerListTransform);

                // Sync Icon
                if (players[i].GetIcon() != -1)
                    newRoomPlayer.GetComponent<IconRandomizer>().SetSpriteByIdx(players[i].GetIcon());
                else {
                    newRoomPlayer.GetComponent<IconRandomizer>().ChooseRandomIcon(usedIdx);
                    players[i].SetIcon(newRoomPlayer.GetComponent<IconRandomizer>().GetSpriteIdx());
                }

                // Player Idx
                newRoomPlayer.transform.GetChild(1).GetComponent<Text>().text = $"Player {i + 1}";

                _roomPlayerList.Add(newRoomPlayer);

            }
        }

        void Start()
        {
            if (isServer)
            {
                var manager = NetworkManager.singleton as RoomManager;
                _minPlayer = manager.minPlayers;
                _maxPlayer = manager.maxConnections;
            }
        }
        
        IEnumerator StartCountDown()
        {
            while (_Timer > 0)
            {
                RpcTickTimer();
                yield return new WaitForSecondsRealtime(1f);
            }
            var manager = NetworkManager.singleton as RoomManager;
            manager.ServerChangeScene(manager.GameplayScene);
            // start the game
        }
        [ClientRpc]
        public void RpcTickTimer()
        {
            _Timer--;
            _timerText.text = _Timer.ToString();
        }
    }
}

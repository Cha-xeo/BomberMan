using System.Collections;
using UnityEngine;
using Mirror;

namespace Assets.Scripts.Room
{
    public class GameRoomPlayerCounter : NetworkBehaviour
    {
        [SyncVar] int _minPlayer;
        [SyncVar] int _maxPlayer;
        [SyncVar] int _Timer = 8;
        [SerializeField] TMPro.TextMeshProUGUI _playerCountText;
        [SerializeField] TMPro.TextMeshProUGUI _timerText;
        public void UpdatePlayerCount()
        {
            var players = FindObjectsOfType<RoomPlayer>();
            bool canStart = players.Length >= _minPlayer;
            _playerCountText.color = canStart ? Color.black : Color.red;
            _playerCountText.text = string.Format("{0}:{1}", players.Length, _maxPlayer);
            if (players.Length == _maxPlayer && isServer)
            {
                StartCoroutine(StartCountDown());
                // start countdown
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

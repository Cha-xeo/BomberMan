using UnityEngine;
using Mirror;

namespace Assets.Scripts.Room
{
    public class RoomUI : MonoBehaviour
    {
        public static RoomUI Instance { get; private set; }

        [SerializeField] GameRoomPlayerCounter _gameRoomPlayerCounter;
        public GameRoomPlayerCounter GameRoomPlayerCounter { get { return _gameRoomPlayerCounter; } }

        private void Awake()
        {
            Instance = this;
        }
        public void CancelRoom()
        {
            var manager = RoomManager.singleton;
            if (manager.mode == NetworkManagerMode.Host)
            {
                manager.StopHost();
            }
            else if (manager.mode == NetworkManagerMode.ClientOnly)
            {

                manager.StopClient();
            }
            //manager.ServerChangeScene(manager.GetComponentInParent<NetworkRoomManager>().offlineScene);
        }
    }
}
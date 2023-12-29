using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace Assets.Scripts.Room
{
    public class RoomPlayer : NetworkRoomPlayer
    {
        public void Start()
        {
            base.Start();
            RoomUI.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
        }

        private void OnDestroy()
        {
            if (RoomUI.Instance)
            {
                RoomUI.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
            }
        }
    }
}
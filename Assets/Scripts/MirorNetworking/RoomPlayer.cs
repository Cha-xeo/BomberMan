using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace Assets.Scripts.Room
{
    public class RoomPlayer : NetworkRoomPlayer
    {
        [SyncVar] int _iconIdx = -1;

        public void Start()
        {
            base.Start();
            RoomUI.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
        }

        public int GetIcon() {  return _iconIdx; }
        public void SetIcon(int idx) { _iconIdx = idx; }

        private void OnDestroy()
        {
            if (RoomUI.Instance)
            {
                RoomUI.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
            }
        }
    }
}
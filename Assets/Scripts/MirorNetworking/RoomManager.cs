using UnityEngine;
using Mirror;
namespace Assets.Scripts.Room
{
    public class RoomManager : NetworkRoomManager
    {
        public override void OnRoomClientConnect()
        {
            base.OnRoomClientConnect();
            Debug.Log("new client in room");
        }
    }
}
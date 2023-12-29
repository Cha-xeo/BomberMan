using Mirror;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class GameplayPlayerCounter : NetworkBehaviour
    {
        [SyncVar] int _maxPlayer;
        [SyncVar] int _playerReady = 0;


        public void UpdatePlayerCount()
        {

        }
    }
}
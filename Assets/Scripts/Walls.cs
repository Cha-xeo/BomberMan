using Mirror;
using Assets.Scripts.Interface;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Walls : NetworkBehaviour, IDamageable
    {
        [SerializeField] GameObject _underGridPrefab;
        [SerializeField] [SyncVar] int _health;
        public int Health
        {
            get => _health;
            set
            {
                // TODO clamp to hp max, check for overhealth
                _health = value;
                if (value <= 0)
                {
                    DestroySelf();
                    //TODO death
                }
            }
        }

        void Start()
        {
            Instantiate(_underGridPrefab, transform.parent);    
        }

        [ServerCallback]
        public void Damage(int amount)
        {
            Health -= amount;
        }

        [Server]
        void DestroySelf()
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
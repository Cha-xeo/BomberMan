using Assets.Scripts.Bomb;
using Assets.Scripts.Interface;
using Mirror;
using Mirror.Examples.BenchmarkIdle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.Progress;

namespace Assets.Scripts.Players
{
    public class CharacterAction : NetworkBehaviour
    {
        [SerializeField] GameObject _bombPrefab;
        Transform _bombHolder;
        Transform _thisTranform;
        [SerializeField] float _bombRange;
        [SerializeField] float _bombTimer;
        [SerializeField] int _bombAttack;

        [SerializeField] float _attackSpeed;
        float _nextShot = 0.0f;
        private void Start()
        {
            _bombHolder = GameObject.FindGameObjectsWithTag(Constants.TAG_BOMBHOLDER)[0].transform;
            _thisTranform = GetComponent<Transform>();
        }

        void Update()
        {
            if (!isLocalPlayer) return;
            if (Input.GetKey(KeyCode.X) && _nextShot + _attackSpeed < Time.time)
            {
                CmdDropBomb();
                _nextShot = Time.time;
            }
        }

        [Command]
        void CmdDropBomb()
        {
            if (_bombPrefab)
            {
                Vector3 tmp = _thisTranform.position;

                Collider2D[] cols = Physics2D.OverlapPointAll(_thisTranform.position);
                foreach (Collider2D col in cols)
                {
                    if (col.gameObject.TryGetComponent(out UnderGrid grid))
                    {
                        tmp = grid.transform.position;
                    }
                }

                GameObject Bomb = Instantiate(_bombPrefab, tmp, Quaternion.identity, _bombHolder);
                Debug.Log(" Bomb Range: " + _bombRange + " Bomb timer: " + _bombTimer + " Bomb atk: " + _bombAttack);
                Bomb.GetComponent<Bombs>().Init(_bombRange, _bombTimer, _bombAttack);
                NetworkServer.Spawn(Bomb);

                RpcDropBomb();
            }
        }

        [ClientRpc]
        void RpcDropBomb()
        {
            //_animator.SetTrigger("Drop");
        }
    }
}
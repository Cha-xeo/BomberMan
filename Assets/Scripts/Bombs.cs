using Assets.Scripts.Interface;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bomb
{
    public class Bombs : NetworkBehaviour
    {
        [SerializeField] GameObject _firePrefab;
        [SerializeField] GameObject _animationPrefab;
        List<Vector2> BakuatsuList = new List<Vector2>();

        float _range;
        float _duration;
        int _attack;

        public void Init(float range, float duration, int dmg)
        {
            _range = range;
            _duration = duration;
            _attack = dmg;
        }

        public override void OnStartServer()
        {
            Debug.Log(" Range: " + _range + " timer: " + _duration + " atk: " + _attack);
            Invoke(nameof(Explossion), _duration);
        }

        [ServerCallback]
        void Explossion()
        {
            BakuatsuList.Clear();
            CastRay(Vector2.up);
            CastRay(Vector2.down);
            CastRay(Vector2.right);
            CastRay(Vector2.left);
            RpcAnimateBomb(BakuatsuList);
            Invoke(nameof(DestroySelf), 0.5f);
        }


        [ServerCallback]
        void CastRay(Vector2 dir)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, _range);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag(Constants.TAG_WALL))
                {
                    break;
                }
                if (hit.collider.gameObject.TryGetComponent(out IDamageable obj))
                {
                    obj.Damage(_attack);
                }
                if (hit.collider.CompareTag(Constants.TAG_UNDERGRID))
                {
                    BakuatsuList.Add(hit.transform.position);
                }
            }
        }

        [Server]
        void DestroySelf()
        {
            NetworkServer.Destroy(gameObject);
        }

        [ClientRpc]
        void RpcAnimateBomb(List<Vector2> list)
        {
            _animationPrefab.SetActive(true);
            foreach (Vector2 item in list)
            {
                Collider2D[] cols = Physics2D.OverlapPointAll(item);
                foreach (Collider2D col in cols)
                {
                    if (col.gameObject.TryGetComponent(out UnderGrid grid))
                    {
                        grid.PlayExplosion();
                    }
                }
            }
        }
    }
}
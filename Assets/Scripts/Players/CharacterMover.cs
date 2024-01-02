using Mirror;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Players
{
    public class CharacterMover : NetworkBehaviour
    {
        public bool isMoveable;
        [SyncVar] public float speed;

        // Update is called once per frame
        void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            if (isOwned && isMoveable)
            {
                //CmdMovePlayer();
                Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f), 1f);
                
                // Change player looking direction
                /*if (dir.x < 0f)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if (dir.x > 0f)
                {
                    transform.localScale = new Vector3( 1f, 1f, 1f);
                }*/
                transform.position += dir * speed * Time.deltaTime;
            }
        }

        /*[Command]
        void CmdMovePlayer()
        {
            if (_bombPrefab)
            {
                GameObject cube = Instantiate(_bombPrefab, _thisTranform.position, Quaternion.identity, _bombHolder);
                NetworkServer.Spawn(cube);
            }
        }*/
    }
}
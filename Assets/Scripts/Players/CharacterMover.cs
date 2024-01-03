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
                Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f), 1f);
                
                transform.position += dir * speed * Time.deltaTime;
            }
        }
    }
}
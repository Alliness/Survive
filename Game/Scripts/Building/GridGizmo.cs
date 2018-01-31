using UnityEngine;

namespace Game.Scripts.Building
{
    public class GridGizmo : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            gameObject.layer = transform.GetComponentInParent<TileBlock>().gameObject.layer;
            if (transform.GetComponentInParent<TileBlock>().Occupied)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.cyan;
            }

            Gizmos.DrawWireCube(transform.position, new Vector3(Constants.tileSizeX, Constants.tileSizeY, Constants.tileSizeZ));
        }
    }
}
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public Vector3 targetOffset;
        public float minDistance;
        public float maxDistance;
        public int zoomRate;
        public float panSpeed;
        public float zoomDampening;

        private float currentDistance;
        private float desiredDistance;
        private Vector3 position;

        private void Awake()
        {
            if (!target)
            {
                GameObject go = new GameObject("Cam Target");
                go.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                target = go.transform;
            }

            position = transform.position;
        }
        
        void LateUpdate()
        {
            float distanceMultiplier = CalcUtils.getPrecents(-position.z, maxDistance);
            if (Input.GetMouseButton(2))
            {
                target.rotation = transform.rotation;
                target.Translate(Vector3.right * -Input.GetAxis("Mouse X") * CalcUtils.percentMultiplier(panSpeed, distanceMultiplier));
                target.Translate(transform.up * -Input.GetAxis("Mouse Y") *  CalcUtils.percentMultiplier(panSpeed, distanceMultiplier), Space.World);
            }

            if (EventController.instance.GetLayer() != Enums.GameLayer.GUI)
            {
                desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate *
                                   Mathf.Abs(desiredDistance);

                desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
                currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);
                
            }

            position = target.position - ( Vector3.forward * currentDistance + targetOffset);
            transform.position = position;

        }

    }
}
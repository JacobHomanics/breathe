using UnityEngine;

namespace JacobHomanics.Core.Raycasts
{

    public class RaycastControllerIngameVisualizer : MonoBehaviour
    {
        public float lineLength = 10000f;

        public LineRenderer lineRenderer;
        public GameObject go;

        public RaycastController controller;


        void Update()
        {
            if (controller.GetHit().collider != null)
            {
                lineRenderer.SetPosition(0, controller.GetRay().origin);
                lineRenderer.SetPosition(1, controller.GetHit().point);

                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;

                go.SetActive(true);
                go.transform.position = controller.GetHit().point;
            }
            else
            {
                lineRenderer.SetPosition(0, controller.GetRay().origin);

                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;

                Vector3 startPoint = controller.GetRay().origin;
                Vector3 endPoint = controller.GetRay().origin + controller.GetRay().direction * lineLength; // Adjust lineLength as needed



                lineRenderer.SetPosition(1, endPoint);
                go.SetActive(false);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetNavigation : MonoBehaviour
{
    [SerializeField] 
    private Camera topDownCamera;

    [SerializeField] 
    private GameObject navTargetObject;

    private NavMeshPath path;   // current calculated path
    private LineRenderer line;  // line renderer to display path

    private void Start()
    {
        path = new NavMeshPath();
        line = GetComponent<LineRenderer>();

        // cấu hình LineRenderer hiển thị màu xanh
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.green;
        line.endColor = Color.green;
    }

    public void Update()
    {
        if (GlobalProperties.Instance.IsShowNavigation) {
            if (navTargetObject == null) return;

            // tính toán đường đi trên NavMesh
            NavMesh.CalculatePath(
                transform.position,
                navTargetObject.transform.position,
                NavMesh.AllAreas,
                path
            );

            // vẽ đường đi bằng LineRenderer
            if (path.corners.Length > 1)
            {
                line.positionCount = path.corners.Length;
                line.SetPositions(path.corners);

                // để đường nằm gần mặt đất hơn một chút
                for (int i = 0; i < path.corners.Length; i++)
                {
                    Vector3 pos = path.corners[i];
                    pos.y += 0.05f; // nâng nhẹ lên khỏi mặt đất
                    path.corners[i] = pos;
                }

                line.enabled = true;
            }
            else
            {
                line.enabled = false;
            }
        }
    }
}

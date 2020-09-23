/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FieldOfView : MonoBehaviour {

    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    public float radius;
    public float totalAngle;
    public float maxDistanceToPlayer;
    private Vector3 origin;
    [HideInInspector]
    public float startingAngle;

    [HideInInspector]
    public bool lookAt = false;

    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
    }

    private void Update()
    {
        if (lookAt)
        {
            TargetPlayer();
        }
    }

    private void LateUpdate() {
        int rayCount = 50;
        float angle = startingAngle;
        float angleIncrease = totalAngle / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++) {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), radius, layerMask);
            if (raycastHit2D.collider == null) {
                // No hit
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * radius;
            } else {
                // Hit object
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0) {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
    }

    public void SetOrigin(Vector3 origin) {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection) {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + totalAngle / 2f;
    }

    public void SetFoV(float fov) {
        this.totalAngle = fov;
    }

    public void SetViewDistance(float viewDistance) {
        this.radius = viewDistance;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<Turret>().enabled = true;
            GetComponentInParent<Patrol>().enabled = false;
            lookAt = true;
        }
    }

    public void TargetPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Vector3 difPos = player.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difPos.y, difPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (Vector3.Distance(this.transform.position, player.transform.position) >= maxDistanceToPlayer)
        {
            GetComponentInParent<Turret>().enabled = false;
            GetComponentInParent<Patrol>().enabled = true;
            lookAt = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, maxDistanceToPlayer);
    }
}

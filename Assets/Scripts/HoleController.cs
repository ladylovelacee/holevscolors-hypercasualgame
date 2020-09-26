using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoleController : MonoBehaviour
{
    [Header("Hole Mesh")]
    [SerializeField] MeshFilter meshFilter = default;
    [SerializeField] MeshCollider meshCollider  = default;
    Mesh mesh;

    [Header("Hole Vertices Radius")]
    [SerializeField] Vector2 moveLimits = default;
    [SerializeField] Transform holeCenter = default;
    [SerializeField] Transform rotatingCircle = default;
    [SerializeField] float radius = default;
    
    List<int> holeVertices;
    List<Vector3> offsets;

    int holeVerticesCount;

    float x, y;
    Vector3 touch, targetPos;
    [Space]
    [SerializeField] [Range(0,1)] float moveSpeed = default;

    void Start()
    {
        RotateCircleAnim();

        Game.isMoving = false;
        Game.isGameOver = false;

        holeVertices = new List<int>();
        offsets = new List<Vector3>();

        mesh = meshFilter.mesh;

        FindHoleVertices();
    }

    void RotateCircleAnim()
    {
        rotatingCircle
            .DORotate(new Vector3(90f, 0f, -90f), .2f)
            .SetEase(Ease.Linear)
            .From(new Vector3(90f, 0f, 0f))
            .SetLoops(-1, LoopType.Incremental);
    }

    void Update()
    {
        Game.isMoving = Input.GetMouseButton(0);

        if(!Game.isGameOver && Game.isMoving)
        {
            //Move hole center position
            MoveHoleCenterPosition();
            //Update hole vertices
            UpdateHoleVertices();
        }
    }

    void MoveHoleCenterPosition()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(
            holeCenter.position,
            holeCenter.position + new Vector3(x, 0f, y), moveSpeed);

        targetPos = new Vector3
            (
            Mathf.Clamp(touch.x, -moveLimits.x, moveLimits.x),
            touch.y,
            Mathf.Clamp(touch.z, -moveLimits.y, moveLimits.y)
            );

        holeCenter.position = targetPos;
    }

    void UpdateHoleVertices()
    {
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }

        //Update mesh
        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    void FindHoleVertices()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);
            if(distance < radius)
            {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - holeCenter.position);
            }
        }

        holeVerticesCount = holeVertices.Count;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(holeCenter.position, radius);
    }
}

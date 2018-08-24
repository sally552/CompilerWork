using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathEnemyes : MonoBehaviour
{
    [SerializeField]
    private Color gizmoColor = Color.green;

    [SerializeField]
    private bool isDrawLine = true;

    public DeathDirection DeathDirection;

    [SerializeField]
    private string pathName;

    [SerializeField]
    private List<Transform> pathPoints;

    private List<NavMeshPath> paths = new List<NavMeshPath>();

    public Vector3 GetSpawnLine()
    {
        return pathPoints[0].position;
    }

    public IEnumerable<NavMeshPath> GetPathMesh()
    {
        return paths;
    }

    public string GetPathName()
    {
        return pathName;
    }

    private void SetPath()
    {
        for (int i = 1; i < pathPoints.Count; i++)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(pathPoints[i-1].position, pathPoints[i].position, NavMesh.AllAreas, path);
            if (path.status == NavMeshPathStatus.PathComplete)
                paths.Add(path);
        }
    }

    private void Awake()
    {
        SetPath();
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        
        if (pathPoints.Count < 2 || pathPoints == null || !isDrawLine)
            return;
        for (int i = 1; i < pathPoints.Count; i++)
            DrawArrow(pathPoints[i-1].position, pathPoints[i].position);
    }

    private void DrawArrow(Vector3 start, Vector3 end)
    {
        Vector3 dir = (end - start).normalized;
        if (dir == Vector3.zero)
            return;

        Gizmos.color = gizmoColor;
        UnityEditor.Handles.color = gizmoColor;

        Gizmos.DrawLine(start, end);
        const float coneCapSize = 1;
        UnityEditor.Handles.ConeHandleCap(0, end - dir * coneCapSize * 0.5f, Quaternion.LookRotation(dir), coneCapSize, EventType.Repaint);
    }
#endif
}

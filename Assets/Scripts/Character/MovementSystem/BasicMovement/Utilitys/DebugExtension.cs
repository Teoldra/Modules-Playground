using UnityEngine;

public static class DebugExtension
{
    public static void DebugWireSphere(Vector3 position, Color color, float radius = 1f, float duration = 0f)
    {
        int segments = 16;
        float angle = 0f;
        float increment = 360f / segments;

        Vector3 lastPoint = Vector3.zero;
        Vector3 nextPoint = Vector3.zero;

        // XY Plane
        lastPoint = position + new Vector3(Mathf.Cos(0f), Mathf.Sin(0f), 0f) * radius;
        for (int i = 1; i <= segments; i++)
        {
            angle = i * increment * Mathf.Deg2Rad;
            nextPoint = position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
            Debug.DrawLine(lastPoint, nextPoint, color, duration);
            lastPoint = nextPoint;
        }

        // XZ Plane
        lastPoint = position + new Vector3(Mathf.Cos(0f), 0f, Mathf.Sin(0f)) * radius;
        for (int i = 1; i <= segments; i++)
        {
            angle = i * increment * Mathf.Deg2Rad;
            nextPoint = position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
            Debug.DrawLine(lastPoint, nextPoint, color, duration);
            lastPoint = nextPoint;
        }

        // YZ Plane
        lastPoint = position + new Vector3(0f, Mathf.Cos(0f), Mathf.Sin(0f)) * radius;
        for (int i = 1; i <= segments; i++)
        {
            angle = i * increment * Mathf.Deg2Rad;
            nextPoint = position + new Vector3(0f, Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Debug.DrawLine(lastPoint, nextPoint, color, duration);
            lastPoint = nextPoint;
        }
    }
}

using UnityEngine;

public class TrajectoryPredictor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform nozzle;       // WAJIB di-assign, titik awal tembakan
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Simulation Settings")]
    [SerializeField] private int resolution = 30;
    [SerializeField] private float timeStep = 0.1f;
    [SerializeField] private float bombMass = 1f;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true; // PENTING: pastikan world space, bukan local
    }

    public void ShowTrajectory(Vector3 force)
    {
        lineRenderer.enabled = true;

        Vector3 startPos = nozzle.position;
        Vector3 velocity = force / bombMass; // Impulse / mass = velocity awal

        Vector3[] points = new Vector3[resolution];
        int actualCount = 0;

        for (int i = 0; i < resolution; i++)
        {
            float t = i * timeStep;

            Vector3 point = startPos
                + velocity * t
                + 0.5f * Physics.gravity * t * t;

            points[i] = point;
            actualCount++;

            if (i > 0 && Physics.Linecast(points[i - 1], point, out RaycastHit hit, groundLayer))
            {
                points[i] = hit.point;
                actualCount = i + 1;
                break;
            }
        }

        lineRenderer.positionCount = actualCount;
        lineRenderer.SetPositions(points);
    }

    public void HideTrajectory()
    {
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 0;
    }
}
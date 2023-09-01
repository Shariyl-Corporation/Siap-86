using System.Collections;
using UnityEngine;

public class Spline : MonoBehaviour
{
    public Transform[] controlPoints;
    public float duration = 2f;

    public float tension, continuity, bias;
    private IEnumerator Start() {
        Debug.Log("Moving");
        while (true){
            yield return FollowSpline();
            yield return new WaitForSeconds(.5f);
        }
    }

    private IEnumerator FollowSpline()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector3 newPosition = CalculateSplinePoint(t);
            Quaternion newRotation = CalculateSplineRotation(t);

            transform.position = newPosition;
            transform.rotation = newRotation;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the object reaches the final control point exactly
        transform.position = controlPoints[2].position;
        transform.rotation = controlPoints[2].rotation;
    }

    private Vector3 CalculateSplinePoint(float t)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * controlPoints[0].position; // P0
        p += 2f * u * t * controlPoints[1].position; // P1
        p += tt * controlPoints[2].position; // P2

        return p;
    }

    private Quaternion CalculateSplineRotation(float t)
    {
        // Calculate the rotation using slerp (spherical linear interpolation)
        Quaternion q0 = Quaternion.Slerp(controlPoints[0].rotation, controlPoints[1].rotation, t);
        Quaternion q1 = Quaternion.Slerp(controlPoints[1].rotation, controlPoints[2].rotation, t);

        return Quaternion.Slerp(q0, q1, t);
    }
    
}
using UnityEngine;
using System.Collections;

public class AI_Sight : MonoBehaviour
{
    public float sightRange = 10f;          // How far the AI can see
    public float sightWidth = 45f;          // Vision angle in degrees (field of view)
    public float perceptionSpeed = 0.5f;    // How often the AI scans (in seconds)
    public float perceptionMemory = 5f;     // Time before AI forgets last seen object

    public string objectID = "";            // Stores detected object's tag or ID
    public Vector3 objectLocation;          // Location of the detected object

    private Coroutine forgetObjectRoutine;  // Coroutine to forget the object

    public string lookingFor;

    private void Start()
    {
        StartCoroutine(PerceiveEnvironment());
    }

    private IEnumerator PerceiveEnvironment()
    {
        while (true)
        {
            DetectObjects();
            yield return new WaitForSeconds(perceptionSpeed);
        }
    }

    private void DetectObjects()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, sightRange);
        bool objectDetected = false;

        foreach (Collider obj in objectsInRange)
        {
            if (obj.CompareTag(lookingFor))  // Adjust this tag as needed
            {
                Vector3 directionToObject = obj.transform.position - transform.position;
                float angleToObject = Vector3.Angle(transform.forward, directionToObject);

                if (angleToObject < sightWidth / 2)
                {
                    objectID = obj.tag;
                    objectLocation = obj.transform.position;
                    objectDetected = true;

                    // Cancel the forget timer if AI re-detects the object
                    if (forgetObjectRoutine != null)
                    {
                        StopCoroutine(forgetObjectRoutine);
                    }
                    break;
                }
            }
        }

        // If no object is detected, start the forget timer
        if (!objectDetected && forgetObjectRoutine == null)
        {
            forgetObjectRoutine = StartCoroutine(ForgetObjectAfterTime());
        }
    }

    private IEnumerator ForgetObjectAfterTime()
    {
        yield return new WaitForSeconds(perceptionMemory);
        objectID = "";                 // Reset object ID after memory duration
        objectLocation = Vector3.zero; // Reset object location
        forgetObjectRoutine = null;    // Reset the forget timer
    }

    private void OnDisable()
    {
        // Stop all routines if the component is disabled
        StopAllCoroutines();
    }
}

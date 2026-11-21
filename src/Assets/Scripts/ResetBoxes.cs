using System.Collections;
using UnityEngine;

public class ResetBoxes : MonoBehaviour, IInteractable
{
    [SerializeField] Transform[] tBoxes;
    [SerializeField] Renderer[] rBumps;
    Material thisMaterial;

    Color startColor;
    Vector3[] startPosition;
    Quaternion[] startRotation;

    void Start()
    {
        thisMaterial = GetComponent<Renderer>().material;

        startPosition = new Vector3[tBoxes.Length];
        startRotation = new Quaternion[tBoxes.Length];

        for (int i = 0; i < tBoxes.Length; i++)
        {
            startPosition[i] = tBoxes[i].position;
            startRotation[i] = tBoxes[i].rotation;
        }

        startColor = rBumps[0].material.color;
    }

    public void Interaction()
    {
        ResetBoxPosition();
        ResetColor();
    }

    void ResetBoxPosition()
    {
        for (int i = 0; i < tBoxes.Length; i++)
        {
            tBoxes[i].SetPositionAndRotation(startPosition[i], startRotation[i]);
        }
        StartCoroutine(ChangeColor());
    }

    void ResetColor()
    {
        for (int i = 0; i < rBumps.Length; i++)
        {
            rBumps[i].material.color = startColor; 
        }
    }

    IEnumerator ChangeColor()
    {
        thisMaterial.SetColor("_EmissionColor", Color.green);
        yield return new WaitForSeconds(1f);
        thisMaterial.SetColor("_EmissionColor", Color.red);
    }
} 

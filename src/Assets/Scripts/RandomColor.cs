using UnityEngine;

public class RandomColor : MonoBehaviour, IInteractable
{
    Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    public void Interaction()
    {
        SetRandomColor();
    }

    void SetRandomColor()
    {
        var r = Random.value;
        var g = Random.value;
        var b = Random.value;

        material.color = new Color(r, g, b);
    }
}

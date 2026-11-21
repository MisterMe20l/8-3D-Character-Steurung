using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KickBox : MonoBehaviour, IInteractable
{
    [SerializeField] float kickForce;
    [SerializeField] float kickHeight;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
     
    public void Interaction()
    {
        KickBoxes();
    }

    void KickBoxes()
    {
        var kickDirection = new Vector3(kickForce, kickHeight, 0);
        kickDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * kickDirection;
        rb.AddForce(kickDirection);
    }
}

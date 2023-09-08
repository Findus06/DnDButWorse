using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] string postMessage;
    public string objectName;

    void Start()
    {
        objectName = transform.name;
    }


    public void Interact()
    {
        Debug.Log(postMessage);
    }
}

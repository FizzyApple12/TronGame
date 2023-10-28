using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    public Hand attachedHand;

    Rigidbody attachedRigidbody;

    public Vector3 velocity
    {
        get
        {
            return attachedRigidbody.velocity;
        }
    }

    public Vector3 angularVelocity
    {
        get
        {
            return attachedRigidbody.angularVelocity;
        }
    }


    void Start()
    {
        attachedRigidbody = GetComponent<Rigidbody>();

        RegisterHandFromGameObject(attachedHand.gameObject);
    }

    void Update()
    {
        
    }

    void RegisterHandFromGameObject(GameObject handObject)
    {
        this.attachedHand = handObject.GetComponent<Hand>();

        if (this.attachedHand is null)
            return;

        this.attachedHand.OnThrow += OnHandThrow;
    }

    void UnregisterHand()
    {
        if (this.attachedHand is null)
            return;

        this.attachedHand.OnThrow -= OnHandThrow;

        this.attachedHand = null;
    }

    void OnHandThrow()
    {
        UnregisterHand();

        ServerConnection.SendDiscThrow(this);
    }

    public void FollowRemoteData(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
    }
}

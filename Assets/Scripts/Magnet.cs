using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]

public class Magnet : MonoBehaviour
{
    #region Singleton class: Magnet
    public static Magnet Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField] float magnetForce = default;

    List<Rigidbody> affectedRigidBodies = new List<Rigidbody>();
    Transform magnet;

    void Start()
    {
        magnet = transform;
        affectedRigidBodies.Clear();
    }

    void FixedUpdate()
    {
        if(!Game.isGameOver && Game.isMoving)
        {
            foreach (Rigidbody rb in affectedRigidBodies)
            {
                rb.AddForce((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if(!Game.isGameOver && (tag.Equals("Object") || tag.Equals("Obstacle")))
        {
            AddtoMagnetField(other.attachedRigidbody);
        }
    }

    void OnTriggerExit(Collider other)
    {
        string tag = other.tag;
        if (!Game.isGameOver && (tag.Equals("Object") || tag.Equals("Obstacle")))
        {
            RemoveFromMagnetField(other.attachedRigidbody);
        }
    }

    public void AddtoMagnetField(Rigidbody rigidbody)
    {
        affectedRigidBodies.Add(rigidbody);
    }

    public void RemoveFromMagnetField(Rigidbody rigidbody)
    {
        affectedRigidBodies.Remove(rigidbody);
    }
}

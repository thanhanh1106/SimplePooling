using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleObjectPooling;

public class CubeDemoPool : MonoBehaviour, IPoolable
{
    public PoolMember MemberName => PoolMember.Cube;

    Rigidbody Rigidbody;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    public void ReturnToPool()
    {
        ObjectPooler.Instance.Push(MemberName, this.gameObject);
    }
    public void Fly()
    {
        Rigidbody.AddForce(Vector3.up * 20);
    }
}

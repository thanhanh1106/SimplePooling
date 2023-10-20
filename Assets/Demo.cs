using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleObjectPooling;

public class Demo : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CubeDemoPool cube = 
                ObjectPooler.Instance.Pull<CubeDemoPool>(PoolMember.Cube,new Vector3(0,5,0));
            cube.Fly();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}

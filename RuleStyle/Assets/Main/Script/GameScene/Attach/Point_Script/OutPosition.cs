using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��O
/// </summary>
public class OutPosition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}

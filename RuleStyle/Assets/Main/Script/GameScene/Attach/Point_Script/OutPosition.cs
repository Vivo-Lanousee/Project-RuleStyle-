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
        other.gameObject.GetComponent<Player_Attach>()._playerData.TurnEnd();
        Destroy(other.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallScript : MonoBehaviour
{
    #region Variables
    #endregion

    #region Properties
    #endregion

    #region BuildIn Methods
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = other.GetComponent<PlayerMovements>().OriginalPosition;
            other.GetComponent<CharacterController>().enabled = true;
        }
    }
    #endregion

    #region Customs Methods
    #endregion
}

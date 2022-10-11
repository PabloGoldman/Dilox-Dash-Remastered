using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.levelToInstantiate.levelGameObject)
        {
            Instantiate(GameManager.instance.levelToInstantiate.levelGameObject);
        }
        else
        {
            //Hacer q instancie un nivel default
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelSO[] levels;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(levels[0].levelGameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

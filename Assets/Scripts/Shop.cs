using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    GameObject itemTemplate;
    [SerializeField] Transform shopScrollView;

    // Start is called before the first frame update
    void Start()
    {
        itemTemplate = shopScrollView.GetChild(0).gameObject;

        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(itemTemplate, shopScrollView);
        }

        Destroy(itemTemplate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

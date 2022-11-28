using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneToPlugin : MonoBehaviour
{
    public void ChangeToSpecificScene(int id)
    {
        SceneManager.LoadScene(6);
    }
}

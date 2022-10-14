using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject endGamePanel;
    // Start is called before the first frame update
    void Start()
    {
        endGamePanel.SetActive(false);
    }

    public void AbleEndGameScreen()
    {
        endGamePanel.SetActive(true);
    }

    public void ChangeToProfile()
    {
        GameManager.instance.ChangeToProfile();
    }
}

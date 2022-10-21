using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    #region Singleton
    public static Profile instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public class Avatar
    {
        public Sprite image;
    }

    public List<Avatar> avatarsList;

    [SerializeField] GameObject avatarUITemplate;
    [SerializeField] Transform avatarsScrollView;

    [SerializeField] Color activeAvatarColor;
    [SerializeField] Color defaultAvatarColor;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip selectAvatarAudioClip;

    GameObject g;

    int newSelectedIndex, previousSelectedIndex;

    bool firstLoad = true;

    // Start is called before the first frame update
    void Start()
    {
        GetAvailableAvatars();

        newSelectedIndex = previousSelectedIndex = 0;
    }

    private void GetAvailableAvatars()
    {
        for (int i = 0; i < Shop.instance.shopItemsList.Count; i++)
        {
            if (Shop.instance.shopItemsList[i].isPurchased)
            {
                AddAvatar(Shop.instance.shopItemsList[i].image);
            }
        }

        SelectAvatar(newSelectedIndex);  //Hay que tocar algo aca para no reiniciar el avatar cada vez q arranca la partida
        firstLoad = false;
    }

    public void ChangeToLevelSelection()
    {
        GameManager.instance.ChangeToLevelSelection();
    }

    public void AddAvatar(Sprite img)
    {
        if (avatarsList == null)
        {
            avatarsList = new List<Avatar>();
        }
        Avatar av = new Avatar() { image = img };
        avatarsList.Add(av);

        g = Instantiate(avatarUITemplate, avatarsScrollView);
        g.transform.GetComponent<Image>().sprite = av.image;

        g.transform.GetComponent<Button>().AddEventListener(avatarsList.Count - 1, OnAvatarClick);
        g.transform.GetComponent<Image>().color = defaultAvatarColor;
    }

    void OnAvatarClick(int avatarIndex)
    {
        SelectAvatar(avatarIndex);
    }

    void SelectAvatar(int avatarIndex)
    {
        if (!firstLoad)
        {
            source.PlayOneShot(selectAvatarAudioClip);
        }
        previousSelectedIndex = newSelectedIndex;
        newSelectedIndex = avatarIndex;
        avatarsScrollView.GetChild(previousSelectedIndex).GetComponent<Image>().color = defaultAvatarColor;
        avatarsScrollView.GetChild(newSelectedIndex).GetComponent<Image>().color = activeAvatarColor;

        GameManager.instance.SetPlayerAvatar(avatarsList[avatarIndex].image);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyHealthBarController : MonoBehaviour
{
    private GameObject[] heartContainers;
    private Image[] heartFills;

    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private void Start()
    {
        // Should I use lists? Maybe :)
        heartContainers = new GameObject[(int)MyPlayer.Instance.maxPlayerHealth];
        heartFills = new Image[(int)MyPlayer.Instance.maxPlayerHealth];

        MyPlayer.Instance.onPlayerHealthChangedCallback += UpdateHeartsHUD;
        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }

    void SetHeartContainers()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < MyPlayer.Instance.shipHealth)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < MyPlayer.Instance.playerHealth)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }

        if (MyPlayer.Instance.playerHealth % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(MyPlayer.Instance.playerHealth);
            heartFills[lastPos].fillAmount = MyPlayer.Instance.playerHealth % 1;
        }
    }

    void InstantiateHeartContainers()
    {
        for (int i = 0; i < MyPlayer.Instance.maxPlayerHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconRandomizer : MonoBehaviour
{
    public List<Sprite> iconList;

    public void ChooseRandomIcon()
    {
        int randomIndex = Random.Range(0, iconList.Count);
        transform.GetChild(0).GetComponent<Image>().sprite = iconList[randomIndex];
    }

    public int GetSpriteIdx()
    {
        return iconList.IndexOf(transform.GetChild(0).GetComponent<Image>().sprite);
    }

    public void SetSpriteByIdx(int idx)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = iconList[idx];
    }
}

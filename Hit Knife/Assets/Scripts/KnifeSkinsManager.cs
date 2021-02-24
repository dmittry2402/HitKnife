using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSkinsManager : MonoBehaviour
{
    KnifeSkinsManager Instance;
    public List<GameObject> Skins;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public GameObject GetSkin(int i)
    {
        return Skins[i];
    }
    public Sprite GetSkinImage(int i)
    {
        return Skins[i].GetComponent<SpriteRenderer>().sprite;
    }
}

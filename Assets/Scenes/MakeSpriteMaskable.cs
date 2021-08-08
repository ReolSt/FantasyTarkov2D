using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSpriteMaskable : MonoBehaviour
{
    private SPUM_SpriteList spriteList;

    private void Start()
    {
        this.spriteList = gameObject.GetComponent<SPUM_SpriteList>();

        List<SpriteRenderer>[] spriteRendererLists =
        {
            this.spriteList._itemList,
            this.spriteList._eyeList,
            this.spriteList._hairList,
            this.spriteList._bodyList,
            this.spriteList._clothList,
            this.spriteList._armorList,
            this.spriteList._pantList,
            this.spriteList._weaponList,
            this.spriteList._backList
        };

        foreach(List<SpriteRenderer> spriteRendererList in spriteRendererLists)
        {
            foreach(SpriteRenderer spriteRenderer in spriteRendererList)
            {
                spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
        }
    }

    private void Update()
    {

    }
}

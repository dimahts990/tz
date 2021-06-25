using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class Cell : MonoBehaviour,IPointerClickHandler
{
   public UnityEvent HitEventOnCreateBeautifully;
   public UnityEvent HitEventOnCreateUgly;
   public UnityEvent HitEventIfCorrect;
   public UnityEvent HitEventIfWrong;
   
   private AssetValue assetLetter;
   private GameSystem gameSystem;

   private void Start()
   {
      gameSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSystem>();
   }
   
   public void OnPointerClick(PointerEventData eventData)
   {
      if (assetLetter.value == theRightLetter.RightLetterString)
      {
         HitEventIfCorrect.Invoke();
      }
      else
      {
         HitEventIfWrong.Invoke();
      }
   }

   public void SetAssetLetter(AssetValue _asset)
   {
      assetLetter = _asset;
      GetComponent<Image>().sprite = _asset.icon;
      transform.localScale = _asset.Scale;
      transform.rotation=Quaternion.Euler(_asset.Rotation);
   }

   public void SetScale()
   {
      transform.localScale = assetLetter.Scale;
   }
   
   public void NextLevel()
   {
      gameSystem.NextLevel();
   }
}

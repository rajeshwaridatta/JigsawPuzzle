using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private RectTransform rectTransform;
    private PuzzlePiece piece;

    private Vector2 dragOffset; 

    public static event Action<PuzzlePiece> OnPiecePlacedCorrectly;

    void Awake()
    {
       
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        piece = this.GetComponent<PuzzlePiece>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        piece.SetVFXMat();

    }
    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
        this.rectTransform,
        eventData.position,
        eventData.pressEventCamera,
        out Vector3 worldMousePosition
    );

        this.rectTransform.position = worldMousePosition;
       


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 snapPosition = this.GetComponent<PuzzlePiece>().correctSnapPosition;
       if(piece.IsOverlapping())
        {
            piece.SetVFXMat();
            piece.PlayScaleAnim();
            
            OnPiecePlacedCorrectly.Invoke(piece);
            StartCoroutine(RemovePiece());
        }
        else
        {
            transform.position = originalPosition; 
           
        }
    }
    private IEnumerator RemovePiece()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
  
}

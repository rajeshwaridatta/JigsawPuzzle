using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;// To store the original position of the piece
    private RectTransform rectTransform;


    private Vector2 dragOffset; // Offset for dragging
    public int pieceIndex; // Index of the puzzle piece to track its original position

    private bool isPlacedCorrectly = false; // Flag to track if piece is placed correctly

    void Awake()
    {
        // Make sure CanvasGroup is attached to the PuzzlePiece
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Store the original position when dragging starts
        originalPosition = transform.position;
       // dragOffset = transform.position - Camera.main.WorldToScreenPoint(eventData.position); // Calculate the drag offset
    }
    public void OnDrag(PointerEventData eventData)
    {

        RectTransformUtility.ScreenPointToWorldPointInRectangle(
        this.rectTransform,
        eventData.position,
        eventData.pressEventCamera,
        out Vector3 worldMousePosition
    );

        // Set image position
        this.rectTransform.position = worldMousePosition;


        //Vector3 mousePos = eventData.position;
        //this.rectTransform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Logic for when drag ends
        Vector2 snapPosition = this.GetComponent<PuzzlePiece>().correctSnapPosition;
        float dist = Vector2.Distance((Vector2)transform.position, snapPosition);
        if (Vector2.Distance((Vector2)transform.localPosition, snapPosition) < 150f ) // You can tweak the distance tolerance here
        {
            transform.position = new Vector3(snapPosition.x, snapPosition.y, 0f);
          
            isPlacedCorrectly = true;
            PuzzleBoard.instance.topImageGrid.maskHolder.transform.GetChild(this.GetComponent<PuzzlePiece>().index).gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            transform.position = originalPosition; // Return to the original position if not snapped
            isPlacedCorrectly = false;
        }
    }
    public bool IsCorrectlyPlaced()
    {
        return this.GetComponent<PuzzlePiece>().IsCorrectlyPlaced();
    }
}

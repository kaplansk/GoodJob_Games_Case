using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    public int colorID;
    private SpriteRenderer spriteRenderer;
    private CollapseManager collapseManager;
    public bool isBeingDestroyed = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collapseManager = FindFirstObjectByType<CollapseManager>();
    }

    private void Start()
    {
        
        UpdateIcon(1);
    }

    private void OnMouseDown()
    {
        if (isBeingDestroyed) return;
        collapseManager.HandleBlockClick(this);
    }

    public void UpdateIcon(int groupSize)
    {
        BlockColorData colorData = BlockColorManager.Instance.GetBlockColorData(colorID);

      

        if (groupSize > CollapseManager.conditionC)
            spriteRenderer.sprite = colorData.thirdSprite;
        else if (groupSize > CollapseManager.conditionB)
            spriteRenderer.sprite = colorData.secondSprite;
        else if (groupSize > CollapseManager.conditionA)
            spriteRenderer.sprite = colorData.firstSprite;
        else
            spriteRenderer.sprite = colorData.defaultSprite;
    }



    public IEnumerator DestroyBlockSmoothly()
    {
        isBeingDestroyed = true;
        float duration = 0.3f;
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
    public void SetSortingOrder(int totalRows, int row)
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = totalRows - row;
    }

}

using UnityEngine;
using UnityEngine.UI;

public class FlexibleLayoutGroup : LayoutGroup
{
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        // Hitung jumlah anak yang aktif
        int activeChildCount = 0;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            if (rectChildren[i].gameObject.activeSelf) activeChildCount++;
        }

        if (activeChildCount == 0) return;

        // Logika otomatis pembagian baris & kolom berdasarkan jumlah item
        float sqrRt = Mathf.Sqrt(activeChildCount);
        rows = Mathf.CeilToInt(sqrRt);
        columns = Mathf.CeilToInt(sqrRt);

        // Hitung ruang yang tersedia di layar
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        // Bagi rata ukuran cell agar pas dengan layar
        float cellWidth = (parentWidth - (spacing.x * (columns - 1)) - padding.left - padding.right) / columns;
        float cellHeight = (parentHeight - (spacing.y * (rows - 1)) - padding.top - padding.bottom) / rows;

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        // Atur posisi setiap objek anak
        for (int i = 0; i < rectChildren.Count; i++)
        {
            int rowCount = i / columns;
            int columnCount = i % columns;

            var item = rectChildren[i];

            float xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            float yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical() { }
    public override void SetLayoutHorizontal() { }
    public override void SetLayoutVertical() { }
}
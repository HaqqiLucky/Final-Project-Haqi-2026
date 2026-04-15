using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FlexibleLayoutGrid : LayoutGroup
{

    private enum FitType
    {
        Uniform,
        Width,
        Height 
    }


    [SerializeField]private FitType fittypex;
    [SerializeField]private int rows;
    [SerializeField] private int columns;
    [SerializeField] private Vector2 cellSize;
    [SerializeField] private Vector2 Spacing;
    public override void CalculateLayoutInputHorizontal()  // default call in layout group is this
    {
        base.CalculateLayoutInputHorizontal();

        // cari berapa kolom sama baris yang ada dengan nyari square root dri kolom dan row
        float sqRt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqRt);
        columns = Mathf.CeilToInt(sqRt);

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float)columns - ((Spacing.x / (float) columns) *2) - (padding.left / (float) columns) - (padding.right / (float) columns);
        float cellHeight = parentHeight / (float)rows - ((Spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows); 

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];
            var xPos = (cellSize.x * columnCount) + (Spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (Spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }

    }

    public override void CalculateLayoutInputVertical()
    {
        throw new System.NotImplementedException();
    }

    public override void SetLayoutHorizontal()
    {
        throw new System.NotImplementedException();
    }

    public override void SetLayoutVertical()
    {
        throw new System.NotImplementedException();
    }

}

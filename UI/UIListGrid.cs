using Terraria.GameContent.UI.Elements;

namespace ExxoAvalonOrigins.UI
{
    public class UIListGrid : UIList
    {
        private float innerListHeight;
        private float innerListWidth;
        // A bit iffy but I want to have text not be cut off, vanilla should have made margins / padding not effect bounds of self but rather children
        public float MarginX = 0f;
        public float MarginY = 0f;

        public override void RecalculateChildren()
        {
            base.RecalculateChildren();

            float currentX = MarginX;
            float currentY = MarginY;
            bool expandedY = false;

            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].Top.Set(currentY, 0f);
                _items[i].Left.Set(currentX, 0f);
                _items[i].Recalculate();
                currentX += _items[i].GetOuterDimensions().Width + ListPadding;
                if (currentX + _items[i].GetOuterDimensions().Width > base.Parent.GetInnerDimensions().Width)
                {
                    expandedY = true;
                    innerListWidth = currentX - ListPadding;
                    currentX = 0;
                    currentY += _items[i].GetOuterDimensions().Height + ListPadding;
                }
            }

            if (!expandedY)
            {
                innerListWidth = currentX - ListPadding;
            }

            innerListHeight = currentY + _items[0].GetOuterDimensions().Height;
        }

        public new float GetTotalHeight()
        {
            return innerListHeight;
        }

        public float GetTotalWidth()
        {
            return innerListWidth;
        }
    }
}
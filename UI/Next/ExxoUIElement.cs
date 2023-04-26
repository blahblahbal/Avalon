using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalon.UI.Next.Enums;
using Avalon.UI.Next.Events;
using Avalon.UI.Next.Structs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.Next;

public class ExxoUIElement : INotifyPropertyChanged {
    public Point Position {
        get => position;
        set => SetField(ref position, value);
    }

    public UIDimension Width {
        get => width;
        set => SetField(ref width, value);
    }

    public UIDimension Height {
        get => height;
        set => SetField(ref height, value);
    }

    public EdgeInsets Padding {
        get => padding;
        set => SetField(ref padding, value);
    }

    public EdgeInsets Margin {
        get => margin;
        set => SetField(ref margin, value);
    }

    public UIDimension Gap {
        get => gap;
        set => SetField(ref gap, value);
    }

    public Enums.DisplayMode DisplayMode {
        get => displayMode;
        set => SetField(ref displayMode, value);
    }

    public PositionMode PositionMode {
        get => positionMode;
        set => SetField(ref positionMode, value);
    }

    public FlexDirection FlexDirection {
        get => flexDirection;
        set => SetField(ref flexDirection, value);
    }

    public int GridCols {
        get => gridCols;
        set => SetField(ref gridCols, value);
    }

    public SnapNode? SnapNode {
        get => snapNode;
        set => SetField(ref snapNode, value);
    }

    public Rectangle OuterBounds { get; private set; }
    public Rectangle Bounds { get; private set; }
    public Rectangle InnerBounds { get; private set; }
    public ExxoUIElement? Parent { get; private set; }
    public bool Outdated { get; set; }

    public virtual bool ContainsPoint(Point point) {
        return InnerBounds.Contains(point);
    }

    public readonly ObservableCollection<ExxoUIElement> Children = new();

    private Point position;
    private UIDimension width;
    private UIDimension height;
    private EdgeInsets padding;
    private EdgeInsets margin;
    private Enums.DisplayMode displayMode;
    private FlexDirection flexDirection;
    private int gridCols;
    private SnapNode? snapNode;
    private PositionMode positionMode;
    private UIDimension gap;

    public ExxoUIElement() {
        Children.CollectionChanged += (_, args) => {
            if (args.OldItems != null) {
                foreach (ExxoUIElement item in args.OldItems) {
                    item.Parent = null;
                }
            }

            if (args.NewItems != null) {
                foreach (ExxoUIElement item in args.NewItems) {
                    item.Parent = this;
                }
            }

            Outdated = true;
        };
        PropertyChanged += (_, args) => {
            if (args.PropertyName != nameof(SnapNode)) {
                Outdated = true;
            }
        };
        Outdated = true;
    }

    public void Detach() {
        Parent?.Children.Remove(this);
    }

    public void Recalculate() {
        if (Outdated) {
            RecalculateSelf();
            PositionChildren();
        }

        foreach (ExxoUIElement child in Children) {
            child.Outdated = child.Outdated || Outdated;
            child.Recalculate();
        }

        Outdated = false;
        PostRecalculate();
    }

    protected virtual void PostRecalculate() {
    }

    protected virtual void PositionChildren() {
        switch (DisplayMode) {
            case Enums.DisplayMode.Flex:
                PositionChildrenByFlex();
                break;
            case Enums.DisplayMode.Grid:
                PositionChildrenByGrid();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PositionChildrenByFlex() {
        int offset = 0;
        int greatestWidth = 0;
        int greatestHeight = 0;

        int flexGap = Gap.Unit switch {
            ScreenUnit.Auto => (int)Gap.Value,
            ScreenUnit.Percent => (FlexDirection is FlexDirection.Column or FlexDirection.ColumnReverse
                ? InnerBounds.Height
                : InnerBounds.Width) * (int)Gap.Value,
            ScreenUnit.Pixels => (int)Gap.Value,
            _ => throw new ArgumentOutOfRangeException(),
        };

        foreach (ExxoUIElement child in Children) {
            if (child.PositionMode != PositionMode.Relative) {
                continue;
            }

            if (child.Outdated) {
                child.Recalculate();
            }

            child.Position = FlexDirection switch {
                FlexDirection.Column => new Point(0, offset),
                FlexDirection.Row => new Point(offset, 0),
                FlexDirection.ColumnReverse => new Point(0, InnerBounds.Height - offset - child.OuterBounds.Height),
                FlexDirection.RowReverse => new Point(0, InnerBounds.Width - offset - child.OuterBounds.Width),
                _ => throw new ArgumentOutOfRangeException(),
            };

            offset += FlexDirection switch {
                FlexDirection.Column => child.OuterBounds.Height,
                FlexDirection.Row => child.OuterBounds.Width,
                FlexDirection.ColumnReverse => child.OuterBounds.Height,
                FlexDirection.RowReverse => child.OuterBounds.Width,
                _ => throw new ArgumentOutOfRangeException(),
            };

            offset += flexGap;

            if (child.OuterBounds.Width > greatestWidth) {
                greatestWidth = child.OuterBounds.Width;
            }

            if (child.OuterBounds.Height > greatestHeight) {
                greatestHeight = child.OuterBounds.Height;
            }
        }

        bool needsRecalculation = false;
        if (Width.Unit == ScreenUnit.Auto) {
            if (FlexDirection is FlexDirection.Column or FlexDirection.ColumnReverse) {
                Width = new UIDimension(greatestWidth, ScreenUnit.Auto);
            }
            else {
                Width = new UIDimension(offset, ScreenUnit.Auto);
            }

            needsRecalculation = true;
        }

        if (Height.Unit == ScreenUnit.Auto) {
            if (FlexDirection is FlexDirection.Column or FlexDirection.ColumnReverse) {
                Height = new UIDimension(offset, ScreenUnit.Auto);
            }
            else {
                Height = new UIDimension(greatestHeight, ScreenUnit.Auto);
            }

            needsRecalculation = true;
        }

        if (needsRecalculation) {
            RecalculateSelf();
        }
    }

    private void PositionChildrenByGrid() {
        var offset = new Point(0, 0);
        int rowHeight = 0;
        int greatestWidth = 0;

        int gridGap = Gap.Unit switch {
            ScreenUnit.Auto => (int)Gap.Value,
            ScreenUnit.Percent => InnerBounds.Width * (int)Gap.Value,
            ScreenUnit.Pixels => (int)Gap.Value,
            _ => throw new ArgumentOutOfRangeException(),
        };


        foreach (ExxoUIElement child in Children) {
            if (child.PositionMode != PositionMode.Relative) {
                continue;
            }

            if (gridCols > 0) {
                child.Width = new UIDimension((InnerBounds.Width - gridGap * (gridCols - 1)) / (float)gridCols,
                    ScreenUnit.Pixels);
            }

            if (child.Outdated) {
                child.Recalculate();
            }

            if (offset.X + child.OuterBounds.Width > InnerBounds.Width) {
                offset = new Point(0, offset.Y + rowHeight + gridGap);
                rowHeight = 0;
            }

            child.Position = offset;
            offset = new Point(offset.X + child.OuterBounds.Width + gridGap, offset.Y);

            if (child.OuterBounds.Height > rowHeight) {
                rowHeight = child.OuterBounds.Height;
            }

            if (offset.X + child.OuterBounds.Width > greatestWidth) {
                greatestWidth = offset.X + child.OuterBounds.Width;
            }
        }

        bool needsRecalculation = false;
        if (Width.Unit == ScreenUnit.Auto) {
            Width = new UIDimension(offset.X + greatestWidth, ScreenUnit.Auto);
            needsRecalculation = true;
        }

        if (Height.Unit == ScreenUnit.Auto) {
            Height = new UIDimension(offset.Y + rowHeight, ScreenUnit.Auto);
            needsRecalculation = true;
        }

        if (needsRecalculation) {
            RecalculateSelf();
        }
    }

    protected virtual void RecalculateSelf() {
        Rectangle parentInnerBounds = Parent?.InnerBounds ?? new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);

        float widthPixels = Width.Unit switch {
            ScreenUnit.Auto => Width.Value,
            ScreenUnit.Pixels => Width.Value,
            ScreenUnit.Percent => parentInnerBounds.Width * Width.Value * 0.01f,
            _ => throw new ArgumentOutOfRangeException(),
        };

        float heightPixels = Height.Unit switch {
            ScreenUnit.Auto => Height.Value,
            ScreenUnit.Pixels => Height.Value,
            ScreenUnit.Percent => parentInnerBounds.Height * Height.Value * 0.01f,
            _ => throw new ArgumentOutOfRangeException(),
        };

        Bounds = new Rectangle(parentInnerBounds.X + Position.X, parentInnerBounds.Y + Position.Y,
            (int)(widthPixels + 0.5f),
            (int)(heightPixels + 0.5f));
        OuterBounds = new Rectangle(Bounds.X - Margin.Left, Bounds.Y - Margin.Top, Bounds.Width + Margin.Right,
            Bounds.Height + Margin.Bottom);
        InnerBounds = new Rectangle(Bounds.X + Padding.Left, Bounds.Y + Padding.Top, Bounds.Width - Padding.Right,
            Bounds.Height - Padding.Bottom);
    }

    public void Update(GameTime gameTime) {
        UpdateSelf(gameTime);
        foreach (ExxoUIElement child in Children) {
            child.Update(gameTime);
        }
    }

    protected virtual void UpdateSelf(GameTime gameTime) {
    }

    public void Draw(SpriteBatch spriteBatch) {
        DrawSelf(spriteBatch);
        foreach (ExxoUIElement child in Children) {
            child.Draw(spriteBatch);
        }
    }

    protected virtual void DrawSelf(SpriteBatch spriteBatch) {
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public event InputEventHandler? OnInputEvent;

    public virtual void InputEvent(InputEventArgs e) {
        OnInputEvent?.Invoke(this, e);
        Parent?.InputEvent(e);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value)) {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public virtual List<SnapPoint> GetSnapPoints() {
        var snapPoints = new List<SnapPoint>();
        if (SnapNode != null) {
            snapPoints.Add(SnapNode.ToSnapPoint(this));
        }

        foreach (ExxoUIElement child in Children) {
            snapPoints.AddRange(child.GetSnapPoints());
        }

        return snapPoints;
    }

    public ExxoUIElement? GetElementAt(Point point) {
        return Children.FirstOrDefault(child => child.ContainsPoint(point))?.GetElementAt(point) ??
               (ContainsPoint(point) ? this : null);
    }
}

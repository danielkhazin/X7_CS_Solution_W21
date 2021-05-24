using System;

public class Rectangle
{
    private int x;
    private int y;
    private int width;
    private int height;

    public int left { get; private set; }
    public int right { get; private set; }
    public int top { get; private set; }
    public int bottom { get; private set; }
    public Point centre { get; private set; }

    public int X
    {
        get { return x; }
        set
        {
            this.x = value;
            this.left = x;
            this.right = x + width;
            this.centre = FindCentre();
        }
    }

    public int Y
    {
        get { return y; }
        set
        {
            this.y = value;
            this.top = y;
            this.bottom = y + height;
            this.centre = FindCentre();
        }
    }

    public int Width
    {
        get { return width; }
        set
        {
            this.width = value;
            this.right = x + width;
            this.centre = FindCentre();
        }
    }

    public int Height
    {
        get { return height; }
        set
        {
            this.height = value;
            this.bottom = y + height;
            this.centre = FindCentre();
        }
    }

    public Rectangle(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;

        this.left = x;
        this.right = x + width;
        this.top = y;
        this.bottom = y + height;
        this.centre = FindCentre();
    }

    private Point FindCentre()
    {
        return new Point((left + right) / 2, (top + bottom) / 2);
    }

    public bool Intersects(Rectangle r2)
    {
        return Intersects(this, r2);
    }

    public static bool Intersects(Rectangle r1, Rectangle r2)
    {
        return !(r1.right < r2.left || r1.left > r2.right ||
                 r1.bottom < r2.top || r1.top > r2.bottom);
    }

    public Rectangle Intersection(Rectangle r2)
    {
        return Intersection(this, r2);
    }

    public static Rectangle Intersection(Rectangle r1, Rectangle r2)
    {
        if (!Intersects(r1, r2)) return null;

        int left = (int)Math.Max(r1.left, r2.left);
        int right = (int)Math.Min(r1.right, r2.right);
        int top = (int)Math.Max(r1.top, r2.top);
        int bottom = (int)(Math.Min(r1.bottom, r2.bottom));

        return new Rectangle(left, top, right - left, bottom - top);
    }

    public bool Intersects(int x, int y)
    {
        return Intersects(this, x, y);
    }

    public static bool Intersects(Rectangle r, int x, int y)
    {
        return x >= r.left && x <= r.right && y >= r.top && y <= r.bottom;
    }
}
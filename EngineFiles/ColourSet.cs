using System;

public class ColourSet
{
    private ConsoleColor fgCol;
    private ConsoleColor bgCol;

    public ColourSet(ConsoleColor bgCol, ConsoleColor fgCol)
    {
        SetFG(fgCol);
        SetBG(bgCol);
    }

    public ColourSet()
    {
        bgCol = Helper.bgCol;
        fgCol = Helper.fgCol;
    }

    public ConsoleColor GetFG()
    {
        return fgCol;
    }

    public ConsoleColor GetBG()
    {
        return bgCol;
    }

    public void SetFG(ConsoleColor fgCol)
    {
        this.fgCol = fgCol;
    }

    public void SetBG(ConsoleColor bgCol)
    {
        this.bgCol = bgCol;
    }

    public bool IsDefault()
    {
        return bgCol == Helper.bgCol && fgCol == Helper.fgCol;
    }

    public bool IsTransparent()
    {
        return bgCol == Helper.bgCol && fgCol == Helper.BLACK;
    }

    public bool Equals(ColourSet set2)
    {
        return this.fgCol == set2.fgCol && this.bgCol == set2.bgCol;
    }

    public bool Equals(ConsoleColor bgCol, ConsoleColor fgCol)
    {
        return this.fgCol == fgCol && this.bgCol == bgCol;
    }

    public ColourSet Copy()
    {
        return new ColourSet(bgCol, fgCol);
    }
}
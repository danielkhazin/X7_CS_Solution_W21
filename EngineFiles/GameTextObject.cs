using System;

class GameTextObject : GameObject
{
    private string text = "";
    protected ConsoleColor colour;

    public GameTextObject(GameContainer gc, int x, int y, ConsoleColor colour, bool isVisible, string text) : base(gc, ' ', x, y, colour, Helper.bgCol, isVisible)
    {
        this.text = text;
        TruncateText();

        this.colour = colour;

        SetText();
        SetPosition(x,y);

        height = 1;
    }

    private void SetText()
    {
        //Set up grid to handle character and colour text
        grid = new char[1, text.Length];
        colourGrid = new ColourSet[1, text.Length];

        for (int i = 0; i < width; i++)
        {
            grid[0, i] = text[i];
            colourGrid[0, i] = new ColourSet();
        }
        SetColours();
    }

    private void SetColours()
    {
        for (int i = 0; i < width; i++)
        {
            colourGrid[0, i].SetFG(colour);
        }
    }

    private void TruncateText()
    {
        int spaceToWall = gc.GetGameWidth() - pos.x;

        if (text.Length > spaceToWall)
        {
            text = text.Substring(0, spaceToWall);
        }

        width = text.Length;
    }

    /**
     * <b><i>UpdateText</b></i>
     * <p>
     * {@code public void UpdateText(string text)}
     * <p>
     * Update the display text of this object
     * 
     * @param text The new text to be displayed
     */
    public void UpdateText(string text)
    {
        this.text = text;
        TruncateText();

        pos = ClampToScreen(pos.x, pos.y);

        SetText();
    }

    /**
     * <b><i>SetColour</b></i>
     * <p>
     * {@code public void SetColour(string colour)}
     * <p>
     * Set the colour of the display text
     * 
     * @param colour The new colour of the display text
     */
    public void SetColour(ConsoleColor colour)
    {
        this.colour = colour;
        SetColours();
    }
}


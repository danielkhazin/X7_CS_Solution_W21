//Author: Trevor Lane
//File Name: UIBarObject.cs
//Project Name: CSGameEngine
//Creation Date: Nov 18, 2020
//Modified Date: Nov. 23, 2020
//Description:  An object maintaining coloured progress bars for various uses

using System;

public class UIBarObject : UIObject
{
    private double percent;
    private int max;
    private int value;
    private string output = "";
    private int fillCount = 0;

    /**
    * <b><i>UIBarObject</b></i>
    * <p>
    * {@code public UIBarObject(GameContainer gc, int x, int y, string colour, bool isVisible, int maxValue, int startValue, int barWidth)}<br>
    * <p>
    * Create a coloured progress bar.
    * 
    * @param gc   The connection to the game loop driver class
    * @param x  The x component of the object's position where (1,1) is the top left corner
    * @param y  The y component of the object's position where (1,1) is the top left corner
    * @param colour  The colour of the progress bar object (See Helper class for colour options)
    * @param isVisible  The visibilty status of the game object
    * @param maxValue  The value that represents a full progress bar, e.g. a player's max health
    * @param startValue  The value the progress bar begins with
    * @param barWidth  The screen width used by the progress bar, including 2 units for edge borders (Min: 3)
    */
    public UIBarObject(GameContainer gc, int x, int y, ConsoleColor colour, bool isVisible, int maxValue, int startValue, int barWidth) : base(gc, x, y, colour, isVisible)
    {
        this.max = Math.Max(1, maxValue);
        this.value = Helper.Clamp(startValue, 0, maxValue);

        width = Helper.Clamp(barWidth, 3, gc.GetUIWidth() - x - 1);
        height = 1;

        for (int i = 0; i < width; i++)
        {
            output += " ";
        }

        this.percent = CalcPercentage();
        fillCount = CalcFillCount();

        //Set up grid to handle character text
        grid = new char[1, width];
        colourGrid = new ColourSet[1, width];

        //Set end points
        grid[0, 0] = ' ';
        colourGrid[0, 0] = new ColourSet(Helper.WHITE, Helper.fgCol);
        grid[0, width - 1] = ' ';
        colourGrid[0, width - 1] = new ColourSet(Helper.WHITE, Helper.fgCol);
        for (int i = 1; i < width - 1; i++)
        {
            grid[0, i] = ' ';
            colourGrid[0, i] = new ColourSet(Helper.DARK_BLUE, Helper.fgCol);
        }
        SetColours();
    }

    private double CalcPercentage()
    {
        return (double)value / (double)max;
    }

    private int CalcFillCount()
    {
        return (int)Math.Ceiling((width - BORDER_WIDTH) * percent);
    }

    private void SetColours()
    {
        int count = 0;
        for (int i = 1; i < width - 1; i++)
        {
            if (count < fillCount)
            {
                colourGrid[0, i].SetBG(colour);
            }
            else
            {
                colourGrid[0, i].SetBG(Helper.DARK_BLUE);
            }
            count++;
        }
    }

    /**
     * <b><i>GetValue</b></i>
     * <p>
     * {@code public int GetValue()}
     * <p>
     * Retrieve the current value of the progress bar
     * 
     * @return The value of the progress bar
     */
    public int GetValue()
    {
        return value;
    }

    /**
     * <b><i>GetMax</b></i>
     * <p>
     * {@code public int GetMax()}
     * <p>
     * Retrieve the maximum value of the progress bar
     * 
     * @return The maximum value of the progress bar
     */
    public int GetMax()
    {
        return max;
    }

    /**
     * <b><i>GetPercentage</b></i>
     * <p>
     * {@code public int GetPercentage()}
     * <p>
     * Retrieve the current fill percentage of the progress bar
     * 
     * @return The fill percentage of the progress bar
     */
    public int GetPercentage()
    {
        return (int)(percent * 100.0);
    }

    /**
     * <b><i>SetColour</b></i>
     * <p>
     * {@code public void SetColour(string colour)}
     * <p>
     * Set the colour of the filled portion of the progress bar
     * 
     * @param colour The new colour of the progress bar
     */
    public override void SetColour(ConsoleColor colour)
    {
        this.colour = colour;
        SetColours();
    }

    /**
     * <b><i>SetValue</b></i>
     * <p>
     * {@code public void SetValue(int value)}
     * <p>
     * Set the value of the progress bar and recalculate its fill percentage
     * 
     * @param value The new value of the progress bar
     */
    public void SetValue(int value)
    {
        this.value = Helper.Clamp(value, 0, max);

        this.percent = CalcPercentage();
        fillCount = CalcFillCount();

        SetColours();
    }

    /**
     * <b><i>ChangeValue</b></i>
     * <p>
     * {@code public void ChangeValue(int changeAmount)}
     * <p>
     * Modify the value of the progress bar relative to its current setting
     * 
     * @param changeAmount The amount to change the progress bar's value by
     */
    public void ChangeValue(int changeAmount)
    {
        SetValue(value + changeAmount);
    }

    /**
    * <b><i>SetMax</b></i>
    * <p>
    * {@code public void SetMax(int max)}
    * <p>
    * Modify the maximum value of the progress bar
    * 
    * @param changeAmount The new maximum value possible
    */
    public void SetMax(int max)
    {
        this.max = Math.Max(1, max);
        SetValue(value);
    }

    /**
     * <b><i>IsEmpty</b></i>
     * <p>
     * {@code public bool IsEmpty()}
     * <p>
     * Check to see if the progress bar is empty
     * 
     * @return True if the value of the progress is zero, False otherwise
     */
    public bool IsEmpty()
    {
        return value == 0;
    }

    /**
     * <b><i>IsFull</b></i>
     * <p>
     * {@code public bool IsFull()}
     * <p>
     * Check to see if the progress bar is full (at max value)
     * 
     * @return True if the value of the progress is full, False otherwise
     */
    public bool IsFull()
    {
        return value == max;
    }
}
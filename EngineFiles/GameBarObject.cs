using System;

class GameBarObject : GameObject
{
    private const int MIN_SIZE = 3;
    private const int BORDER_WIDTH = 2;

    private double percent;
    private int max;
    private int value;
    private string output = "";
    private int fillCount = 0;
    private ConsoleColor colour;

    public GameBarObject(GameContainer gc, int x, int y, ConsoleColor colour, bool isVisible, int maxValue, int startValue, int barWidth) : base(gc, ' ', x, y, colour, colour, isVisible)
    {
        this.max = Math.Max(1,maxValue);
        this.value = Helper.Clamp(startValue, 0, maxValue);
        this.colour = colour;

        width = Helper.Clamp(barWidth, MIN_SIZE, gc.GetUIWidth() - x - 1);
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
    public void SetColour(ConsoleColor colour)
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

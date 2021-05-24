//Author: Trevor Lane
//File Name: UIObject.cs
//Project Name: CSharpGameEngine
//Creation Date: Nov 18, 2020
//Modified Date: Nov. 23, 2020
//Description:  This class represents a base class User Interface object that can be drawn to the screen

using System;

public class UIObject
{
    protected const int BORDER_WIDTH = 2;

    protected GameContainer gc;
    protected Point pos;
    protected bool isVisible = true;
    protected ConsoleColor colour;
    protected int width;
    protected int height;
    protected char[,] grid;
    protected ColourSet[,] colourGrid;

    /**
    * <b><i>UIObject</b></i>
    * <p>
    * {@code public UIObject(GameContainer gc, int x, int y, String colour, bool isVisible)}<br>
    * <p>
    * Create a user interface object to add to the game play
    * 
    * @param gc  The connection to the game loop driver class
    * @param x  The x component of the object's position where (1,1) is the top left corner
    * @param y  The y component of the object's position where (1,1) is the top left corner
    * @param colour  The colour of the game object (See Helper class for colour options)
    * @param isVisible  The visibilty status of the UI object
    */
    public UIObject(GameContainer gc, int x, int y, ConsoleColor colour, bool isVisible)
    {
        this.gc = gc;
        this.pos = new Point(x, y);
        this.isVisible = isVisible;
        this.colour = colour;
        this.width = 1;
        this.height = 1;

        pos = ClampToUI(pos);
    }

    /*
    protected int Clamp(int min, int max, int value)
    {
      return Math.min(max, Math.max(min,value));
    }
    */

    protected Point ClampToUI(int x, int y)
    {
        //Clamp game object within bounds
        x = Helper.Clamp(x, 0, gc.GetUIWidth() - width);
        y = Helper.Clamp(y, 0, gc.GetUIHeight() - height);

        return new Point(x, y);
    }

    protected Point ClampToUI(Point pt)
    {
        //Clamp game object within bounds
        pt.x = Helper.Clamp(pt.x, 0, gc.GetUIWidth() - width);
        pt.y = Helper.Clamp(pt.y, 0, gc.GetUIHeight() - height);

        return pt;
    }

    /**
     * <b><i>GetPos</b></i>
     * <p>
     * {@code public Point GetPos()}
     * <p>
     * Retrieve the on screen position of the object's top left corner
     * 
     * @return an (x,y) Point for the object's top left corner
     */
    public Point GetPos()
    {
        return pos;
    }

    /**
     * <b><i>ToggleVisibility</b></i>
     * <p>
     * {@code public void ToggleVisibility()}
     * <p>
     * Flip the visibility status of the object
     */
    public void ToggleVisibility()
    {
        isVisible = !isVisible;
    }

    /**
     * <b><i>GetVisibility</b></i>
     * <p>
     * {@code public bool GetVisibility()}
     * <p>
     * Retrieve the visibility status of the object
     * 
     * @return True if the object is visible, false otherwise
     */
    public bool GetVisibility()
    {
        return isVisible;
    }

    /**
     * <b><i>GetWidth</b></i>
     * <p>
     * {@code public int GetWidth()}
     * <p>
     * Retrieve the width of the object
     * 
     * @return The width of the object
     */
    public int GetWidth()
    {
        return width;
    }

    /**
     * <b><i>GetHeight</b></i>
     * <p>
     * {@code public int GetHeight()}
     * <p>
     * Retrieve the height of the object
     * 
     * @return The height of the object
     */
    public int GetHeight()
    {
        return height;
    }

    /**
     * <b><i>SetPosition</b></i>
     * <p>
     * {@code public void SetPosition(int x, int y)}
     * <p>
     * Set the position of the top left corner of the object
     * 
     * @param x The x component of the object's position
     * @param y The y component of the object's position
     */
    public void SetPosition(int x, int y)
    {
        pos = ClampToUI(x, y);
    }


    /**
     * <b><i>SetColour</b></i>
     * <p>
     * {@code public void SetColour(String colour)}
     * <p>
     * Set the colour of the object
     * 
     * @param colour The new colour to be used in object display
     */
    public virtual void SetColour(ConsoleColor colour)
    {
        this.colour = colour;
    }

    /**
     * <b><i>Move</b></i>
     * <p>
     * {@code public void Move(int deltaX, int deltaY)}
     * <p>
     * Move the object relative to its current position
     * 
     * @param deltaX The change in the x component, use 0 for no change
     * @param deltaY The change in the y component, use 0 for no change
     */
    public void Move(int deltaX, int deltaY)
    {
        pos.x += deltaX;
        pos.y += deltaY;

        pos = ClampToUI(pos);
    }

    /**
     * <b><i>GetGrid</b></i>
     * <p>
     * {@code public char[][] GetGrid()}
     * <p>
     * Retrieve the character data of the object in a 2D grid
     * 
     * @return The character data of the object
     */
    public char[,] GetGrid()
    {
        return grid;
    }

    /**
     * <b><i>GetColours</b></i>
     * <p>
     * {@code public String[][] GetColours()}
     * <p>
     * Retrieve the colour data of the object in a 2D grid
     * 
     * @return The colour data of the object
     */
    public ColourSet[,] GetColours()
    {
        return colourGrid;
    }

    public void SetVisibility(bool state)
    {
        isVisible = state;
    }
}
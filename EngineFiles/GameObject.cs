//Author: Trevor Lane
//File Name: GameObject.cs
//Project Name: CSGameEngine
//Creation Date: Nov 18, 2020
//Modified Date: Nov. 23, 2020
//Description:  This class represents a game play object that can be drawn and moved around

using System;

/**
* <h3>The core game element of all games</h3>
* <b>Creation Date:</b> Nov 18, 2020<br>
* <b>Modified Date:</b> Nov 23, 2020
* <p>
* 
* @author Trevor Lane
* @version 0.5
*/
public class GameObject
{
    protected GameContainer gc;
    protected char[,] grid;
    protected ColourSet[,] colourGrid;
    protected Point pos;
    protected Vector2F truePos;
    protected bool isVisible;
    protected int width;
    protected int height;

    /**
    * <b><i>GameObject</b></i>
    * <p>
    * {@code public GameObject(GameContainer gc, char ch, int x, int y, String colour, boolean isVisible)}<br>
    * <p>
    * Create a game object to add to the game play
    * 
    * @param gc   The connection to the game loop driver class
    * @param ch  The character that visually represents the game object
    * @param x  The x component of the object's position where (0,0) is the top left corner
    * @param y  The y component of the object's position where (0,0) is the top left corner
    * @param colour  The colour of the game object (See Helper class for colour options)
    * @param isVisible  The visibilty status of the game object
    */
    public GameObject(GameContainer gc, char ch, int x, int y, ConsoleColor textColour, ConsoleColor bgColour, bool isVisible)
    {
        this.gc = gc;

        //Clamp game object within bounds
        x = Helper.Clamp(x, 0, gc.GetGameWidth() - 1);
        y = Helper.Clamp(y, 0, gc.GetGameHeight() - 1);

        pos = new Point(x, y);
        truePos = new Vector2F(x, y);

        this.isVisible = isVisible;

        this.grid = new char[1, 1];
        this.colourGrid = new ColourSet[1, 1];

        this.grid[0, 0] = ch;
        this.colourGrid[0, 0] = new ColourSet(textColour, bgColour);

        this.width = 1;
        this.height = 1;
    }

    /**
    * <b><i>GameObject</b></i>
    * <p>
    * {@code public GameObject(GameContainer gc, char[][] grid, int x, int y, String[][] colourGrid, boolean isVisible)}<br>
    * <p>
    * Create a game object to add to the game play
    * 
    * @param gc   The connection to the game loop driver class
    * @param grid  The grid of characters representing the visuals of the game object
    * @param x  The x component of the object's position
    * @param y  The y component of the object's position
    * @param colourGrid  The grid of colours representing the visuals of the game object
    * @param isVisible  The visibilty status of the game object
    */
    public GameObject(GameContainer gc, char[,] grid, int x, int y, ColourSet[,] colourGrid, bool isVisible)
    {
        this.gc = gc;

        this.width = grid.GetLength(1);
        this.height = grid.GetLength(0);

        //Clamp game object within bounds
        x = Helper.Clamp(x, 0, gc.GetGameWidth() - width);
        y = Helper.Clamp(y, 0, gc.GetGameHeight() - height);

        pos = new Point(x, y);
        truePos = new Vector2F(x, y);

        this.isVisible = isVisible;

        this.grid = new char[height, width];
        this.colourGrid = new ColourSet[height, width];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                this.grid[row, col] = grid[row, col];
                this.colourGrid[row, col] = colourGrid[row, col];
            }
        }
    }

    /**
    * <b><i>GameObject</b></i>
    * <p>
    * {@code public GameObject(GameContainer gc, Image img, int x, int y, boolean isVisible)}<br>
    * <p>
    * Create a game object to add to the game play
    * 
    * @param gc   The connection to the game loop driver class
    * @param img  The Image object loaded from a .txt file storing the object's visuals
    * @param x  The x component of the object's position
    * @param y  The y component of the object's position
    * @param isVisible  The visibilty status of the game object
    */
    public GameObject(GameContainer gc, Image img, int x, int y, bool isVisible)
    {
        this.gc = gc;

        char[,] grid = img.GetGrid();
        ColourSet[,] colourGrid = img.GetColourGridCopy();

        this.width = grid.GetLength(1);
        this.height = grid.GetLength(0);

        //Clamp game object within bounds
        x = Helper.Clamp(x, 0, gc.GetGameWidth() - width);
        y = Helper.Clamp(y, 0, gc.GetGameHeight() - height);

        pos = new Point(x, y);
        truePos = new Vector2F(x, y);

        this.isVisible = isVisible;

        this.grid = new char[height, width];
        this.colourGrid = new ColourSet[height, width];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                this.grid[row, col] = grid[row, col];
                this.colourGrid[row, col] = colourGrid[row, col];
            }
        }
    }

    protected Point ClampToScreen(int x, int y)
    {
        //Clamp game object within bounds
        x = Helper.Clamp(x, 0, gc.GetGameWidth() - width);
        y = Helper.Clamp(y, 0, gc.GetGameHeight() - height);

        return new Point(x, y);
    }

    protected Point ClampToScreen(Point pt)
    {
        //Clamp game object within bounds
        return ClampToScreen(pt.x, pt.y);
    }

    protected Vector2F ClampToScreen(Vector2F pt)
    {
        pt.x = (float)Helper.Clamp((float)pt.x, 0.0f, (float)(gc.GetGameWidth() - width));
        pt.y = (float)Helper.Clamp((float)pt.y, 0.0f, (float)(gc.GetGameHeight() - height));

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
     * <b><i>GetTruePos</b></i>
     * <p>
     * {@code public Vector2F GetTruePos()}
     * <p>
     * Retrieve the precise position of the object's top left corner
     * 
     * @return an (x,y) Point for the object's top left corner
     */
    public Vector2F GetTruePos()
    {
        return truePos;
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
     * {@code public boolean GetVisibility()}
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

    /**
     * <b><i>SetPosition</b></i>
     * <p>
     * {@code public void SetPosition(float x, float y)}
     * <p>
     * Set the precise position of the top left corner of the object
     * 
     * @param x The x component of the object's position
     * @param y The y component of the object's position
     */
    public void SetPosition(float x, float y)
    {
        truePos.x = x;
        truePos.y = y;

        truePos = ClampToScreen(truePos);

        pos.x = (int)truePos.x;
        pos.y = (int)truePos.y;
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
        pos = ClampToScreen(x, y);

        truePos.x = pos.x;
        truePos.y = pos.y;
    }

    /**
     * <b><i>UpdateGrid</b></i>
     * <p>
     * {@code public void UpdateGrid(int row, int col, char ch)}
     * <p>
     * Update the character and/or colour of a single character in the object
     * 
     * @param row The row in the character grid to update
     * @param col The column in the character grid to update
     * @param ch The updated character value
     * @param colour The updated colour value
     */
    public void UpdateGrid(int row, int col, char ch, ConsoleColor textColour, ConsoleColor bgColour)
    {
        if (row < height && col < width)
        {
            grid[row, col] = ch;
            colourGrid[row, col].SetFG(textColour);
            colourGrid[row, col].SetBG(bgColour);
        }
    }

    /**
     * <b><i>Move</b></i>
     * <p>
     * {@code public void Move(float deltaX, float deltaY)}
     * <p>
     * Move the object relative to its current position
     * 
     * @param deltaX The change in the x component, use 0 for no change
     * @param deltaY The change in the y component, use 0 for no change
     */
    public void Move(float deltaX, float deltaY)
    {
        truePos.x += deltaX;
        truePos.y += deltaY;

        truePos = ClampToScreen(truePos);

        pos.x = (int)truePos.x;
        pos.y = (int)truePos.y;
    }

    //Will recolour all characters that do not match ignoreColor with new Colours
    public void OverlayColors(ConsoleColor bgCol, ConsoleColor fgCol, bool keepTransparencies)
    {
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (!keepTransparencies || !colourGrid[row, col].IsTransparent())
                {
                    colourGrid[row, col].SetFG(fgCol);
                    colourGrid[row, col].SetBG(bgCol);
                }
            }
        }
    }

    public ConsoleColor GetFGColour(int row, int col)
    {
        if (row < height && col < width)
        {
            return colourGrid[row, col].GetFG();
        }
        return Helper.fgCol;
    }

    public ConsoleColor GetBGColour(int row, int col)
    {
        if (row < height && col < width)
        {
            return colourGrid[row, col].GetBG();
        }
        return Helper.bgCol;
    }

    public void SetVisibility(bool state)
    {
        isVisible = state;
    }
}
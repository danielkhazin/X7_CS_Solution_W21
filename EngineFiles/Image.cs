//Author: Trevor Lane
//File Name: Image.cs
//Project Name: CSGameEngine
//Creation Date: Nov. 15, 2020
//Modified Date: Nov. 23, 2020
//Description:  This class represents the image data to be used in various game objects

/**
* <h3>A basic object to hold visual game object data</h3>
* <b>Creation Date:</b> Nov 15, 2020<br>
* <b>Modified Date:</b> Nov 23, 2020
* <p>
* These Image objects may be reused in multiple game object variables
* 
* @author Trevor Lane
* @version 1.0
*/
public class Image
{
    private char[,] grid;
    private ColourSet[,] colourGrid;

    /**
    * <b><i>Image</b></i>
    * <p>
    * {@code public Image(char[,] grid, String[,]colourGrid)}<br>
    * <p>
    * Create an Image object to store the visuals of a game object
    * 
    * @param grid   The grid of characters representing the visuals of the game object
    * @param colourGrid  The grid of colours representing the visuals of the game object
    */
    public Image(char[,] grid, ColourSet[,] colourGrid)
    {
        this.grid = new char[grid.GetLength(0), grid.GetLength(1)];
        this.colourGrid = new ColourSet[colourGrid.GetLength(0), colourGrid.GetLength(1)];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                this.grid[i, j] = grid[i, j];
                this.colourGrid[i, j] = colourGrid[i, j];
            }
        }
    }

    /**
    * <b><i>Image</b></i>
    * <p>
    * {@code public Image(string[,] imageData}<br>
    * <p>
    * Create an Image object to store the visuals of a game object
    * 
    * @param imageData   The grid of characters representing the visuals of the game object
    */
    public Image(string [,] imageData)
    {
      this.grid = new char[imageData.GetLength(0), imageData.GetLength(1)];
      this.colourGrid = new ColourSet[imageData.GetLength(0), imageData.GetLength(1)];

      //Begin reading in and parsing the image row by row
      for (int i = 0; i < grid.GetLength(0); i++)
      {
        //line = inFile.ReadLine();

        //Loop through each "pixel" from left to right
        for (int j = 0; j < grid.GetLength(1); j++)
        {
          //data = line.Split(new string[] { "," }, StringSplitOptions.None);
          if (imageData[i,j].Equals(Helper.NO_CHAR))
          {
            //Create Transparent character that will not be drawn
            grid[i, j] = ' ';
            colourGrid[i, j] = new ColourSet(Helper.bgCol, Helper.colours['0']);
          }
          else
          {
            //Load the character and colours
            grid[i, j] = imageData[1,j][0];
            colourGrid[i, j] = new ColourSet(Helper.colours[imageData[i,j][1]], Helper.colours[imageData[i,j][2]]);
          }
        }
      }
    }

    /**
    * <b><i>Image</b></i>
    * <p>
    * {@code public Image()}<br>
    * <p>
    * Create a generic Image object to store the visuals of a game object after a failed file load
    */
    public Image()
    {
        grid = new char[,]
        {
      {'B','A','D'},
      {'I','M','G'},
        };

        colourGrid = new ColourSet[,]
        {
      {new ColourSet(Helper.bgCol, Helper.RED),new ColourSet(Helper.bgCol, Helper.RED),new ColourSet(Helper.bgCol, Helper.RED)},
      {new ColourSet(Helper.bgCol, Helper.RED),new ColourSet(Helper.bgCol, Helper.RED),new ColourSet(Helper.bgCol, Helper.RED)}
        };
    }

    /**
     * <b><i>GetGrid</b></i>
     * <p>
     * {@code public char[,] GetGrid()}
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
     * <b><i>GetColourGrid</b></i>
     * <p>
     * {@code public ColourSet[,] GetColourGrid()}
     * <p>
     * Retrieve the colour data of the object in a 2D grid
     * 
     * @return The colour data of the object
     */
    public ColourSet[,] GetColourGrid()
    {
        return colourGrid;
    }

    public int GetWidth()
    {
        return grid.GetLength(1);
    }

    public int GetHeight()
    {
        return grid.GetLength(0);
    }

    public ColourSet[,] GetColourGridCopy()
    {
        ColourSet[,] colGrid = new ColourSet[GetHeight(), GetWidth()];

        for (int i = 0; i < GetHeight(); i++)
        {
            for (int j = 0; j < GetWidth(); j++)
            {
                colGrid[i, j] = new ColourSet(colourGrid[i, j].GetBG(), colourGrid[i, j].GetFG());
            }
        }

        return colGrid;
    }
}
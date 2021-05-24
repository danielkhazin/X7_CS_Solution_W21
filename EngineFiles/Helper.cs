using System;
using System.IO;
using System.Collections.Generic;

public class Helper
{
    private static StreamReader inFile;

    //All the possible User Interface locations
    public const int UI_RIGHT = 0;
    public const int UI_BOTTOM = 1;
    public const int UI_LEFT = 2;
    public const int UI_TOP = 3;
    public const int UI_NONE = 4;

    //All possible colours to simplify access
    public const ConsoleColor BLACK = ConsoleColor.Black;  //Default BackgroundColor
    public const ConsoleColor DARK_BLUE = ConsoleColor.DarkBlue;
    public const ConsoleColor DARK_GREEN = ConsoleColor.DarkGreen;
    public const ConsoleColor DARK_CYAN = ConsoleColor.DarkCyan;
    public const ConsoleColor DARK_RED = ConsoleColor.DarkRed;
    public const ConsoleColor DARK_MAGENTA = ConsoleColor.DarkMagenta;
    public const ConsoleColor DARK_YELLOW = ConsoleColor.DarkYellow;
    public const ConsoleColor GRAY = ConsoleColor.Gray;  //Default ForegroundColor (Text colour)
    public const ConsoleColor DARK_GRAY = ConsoleColor.DarkGray;
    public const ConsoleColor BLUE = ConsoleColor.Blue;
    public const ConsoleColor GREEN = ConsoleColor.Green;
    public const ConsoleColor CYAN = ConsoleColor.Cyan;
    public const ConsoleColor RED = ConsoleColor.Red;
    public const ConsoleColor MAGENTA = ConsoleColor.Magenta;
    public const ConsoleColor YELLOW = ConsoleColor.Yellow;
    public const ConsoleColor WHITE = ConsoleColor.White;

    public static ConsoleColor fgCol = Console.ForegroundColor;
    public static ConsoleColor bgCol = Console.BackgroundColor;

    public const string NO_CHAR = "---";

    private static Random rng = new Random();

    public static readonly Dictionary<char, ConsoleColor> colours = new Dictionary<char, ConsoleColor>
    {
      { '0', ConsoleColor.Black },
      { '1', ConsoleColor.DarkBlue },
      { '2', ConsoleColor.DarkGreen },
      { '3', ConsoleColor.DarkCyan },
      { '4', ConsoleColor.DarkRed },
      { '5', ConsoleColor.DarkMagenta },
      { '6', ConsoleColor.DarkYellow },
      { '7', ConsoleColor.Gray },
      { '8', ConsoleColor.DarkGray },
      { '9', ConsoleColor.Blue },
      { 'A', ConsoleColor.Green },
      { 'B', ConsoleColor.Cyan },
      { 'C', ConsoleColor.Red },
      { 'D', ConsoleColor.Magenta },
      { 'E', ConsoleColor.Yellow },
      { 'F', ConsoleColor.White }
    };

    public static bool IsTransparent(char ch, ColourSet colours)
    {
        return ch == ' ' && colours.IsTransparent();
    }

    public static bool FastIntersects(GameObject obj1, GameObject obj2)
    {
        // Create a rectangle from the object's bounding box
        Rectangle r1 = new Rectangle(obj1.GetPos().x, obj1.GetPos().y, obj1.GetWidth()-1, obj1.GetHeight()-1);
        Rectangle r2 = new Rectangle(obj2.GetPos().x, obj2.GetPos().y, obj2.GetWidth()-1, obj2.GetHeight()-1);

        return r1.Intersects(r2);
    }

    public static bool Intersects(GameObject obj1, GameObject obj2)
    {
        // Create a rectangle from the object's bounding box
        Rectangle r1 = new Rectangle(obj1.GetPos().x, obj1.GetPos().y, obj1.GetWidth(), obj1.GetHeight());
        Rectangle r2 = new Rectangle(obj2.GetPos().x, obj2.GetPos().y, obj2.GetWidth(), obj2.GetHeight());

        // Find the overlapping rectangle of intersection if it exists
        Rectangle r3 = r1.Intersection(r2);

        // First test if their overall rectangles collide
        if (r3 != null)
        {
            // if any "pixel" in the overlapping rectangle is not isEmpty
            // for both rectangles then it is a collision
            for (int y = r3.Y; y < r3.Y + r3.Height; y++)
            {
                for (int x = r3.X; x < r3.X + r3.Width; x++)
                {
                    if (!Helper.IsTransparent(obj1.GetGrid()[y - obj1.GetPos().y, x - obj1.GetPos().x],
                        obj1.GetColours()[y - obj1.GetPos().y, x - obj1.GetPos().x]) &&
                        !Helper.IsTransparent(obj2.GetGrid()[y - obj2.GetPos().y, x - obj2.GetPos().x],
                        obj2.GetColours()[y - obj2.GetPos().y, x - obj2.GetPos().x]))
                    {
                        return true;
                    }
                }
            }
        }

        // no collision found
        return false;
    }

    public static int Clamp(int value, int min, int max)
    {
        return Math.Min(max, Math.Max(min, value));
    }

    public static float Clamp(float value, float min, float max)
    {
        return Math.Min(max, Math.Max(min, value));
    }

    public static Image LoadImage(string fileName)
    {
      Image img = null;
      int width;
      int height;
      char[,] grid;
      ColourSet[,] colourGrid;

      List<String[]> allData = new List<String[]>();

      string[] data;
      int numCols = -1;

      try
      {
        inFile = File.OpenText(fileName);

        while (!inFile.EndOfStream)
        {
          allData.Add(inFile.ReadLine().Split(','));

          if (numCols < 0)
          {
            numCols = allData[allData.Count-1].Length;
          }
          else if (allData[allData.Count-1].Length != numCols)
          {
            throw new Exception("Uneven row lengths in image data");
          }
        }

        //Set the grid dimensions
        height = allData.Count;
        width = numCols;

        //Instantiate the grid sizes
        grid = new char[height, width];
        colourGrid = new ColourSet[height, width];

        //Begin reading in and parsing the image row by row
        for (int i = 0; i < height; i++)
        {
          //Loop through each "pixel" from left to right
          for (int j = 0; j < width; j++)
          {
            data = allData[i];
            if (data[j].Equals(NO_CHAR))
            {
              //Create Transparent character that will not be drawn
              grid[i, j] = ' ';
              colourGrid[i, j] = new ColourSet(bgCol, colours['0']);
            }
            else
            {
              //Load the character and colours
              grid[i, j] = data[j][0];
              colourGrid[i, j] = new ColourSet(colours[data[j][1]], colours[data[j][2]]);
            }
          }
        }

        img = new Image(grid, colourGrid);

      }
      catch (Exception e)
      {
        Console.WriteLine("\n\n===================================");
        Console.WriteLine("Error loading image " + fileName + ", check the file for formatting");
        Console.WriteLine(e.Message);
        Console.WriteLine("===================================\n\n");
      }
      finally
      {
        if (inFile != null)
        {
          inFile.Close();
        }

        if (img == null)
        {
          //Create a BAD IMG
          img = new Image();
        }
      }

      return img;
    }

    public static int GetRandomNum(int min, int max)
    {
        return rng.Next(min, max);
    }

    public static float GetRandomNum(float min, float max)
    {
        return (float)(rng.NextDouble() * (max - min)) + min;
    }
}
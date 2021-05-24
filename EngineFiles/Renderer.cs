//Author: Trevor Lane
//File Name: Renderer.cs
//Project Name: CSharpGameEngine
//Creation Date: Nov. 23, 2020
//Modified Date: Nov. 23, 2020
//Description:  This class handles the drawing and updating of the full game screen

//Preprocessor to resize the window if running from Visual Studio
//Comment this line out to keep original window size or if running in repl.it
//#define VS_ENV

using System;
using System.Collections.Generic;

class Renderer
{
#if VS_ENV
  private const int FULL_WIDTH = 120;
  private const int MAX_FULL_HEIGHT = 50;
#else
  //Repl maximun window size is 80 wide x 24 high
  private const int FULL_WIDTH = 80;
  private const int MAX_FULL_HEIGHT = 24;
#endif

  private const int MAX_UI_SIZE = 30;
  private const int MIN_UI_SIZE = 5;
  private const int MIN_SIDE_UI_SIZE = 10;

  private int gameWidth;
  private int gameHeight;
  private int uiWidth;
  private int uiHeight;
  private int fullWidth;
  private int fullHeight;

  // Store the characters to be displayed at every "pixel"
  private char[,] gameCanvas;
  private char[,] uiCanvas;
  private char[,] fullCanvas;
  private char[,] updateScreen;
  private char[,] cleanCanvas;
  private char[,] cleanUI;

  // Store the colour of the characters to be displayed at every "pixel"
  private ColourSet[,] gameCanvasCols;
  private ColourSet[,] uiCanvasCols;
  private ColourSet[,] fullCanvasCols;
  private ColourSet[,] updateScreenCols;
  private ColourSet[,] cleanCanvasCols;
  private ColourSet[,] cleanUICols;

  private int uiLocation = Helper.UI_RIGHT;
  private Point uiPos;
  private Point gamePos;

  List<GameObject> bgObjects = new List<GameObject>();
  List<GameObject> mgObjects = new List<GameObject>();
  List<GameObject> fgObjects = new List<GameObject>();
  List<UIObject> uiObjects = new List<UIObject>();

  /**
  * <b><i>Renderer</b></i>
  * <p>
  * {@code public Renderer(int uiLocation, int uiSize, int gameWidth, int gameHeight)}<br>
  * <p>
  * Create the renderer to handle all drawing
  * 
  * @param uiLocation  The relative location of the User Interface
  * @param uiSize  The width/height of the User Interface panel
  * @param gameWidth  The width of the game window in pixels
  * @param gameHeight  The height of the game window in pixels
  */
  public Renderer(int uiLocation, int uiSize, int gameWidth, int gameHeight)
  {
    Console.Clear();

    this.uiLocation = uiLocation;

    // Determine game display sizes
    if (uiLocation == Helper.UI_LEFT || uiLocation == Helper.UI_RIGHT) 
    {
      this.uiWidth = Helper.Clamp(uiSize, MIN_SIDE_UI_SIZE, MAX_UI_SIZE);
      this.uiHeight = Math.Min(MAX_FULL_HEIGHT, gameHeight);

      this.gameWidth = Math.Min(FULL_WIDTH - this.uiWidth, gameWidth);
      this.gameHeight = uiHeight;

      if (uiLocation == Helper.UI_LEFT) 
      {
        uiPos = new Point(0, 0);
        gamePos = new Point(this.uiWidth + 1, 0);
      } 
      else 
      {
        uiPos = new Point(this.gameWidth + 1, 0);
        gamePos = new Point(0, 0);
      }

      this.fullWidth = this.gameWidth + this.uiWidth;
      this.fullHeight = this.gameHeight;
    } 
    else if (uiLocation == Helper.UI_TOP || uiLocation == Helper.UI_BOTTOM) 
    {
      this.uiWidth = Math.Min(FULL_WIDTH, gameWidth);
      this.uiHeight = Helper.Clamp(uiSize, MIN_UI_SIZE, MAX_UI_SIZE);

      this.gameWidth = this.uiWidth;
      this.gameHeight = Math.Min(MAX_FULL_HEIGHT - this.uiHeight, gameHeight);

      if (uiLocation == Helper.UI_TOP) 
      {
        uiPos = new Point(0, 0);
        gamePos = new Point(0, uiHeight + 1);
      } 
      else 
      {
        uiPos = new Point(0, this.gameHeight + 1);
        gamePos = new Point(0, 0);
      }

      this.fullWidth = this.gameWidth;
      this.fullHeight = this.gameHeight + this.uiHeight;
    } 
    else // None
    {
      this.uiWidth = 0;
      this.uiHeight = 0;

      this.gameWidth = Math.Min(FULL_WIDTH, gameWidth);
      this.gameHeight = Math.Min(MAX_FULL_HEIGHT, gameHeight);

      this.uiPos = new Point(-1, -1);
      this.gamePos = new Point(0, 0);

      this.fullWidth = this.gameWidth;
      this.fullHeight = this.gameHeight;
    }

    //Set up all layers and their colour maps
    gameCanvas = new char[this.gameHeight,this.gameWidth];
    uiCanvas = new char[this.uiHeight,this.uiWidth];
    fullCanvas = new char[this.fullHeight,this.fullWidth];
    updateScreen = new char[this.fullHeight,this.fullWidth];
    cleanCanvas = new char[this.gameHeight,this.gameWidth];
    cleanUI = new char[this.uiHeight,this.uiWidth];

    gameCanvasCols = new ColourSet[this.gameHeight,this.gameWidth];
    uiCanvasCols = new ColourSet[this.uiHeight,this.uiWidth];
    fullCanvasCols = new ColourSet[this.fullHeight,this.fullWidth];
    updateScreenCols = new ColourSet[this.fullHeight,this.fullWidth];
    cleanCanvasCols = new ColourSet[this.gameHeight,this.gameWidth];
    cleanUICols = new ColourSet[this.uiHeight,this.uiWidth];

    Console.SetWindowSize(fullCanvas.GetLength(1),fullCanvas.GetLength(0));

    // Fill gameCanvas with blanks
    for (int row = 0; row < this.gameHeight; row++) 
    {
      for (int col = 0; col < this.gameWidth; col++) 
      {
        gameCanvas[row,col] = ' ';
        gameCanvasCols[row,col] = new ColourSet();
        cleanCanvas[row,col] = gameCanvas[row,col];
        cleanCanvasCols[row,col] = gameCanvasCols[row,col].Copy();
      }
    }

    // Fill uiCanvas with blanks and a % boarder
    for (int row = 0; row < this.uiHeight; row++) 
    {
      for (int col = 0; col < this.uiWidth; col++) 
      {
        if (row == 0 || row == this.uiHeight - 1 || col == 0 || col == this.uiWidth - 1) 
        {
          uiCanvas[row,col] = '%';
          uiCanvasCols[row,col] = new ColourSet(Helper.bgCol, Helper.RED);
          cleanUI[row,col] = uiCanvas[row,col];
          cleanUICols[row,col] = uiCanvasCols[row,col].Copy();
        } 
        else 
        {
          uiCanvas[row,col] = ' ';
          uiCanvasCols[row,col] = new ColourSet();
          cleanUI[row,col] = uiCanvas[row,col];
          cleanUICols[row,col] = uiCanvasCols[row,col].Copy();
        }
      }
    }

    // Combine Canvases
    CombineCanvases();

    // Copy updated Canvas to full Canvas for the first time
    for (int row = 0; row < fullCanvas.GetLength(0); row++) 
    {
      for (int col = 0; col < fullCanvas.GetLength(1); col++) 
      {
        fullCanvas[row,col] = updateScreen[row,col];
        fullCanvasCols[row,col] = updateScreenCols[row,col].Copy();
      }
    }

    // Do first Draw
    DrawFullScreen();
  }

  private void CombineCanvases() 
  {
    switch(uiLocation)
    {
      case Helper.UI_RIGHT:
        for (int row = 0; row < this.fullHeight; row++) 
        {
          for (int col = 0; col < this.fullWidth; col++) 
          {
            if (col < this.gameWidth) 
            {
              updateScreen[row,col] = gameCanvas[row,col];
              updateScreenCols[row,col] = gameCanvasCols[row,col].Copy();
            } 
            else 
            {
              updateScreen[row,col] = uiCanvas[row,col - gameWidth];
              updateScreenCols[row,col] = uiCanvasCols[row,col - gameWidth].Copy();
            }
          }
        }
        break;
      case Helper.UI_LEFT:
        for (int row = 0; row < this.fullHeight; row++) 
        {
          for (int col = 0; col < this.fullWidth; col++) 
          {
            if (col < this.uiWidth) 
            {
              updateScreen[row,col] = uiCanvas[row,col];
              updateScreenCols[row,col] = uiCanvasCols[row,col].Copy();
            } 
            else 
            {
              updateScreen[row,col] = gameCanvas[row,col - uiWidth];
              updateScreenCols[row,col] = gameCanvasCols[row,col - uiWidth].Copy();
            }
          }
        }
        break;
      case Helper.UI_BOTTOM:
        for (int row = 0; row < this.fullHeight; row++) 
        {
          for (int col = 0; col < this.fullWidth; col++) 
          {
            if (row < this.gameHeight) 
            {
              updateScreen[row,col] = gameCanvas[row,col];
              updateScreenCols[row,col] = gameCanvasCols[row,col].Copy();
            } 
            else 
            {
              updateScreen[row,col] = uiCanvas[row - gameHeight,col];
              updateScreenCols[row,col] = uiCanvasCols[row - gameHeight,col].Copy();
            }
          }
        }
        break;
      case Helper.UI_TOP:
        for (int row = 0; row < this.fullHeight; row++) 
        {
          for (int col = 0; col < this.fullWidth; col++) 
          {
            if (row < this.uiHeight) 
            {
              updateScreen[row,col] = uiCanvas[row,col];
              updateScreenCols[row,col] = uiCanvasCols[row,col].Copy();
            } 
            else 
            {
              updateScreen[row,col] = gameCanvas[row - uiHeight,col];
              updateScreenCols[row,col] = gameCanvasCols[row - uiHeight,col].Copy();
            }
          }
        }
        break;
      default:
        for (int row = 0; row < this.fullHeight; row++) 
        {
          for (int col = 0; col < this.fullWidth; col++) 
          {
            updateScreen[row,col] = gameCanvas[row,col];
            updateScreenCols[row,col] = gameCanvasCols[row,col].Copy();
          }
        }
        break;
    }
  }

  private void DrawFullScreen() 
  {
    for (int row = 0; row < fullCanvas.GetLength(0); row++) 
    {
      for (int col = 0; col < fullCanvas.GetLength(1); col++) 
      {
        DrawOnScreen(fullCanvas[row,col], col, row, fullCanvasCols[row,col]);
      }
    }

    Console.ResetColor();
    Console.SetCursorPosition(0, 0);
  }
  
  private void DrawOnScreen(char ch, int x, int y, ColourSet colours) 
  {
    try
    {

      #if VS_ENV
      if (!colours.Equals(Console.BackgroundColor, Console.ForegroundColor))
      #endif
      {
        Console.ForegroundColor = colours.GetFG();
        Console.BackgroundColor = colours.GetBG();
      }

      Console.SetCursorPosition(x,y);
      Console.Write(ch);

      if (!colours.IsDefault())
      {
        Console.ResetColor();
      }
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
      Console.WriteLine("OOPS: " + x + ", " + y);
      Console.ReadLine();
    }
  }

  private List<UpdateElem> DiffScreen() 
  {
    List<UpdateElem> updateList = new List<UpdateElem>();

    for (int row = 0; row < fullCanvas.GetLength(0); row++) 
    {
      for (int col = 0; col < fullCanvas.GetLength(1); col++) 
      {
        if (fullCanvas[row,col] != updateScreen[row,col] || // compare char
            !fullCanvasCols[row,col].Equals(updateScreenCols[row,col]))
        {
          updateList.Add(new UpdateElem(updateScreen[row,col], updateScreenCols[row,col].Copy(), new Point(col, row)));
        }
      }
    }

    return updateList;
  }

  private void AddLayerToGameCanvas(List<GameObject> objects) 
  {
    char ch;
    ColourSet colours;

    for (int i = 0; i < objects.Count; i++) 
    {
      if (objects[i].GetVisibility())
      {
        for (int row = 0; row < objects[i].GetHeight(); row++) 
        {
          for (int col = 0; col < objects[i].GetWidth(); col++) 
          {
            ch = objects[i].GetGrid()[row,col];
            colours = objects[i].GetColours()[row,col];

            if (!Helper.IsTransparent(ch, colours)) 
            {
              gameCanvas[objects[i].GetPos().y + row,objects[i].GetPos().x + col] = ch;
              gameCanvasCols[objects[i].GetPos().y + row,objects[i].GetPos().x + col] = colours.Copy();
            }
          }
        }
      }
    }
  }

  private void AddLayerToUICanvas(List<UIObject> objects) 
  {
    char ch;
    ColourSet colours;

    for (int i = 0; i < objects.Count; i++) 
    {
      if (objects[i].GetVisibility())
      {
        for (int row = 0; row < objects[i].GetHeight(); row++) 
        {
          for (int col = 0; col < objects[i].GetWidth(); col++) 
          {
            ch = objects[i].GetGrid()[row,col];
            colours = objects[i].GetColours()[row,col];

            if (!Helper.IsTransparent(ch, colours)) 
            {
              uiCanvas[objects[i].GetPos().y + row,objects[i].GetPos().x + col] = ch;
              uiCanvasCols[objects[i].GetPos().y + row,objects[i].GetPos().x + col] = colours.Copy();
            }
          }
        }
      }
    }
  }

  private void CombineLayers() 
  {
    // Background Layer
    AddLayerToGameCanvas(bgObjects);

    // Middleground Layer
    AddLayerToGameCanvas(mgObjects);

    // Foreground Layer
    AddLayerToGameCanvas(fgObjects);

    // UI Layer
    AddLayerToUICanvas(uiObjects);
  }

  private void ClearCanvases() 
  {
    for (int row = 0; row < gameCanvas.GetLength(0); row++) 
    {
      for (int col = 0; col < gameCanvas.GetLength(1); col++) 
      {
        gameCanvas[row,col] = cleanCanvas[row,col];
        gameCanvasCols[row,col] = cleanCanvasCols[row,col].Copy();
      }
    }

    for (int row = 0; row < uiCanvas.GetLength(0); row++) 
    {
      for (int col = 0; col < uiCanvas.GetLength(1); col++) 
      {
        uiCanvas[row,col] = cleanUI[row,col];
        uiCanvasCols[row,col] = cleanUICols[row,col].Copy();
      }
    }

    bgObjects.Clear();
    mgObjects.Clear();
    fgObjects.Clear();
    uiObjects.Clear();
  }

  /**
   * <b><i>GetGameWidth</b></i>
   * <p>
   * {@code public int GetGameWidth()}
   * <p>
   * Retrieve the width of the game play canvas
   * 
   * @return The width of the game canvas
   */
  public int GetGameWidth() 
  {
    return gameWidth;
  }

  /**
   * <b><i>GetGameHeight</b></i>
   * <p>
   * {@code public int GetGameHeight()}
   * <p>
   * Retrieve the height of the game play canvas
   * 
   * @return The height of the game canvas
   */
  public int GetGameHeight() 
  {
    return gameHeight;
  }

  /**
   * <b><i>GetUIWidth</b></i>
   * <p>
   * {@code public int GetUIWidth()}
   * <p>
   * Retrieve the width of the User Interface canvas
   * 
   * @return The width of the User Interface
   */
  public int GetUIWidth() 
  {
    return uiWidth;
  }

  /**
   * <b><i>GetUIHeight</b></i>
   * <p>
   * {@code public int GetUIHeight()}
   * <p>
   * Retrieve the height of the User Interface canvas
   * 
   * @return The height of the User Interface
   */
  public int GetUIHeight() 
  {
    return uiHeight;
  }

  /**
   * <b><i>GetUILocation</b></i>
   * <p>
   * {@code public int GetUILocation()}
   * <p>
   * Retrieve the relative location of the user interface canvas<p>
   * One of UI_TOP, UI_RIGHT, UI_BOTTOM, UI_LEFT or UI_NONE
   *
   * @return The location of the user interface canvas
   */
  public int GetUILocation()
  {
    return uiLocation;
  }


  /**
   * <b><i>Draw</b></i>
   * <p>
   * {@code public void Draw()}
   * <p>
   * Combine and Draw all of the game layers to the screen
   */
  public void Draw() 
  {
    CombineLayers();
    CombineCanvases();

    List<UpdateElem> updateList = DiffScreen();

    if (updateList.Count > 0) 
    {
      // Update and draw each needed change
      for (int i = 0; i < updateList.Count; i++) 
      {
        fullCanvas[updateList[i].pos.y,updateList[i].pos.x] = updateList[i].ch;
        fullCanvasCols[updateList[i].pos.y, updateList[i].pos.x] = updateList[i].colours;

        DrawOnScreen(updateList[i].ch, updateList[i].pos.x, updateList[i].pos.y, updateList[i].colours);
      }

      Console.SetCursorPosition(0, 0);
    }

    //Clear the canvases for the next frame
    ClearCanvases();
  }

  /**
   * <b><i>AddToBG</b></i>
   * <p>
   * {@code public void AddToBG(GameObject gameObject)}
   * <p>
   * Add the gameObject to the Background layer
   * 
   * @param gameObject The element to be drawn
   */
  public void AddToBG(GameObject gameObject) 
  {
    bgObjects.Add(gameObject);
  }

  /**
   * <b><i>AddToMG</b></i>
   * <p>
   * {@code public void AddToMG(GameObject gameObject)}
   * <p>
   * Add the gameObject to the Middleground layer
   * 
   * @param gameObject The element to be added
   */
  public void AddToMG(GameObject gameObject) 
  {
    mgObjects.Add(gameObject);
  }

  /**
   * <b><i>AddToFG</b></i>
   * <p>
   * {@code public void AddToFG(GameObject gameObject)}
   * <p>
   * Add the gameObject to the Foreground layer
   * 
   * @param gameObject The element to be added
   */
  public void AddToFG(GameObject gameObject) 
  {
    fgObjects.Add(gameObject);
  }

  /**
   * <b><i>AddToUI</b></i>
   * <p>
   * {@code public void AddToUI(UIObject uiObject)}
   * <p>
   * Add the uiObject to the User Interface layer
   * 
   * @param uiObject The element to be added to the user interface
   */
  public void AddToUI(UIObject uiObject) 
  {
    uiObjects.Add(uiObject);
  }
}
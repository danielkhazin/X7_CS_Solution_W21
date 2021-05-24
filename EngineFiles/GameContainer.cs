//Author: Trevor Lane
//File Name: GameContainer.cs
//Project Name: CSGameEngine
//Creation Date: June 21, 2016
//Modified Date: Nov. 26, 2020
//Description:  This is the driver class used to implement a full game loop.  This code is a
//              Console based rewrite of a full graphical version written in 2016 that was
//              originally adapted from a series of online sources.  I could not track the
//              original source as it appears with a simple Google search that nearly every
//              Java Game Engine has adapted this game loop logic.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

public class GameContainer
{
  private Input input;
  private AbstractGame game;
  private Renderer render;

  private Thread thread;
  private bool isRunning = false;

  private const float CURR_FPS = 60;
  private int actualFPS = 0;

  /**
  * <b><i>GameContainer</b></i>
  * <p>
  * {@code public GameContainer(AbstractGame game, int uiLocation, int uiSize, int gameWidth, int gameHeight)}<br>
  * <p>
  * Create a driver class to implement the Game Loop backend operations
  * 
  * @param game   The template class to be used for the Game Loop
  * @param uiLocation  The relative location of the User Interface
  * @param uiSize  The width/height of the User Interface panel
  * @param width  The width of the game window in pixels
  * @param height  The height of the game window in pixels
  */
  public GameContainer(AbstractGame game, int uiLocation, int uiSize, int gameWidth, int gameHeight) 
  {
    this.game = game;
    this.render = new Renderer(uiLocation, uiSize, gameWidth, gameHeight);
  }

  /**
   * <b><i>Start</b></i>
   * <p>
   * {@code public void Start()}
   * <p>
   * Begin the execution of the Game Loop and hence the program itself
   */
  public void Start() 
  {
    if (!isRunning) 
    {
      // Initialize game components
      input = new Input();

      thread = new Thread(new ThreadStart(Run));
      thread.Start();
    }
  }

  /**
   * <b><i>Stop</b></i>
   * <p>
   * {@code public void Stop()}
   * <p>
   * Stop the execution of the Game Loop and hence the program itself
   */
  public void Stop() 
  {
    if (isRunning) 
    {
      isRunning = false;
    }
  }

  /**
   * <b><i>Run</b></i>
   * <p>
   * {@code public void Stop()}
   * <p>
   * Stop the execution of the Game Loop and hence the program itself
   */
  public void Run() 
  {
    isRunning = true;

    // modify this to have Update run at different frequency than Draw
    const double GAME_HERTZ = CURR_FPS;

    const double TIME_BETWEEN_UPDATES = 1000000000 / GAME_HERTZ;
    const int MAX_UPDATES_BEFORE_RENDER = 5;

    double lastUpdateTime = NanoTime();
    double lastRenderTime = lastUpdateTime;

    const double TARGET_TIME_BETWEEN_RENDERS = 1000000000 / CURR_FPS;

    int lastSecondTime = (int) (lastUpdateTime / 1000000000);
    double now = 0;
    int updateCount = 0;
    float delta = 0f;
    int frameCount = 0;
    bool render = false;

    // Before the game loop begins, load all content
    LoadContent();

    while (isRunning) 
    {
      now = NanoTime();
      updateCount = 0;
      render = false;

      // If too much time has passed, keep updating until we are caught up to a set
      // maximum update count
      while (now - lastUpdateTime > TIME_BETWEEN_UPDATES && 
             updateCount < MAX_UPDATES_BEFORE_RENDER) 
      {
        // How much time has passed since the last update
        delta = (float) ((now - lastUpdateTime) / 1000000f);
        input.Update(); // Check for user input
        game.Update(this, delta); // Update the game

        // Update update and render data
        lastUpdateTime += TIME_BETWEEN_UPDATES;
        ++updateCount;
        render = true;
      }

      // Render. To do so, we need to calculate interpolation for a smooth render.
      if (render) 
      {
        Draw();
        ++frameCount;
        lastRenderTime = now;
      }

      // Calculate the actual frame rate
      int thisSecond = (int) (lastUpdateTime / 1000000000);
      if (thisSecond > lastSecondTime) 
      {
        // System.out.println("FPS: " + frameCount);
        actualFPS = frameCount;
        frameCount = 0;
        lastSecondTime = thisSecond;
      }

      // Yield until it has been at least the target time between renders.
      while (now - lastRenderTime < TARGET_TIME_BETWEEN_RENDERS && 
             now - lastUpdateTime < TIME_BETWEEN_UPDATES) 
      {
        // Since we have time to spare, allow the CPU to let another process take over
        Thread.Yield();
        now = NanoTime();
      }
    }
  }

  private long NanoTime() 
  {
   long nano = 10000L * Stopwatch.GetTimestamp();
   nano /= TimeSpan.TicksPerMillisecond;
   nano *= 100L;
   return nano;
  }

  private void Sleep(int milliseconds) 
  {
    try 
    {
      Thread.Sleep(milliseconds);
    } 
    catch (Exception e) 
    {
      Console.WriteLine(e.StackTrace);
    }
  }

  private void LoadContent() 
  {
    game.LoadContent(this);
  }

  private void Update(double deltaTime) 
  {
    game.Update(this, (float) deltaTime);
  }

  private void Draw() 
  {
    game.Draw(this);
    render.Draw();
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
    return render.GetGameWidth();
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
    return render.GetGameHeight();
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
    return render.GetUIWidth();
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
    return render.GetUIHeight();
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
    return render.GetUILocation();
  }

  /**
   * <b><i>GetTargetFPS</b></i>
   * <p>
   * {@code public float GetTargetFPS()}
   * <p>
   * Retrieve the target game frame rate
   * 
   * @return The target frame rate of the game
   */
  public float GetTargetFPS() 
  {
    return CURR_FPS;
  }

  /**
   * <b><i>GetActualFPS</b></i>
   * <p>
   * {@code public int GetActualFPS()}
   * <p>
   * Retrieve the current game frame rate
   * 
   * @return The current frame rate of the game
   */
  public int GetActualFPS() 
  {
    return actualFPS;
  }

  /**
   * <b><i>DrawToBackground</b></i>
   * <p>
   * {@code public void DrawToBackground(GameObject gameObject)}
   * <p>
   * Draw the gameObject to the Background layer
   * 
   * @param gameObject The element to be drawn
   */
  public void DrawToBackground(GameObject gameObject) 
  {
    render.AddToBG(gameObject);
  }

  /**
   * <b><i>DrawToMidground</b></i>
   * <p>
   * {@code public void DrawToMidground(GameObject gameObject)}
   * <p>
   * Draw the gameObject to the Middleground layer
   * 
   * @param gameObject The element to be drawn
   */
  public void DrawToMidground(GameObject gameObject) 
  {
    render.AddToMG(gameObject);
  }

  /**
   * <b><i>DrawToForeground</b></i>
   * <p>
   * {@code public void DrawToForeground(GameObject gameObject)}
   * <p>
   * Draw the gameObject to the Foreground layer
   * 
   * @param gameObject The element to be drawn
   */
  public void DrawToForeground(GameObject gameObject) 
  {
    render.AddToFG(gameObject);
  }

  /**
   * <b><i>DrawToUserInterface</b></i>
   * <p>
   * {@code public void DrawToUserInterface(UIObject uiObject)}
   * <p>
   * Draw the gameObject to the User Interface panel
   * 
   * @param uiObject The element to be drawn
   */
  public void DrawToUserInterface(UIObject uiObject) 
  {
    render.AddToUI(uiObject);
  }
}
//Author:
//FileName:
//Project Name:
//Creation Date:
//Modified Date:
//Description:

using System;
using System.Collections.Generic;

class MainClass : AbstractGame
{
  //Game States - Add/Remove/Modify as needed
  //These are the most common game states, but modify as needed
  //You will ALSO need to modify the two switch statements in Update and Draw
  private const int MENU = 0;
  private const int SETTINGS = 1;
  private const int INSTRUCTIONS = 2;
  private const int GAMEPLAY = 3;
  private const int PAUSE = 4;
  private const int ENDGAME = 5;

  //Choose between UI_RIGHT, UI_LEFT, UI_TOP, UI_BOTTOM, UI_NONEs
  private static int uiLocation = Helper.UI_BOTTOM;

  ////////////////////////////////////////////
  //Set the game and user interface dimensions
  ////////////////////////////////////////////

  //Min: 5 top/bottom, 10 left/right, Max: 30
  private static int uiSize = 5;

  //On VS: Max: 120 - uiSize, UI_NONE gives full width up to 120
  //On Repl: Max: 80 - uiSize, UI_NONE can give full width up to 80
  private static int gameWidth = 80;

  //On VS: Max: 50 - uiSize, UI_NONE gives full height up to 50
  //On Repl: Max: 24 - uiSize, UI_NONE can give full height up to 24
  private static int gameHeight = 19;

  //Store and set the initial game state, typically MENU to start
  int gameState = GAMEPLAY;

  ////////////////////////////////////////////
  //Define your Global variables here (They do NOT need to be static)
  ////////////////////////////////////////////
  const int TOP = 0;
  const int MID = 1;
  const int BOT = 2;

  Image playerImg;
  Image enemyImg;
  Image bulletImg;
  GameObject player;
  GameObject enemyTop;
  GameObject enemyMid;
  GameObject enemyBot;

  //Store the y value for each enemy position to allow for
  //easy player movement
  int[] enemyPositions = new int[]{1,8,16};

  int[] enemyMinTime = new int[]{2000,1000,3000};
  int[] enemyMaxTime = new int[]{4000,5000,5000};
  float[] enemyCurTime = new float[3];

  //Store which enemy the player is in line with
  int playerPos = MID;

  //The size is 3 allowing for one active bullet per enemy at a time.
  //No more than 3 is needed because at the speed of 1.5 pixels per update
  //and a screen width of 80, it will take less than 1 second (the minimum
  //bullet spawn time) to make it across the screen to deactivate.
  GameObject[] bullets = new GameObject[3];

  float bulletSpeed = 1.5f;

  static void Main(string[] args)
  {
    /***************************************************************
                DO NOT TOUCH THIS SECTION
    ***************************************************************/
    GameContainer gameContainer = new GameContainer(new MainClass(), uiLocation, uiSize, gameWidth, gameHeight);
    gameContainer.Start();
  }

  public override void LoadContent(GameContainer gc)
  {
    //Load all of your "Images", GameObjects and UIObjects here.
    //This is also the place to setup any other aspects of your program before the game loop starts
    playerImg = Helper.LoadImage("Images/Player.txt");
    enemyImg = Helper.LoadImage("Images/Enemy.txt");
    bulletImg = Helper.LoadImage("Images/Bullet.txt");

    player = new GameObject(gc, playerImg, 2, enemyPositions[MID], true);
    enemyTop = new GameObject(gc, enemyImg, gc.GetGameWidth() - enemyImg.GetWidth() - 2, enemyPositions[TOP], true);
    enemyMid = new GameObject(gc, enemyImg, enemyTop.GetPos().x, enemyPositions[MID], true);
    enemyBot = new GameObject(gc, enemyImg, enemyTop.GetPos().x, enemyPositions[BOT], true);

    enemyCurTime[TOP] = GenBulletSpawnTime(TOP);
    enemyCurTime[MID] = GenBulletSpawnTime(MID);
    enemyCurTime[BOT] = GenBulletSpawnTime(BOT);

    //Create the bullets at 0,0, but invisible (and hence inactive)
    for (int i = 0; i < bullets.Length; i++)
    {
      bullets[i] = new GameObject(gc, bulletImg, 0, 0, false);
    }
  }

  public override void Update(GameContainer gc, float deltaTime)
  {

    //This will exit your program with the x key.  You may remove this if you want       
    if (Input.IsKeyDown(ConsoleKey.X)) gc.Stop();

    switch (gameState)
    {
      case MENU:
        //Get and implement menu interactions
        break;
      case SETTINGS:
        //Get and apply changes to game settings
        break;
      case INSTRUCTIONS:
        //Get user input to return to MENU
        break;
      case GAMEPLAY:
        //Implement standared game logic (input, update game objects, apply physics, collision detection)
        UpdateGamePlay(gc, deltaTime);
        break;
      case PAUSE:
        //Get user input to resume the game
        break;
      case ENDGAME:
        //Wait for final input based on end of game options (end, restart, etc.)
        break;
    }
  }

  public override void Draw(GameContainer gc)
  {
    //NOTE: The only logic in this section should be draw commands and loops.
    //There may be some minor selection, but choosing what to draw 
    //should be handled in the Update and the visibility property 
    //of GameObject's

    switch (gameState)
    {
      case MENU:
        //Draw the possible menu options
        break;
      case SETTINGS:
        //Draw the settings with prompts
        break;
      case INSTRUCTIONS:
        //Draw the game instructions including prompt to return to MENU
        break;
      case GAMEPLAY:
        //Draw all game objects on each layers (background, middleground, foreground and user interface)
        DrawGamePlay(gc);
        break;
      case PAUSE:
        //Draw the pause screen, this may include the full game drawing behind
        break;
      case ENDGAME:
        //Draw the final feedback and prompt for available options (exit,restart, etc.)
        break;
    }
  }

  private void UpdateGamePlay(GameContainer gc, float deltaTime)
  {
    //React to User Input
    if (Input.IsKeyDown(ConsoleKey.W))
    {
      playerPos = Helper.Clamp(playerPos-1, TOP, BOT);
      player.SetPosition(player.GetPos().x, enemyPositions[playerPos]);
    }
    if (Input.IsKeyDown(ConsoleKey.S))
    {
      playerPos = Helper.Clamp(playerPos+1, TOP, BOT);
      player.SetPosition(player.GetPos().x, enemyPositions[playerPos]);
    }

    //Update World
    UpdateEnemyTiming(gc, TOP, deltaTime);
    UpdateEnemyTiming(gc, MID, deltaTime);
    UpdateEnemyTiming(gc, BOT, deltaTime);

    //Update Game objects
    for (int i = 0; i < bullets.Length; i++)
    {
      //Only move active bullets
      if (bullets[i].GetVisibility() == true)
      {
        bullets[i].Move(-bulletSpeed, 0f);

        //deactivate the bullet if it reaches the edge of the screen
        if (bullets[i].GetPos().x <= 0)
        {
          bullets[i].SetVisibility(false);
        }
      }
    }

  }

  private void DrawGamePlay(GameContainer gc)
  {
    gc.DrawToMidground(player);
    gc.DrawToMidground(enemyTop);
    gc.DrawToMidground(enemyMid);
    gc.DrawToMidground(enemyBot);

    for (int i = 0; i < bullets.Length; i++)
    {
      //Only draw active bullets
      if (bullets[i].GetVisibility() == true)
      {
        gc.DrawToMidground(bullets[i]);
      }
    }
  }

  private int GenBulletSpawnTime(int enemyType)
  {
    return Helper.GetRandomNum(enemyMinTime[enemyType], enemyMaxTime[enemyType] + 1);
  }

  private void UpdateEnemyTiming(GameContainer gc, int enemyType, float deltaTime)
  {
    int index;
    
    enemyCurTime[enemyType] -= deltaTime;

    if (enemyCurTime[enemyType] <= 0f)
    {
      //Find the first inactive bullet in the array to shoot (activate)
      index = FindInactiveObject(bullets);

      if (index >= 0)
      {
        //Move the bullet to the enemy position
        bullets[index].SetPosition(enemyTop.GetPos().x, enemyPositions[enemyType]);

        //Activate the bullet
        bullets[index].SetVisibility(true);

        //Only reset the timer if the bullet was fired, otherwise it will try again next update
        enemyCurTime[enemyType] = GenBulletSpawnTime(enemyType);
      }
    }
  }

  private int FindInactiveObject(GameObject[] objects)
  {
    //Loop through all possible objects looking for the first inactive one
    for (int i = 0; i < objects.Length; i++)
    {
      //An object is inactive it is not visible
      if (objects[i].GetVisibility() == false)
      {
        return i;
      }
    }

    //No objects active already, return a bad index
    return -1;
  }
}
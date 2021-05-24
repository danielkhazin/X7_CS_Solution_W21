//Author: Trevor Lane
//File Name: UITextObject.cs
//Project Name: CShGameEngine
//Creation Date: Nov 18, 2020
//Modified Date: Nov. 23, 2020
//Description:  An object maintaining display text for various uses in the user interface

using System;

public class UITextObject : UIObject
{
  private string text = "";

  /**
  * <b><i>UITextObject</b></i>
  * <p>
  * {@code public UITextObject(GameContainer gc, int x, int y, string colour, bool isVisible, string text)}<br>
  * <p>
  * Create a coloured progress bar.
  * 
  * @param gc   The connection to the game loop driver class
  * @param x  The x component of the object's position where (1,1) is the top left corner
  * @param y  The y component of the object's position where (1,1) is the top left corner
  * @param colour  The colour of the progress bar object (See Helper class for colour options)
  * @param isVisible  The visibilty status of the game object
  * @param text  The text to be displayed on the user interface
  */
  public UITextObject(GameContainer gc, int x, int y, ConsoleColor colour, bool isVisible, string text) : base(gc,x,y,colour,isVisible)
  {
    this.text = text;
    TruncateText();

    height = 1;
    pos = ClampToUI(pos);

    //Store text in char array and its colours
    SetText();
  }

  private void SetText()
  {
    //Set up grid to handle character and colour text
    grid = new char[1,text.Length];
    colourGrid = new ColourSet[1,text.Length];

    for (int i = 0; i < width; i++)
    {
      grid[0,i] = text[i];
      colourGrid[0, i] = new ColourSet();
    }
    SetColours();
  }

  private void SetColours()
  {
    for (int i = 0; i < width; i++)
    {
      colourGrid[0,i].SetFG(colour);
    }
  }

  private void TruncateText()
  {
    int spaceToWall = gc.GetUIWidth() - pos.x;

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

    pos = ClampToUI(pos.x,pos.y);

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
  public override void SetColour(ConsoleColor colour)
  {
    this.colour = colour;
    SetColours();
  }
}
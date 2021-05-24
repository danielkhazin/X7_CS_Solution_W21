//Author: Trevor Lane
//File Name: UpdateElem.cs
//Project Name: CSharpGameEngine
//Creation Date: Nov 18, 2020
//Modified Date: Nov. 23, 2020
//Description:  A data class managing a single character to be updated on the screen

public class UpdateElem
{

  /**
	 * The character to be updated
	 */
  public char ch;

  /**
	 * The colour to be updated
	 */
  public ColourSet colours;

  /**
	 * The position of the character within the full game boundaries
	 */
  public Point pos;

  /**
	 * <b><i>UpdateElem</b></i><p>
	 * {@code public UpdateElem(char ch, String colour, Point pos)}<p>
	 * Create a vector with the given coordinates
   *
	 * @param ch The new character
	 * @param colour The new colour of the character
   * @param pos The screen coordinate to be modified
	 */
  public UpdateElem(char ch, ColourSet colours, Point pos)
  {
    this.ch = ch;
    this.colours = colours;
    this.pos = pos;
  }
}
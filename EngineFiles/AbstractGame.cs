/**
 * <h3>Template Class for the Game Loop</h3>
 * <b>Creation Date:</b> June 21, 2016<br>
 * <b>Modified Date:</b> Nov 13, 2020<p>
 * @author Trevor Lane
 * @version 1.0 
 */
public abstract class AbstractGame
{
	
	/**
	 * <b><i>LoadContent</i></b><p>
	 * {@code public abstract void LoadContent(GameContainer gc)}<p>
	 * Load all the Game Media and set up and associated data
	 * @param gc The driver class for the Game
	 */
	public abstract void LoadContent(GameContainer gc);
	
	/**
	 * <b><i>Update</i></b><p>
	 * {@code public abstract void Update(GameContainer gc, float deltaTime)}<p>
	 * Update all game related data including user input, game objects, etc.
	 * @param gc The driver class for the Game
	 * @param deltaTime The amount of time since the last time the game was updated
	 */
	public abstract void Update(GameContainer gc, float deltaTime);
	
	/**
	 * <b><i>Draw</i></b><p>
	 * {@code public abstract void Draw(GameContainer gc, Graphics2D gfx)}<p>
	 * Draw all game objects relevant to the current state of the game
	 * @param gc The driver class for the Game
	 */
	public abstract void Draw(GameContainer gc);

}
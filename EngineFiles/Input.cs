//Author: Trevor Lane
//File Name: Input.java
//Project Name: JavaConsoleGame
//Creation Date: June 21, 2016
//Modified Date: Nov. 23, 2020
//Description:  This class represents manages user input via Console keyboard input

using System;

public class Input
{
	private static bool[] kb = new bool[256];
	private static bool[] prevKb = new bool[256];

	/**
	 * <b><i>Input</b></i><p>
	 * {@code public Input()}<p>
	 * Create a class to track user input via keyboard input
	 */
	public Input()
	{
    Console.TreatControlCAsInput = true;
    Console.CursorVisible = false;
	}

	/**
	 * <b><i>Update</b></i><p>
	 * {@code public void Update()}<p>
	 * Update the previous states of the keyboard.<br>
	 * Current states are tracked via standard input in while in raw input mode
	 */
	public void Update()
	{
		prevKb = (bool[])kb.Clone();

    //turn all keys off
    for (int i = 0; i < kb.Length; i++)
    {
      kb[i] = false;
    }

    try
    {
      //turn any pressed keys on
      if (Console.KeyAvailable)
      {
        kb[(int)Console.ReadKey(true).Key] = true;
      }
    }
    catch(Exception e)
    {
      Console.WriteLine(e.Message);
    }
	}

	/**
	 * <b><i>IsKeyDown</b></i><p>
	 * {@code public static boolean IsKeyDown(int keyCode)}<p>
	 * Checks if a given key is currently down
	 * @param keyCode The char of the key to be tested<br>e.g. 'a' for "a"
	 * @return True if the given key is currently down, false otherwise
	 */
	public static bool IsKeyDown(ConsoleKey keyCode)
	{
		return kb[(int)keyCode];
	}
}
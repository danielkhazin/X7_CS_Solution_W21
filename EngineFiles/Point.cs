using System;

public class Point
{
  public int x{get;set;}
  public int y{get;set;}

  public Point(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  public Point()
  {
    this.x = 0;
    this.y = 0;
  }

  /**
	 * A default zero Point
	 */
  public static Point ZERO = new Point(0,0);

  public override string ToString()
  {
    return "(" + x + "," + y + ")";
  }

  public static Point operator +(Point p1, Point p2)
    => new Point(p1.x + p2.x, p1.y + p2.y);

  public static Point operator -(Point p1, Point p2)
    => new Point(p1.x - p2.x, p1.y - p2.y);

  public static bool operator ==(Point p1, Point p2)
  {
    // Check for null on p1
    if (Object.ReferenceEquals(p1, null))
    {
        if (Object.ReferenceEquals(p2, null))
        {
            // null == null = true.
            return true;
        }

        // Only the p1 is null.
        return false;
    }
    // Equals handles case of null p2.
    return p1.Equals(p2);
  }
  
  public static bool operator !=(Point p1, Point p2)
    => !(p1 == p2);
  
  public override bool Equals(Object o)
    => this.Equals(o as Point);

  public bool Equals(Point p)
  {
    // If parameter is null, return false.
    if (Object.ReferenceEquals(p, null)) return false;

    // Comparing p against itself (Both reference the same object)
    if (Object.ReferenceEquals(this, p)) return true;

    // If run-time types are not exactly the same, return false.
    if (this.GetType() != p.GetType()) return false;

    // Return true if the fields match.
    return (x == p.x) && (y == p.y);
  }

  public override int GetHashCode()
  {
      return x * 0x00010000 + y;
  }

  /**
	* <b><i>Copy</b></i><p>
	* {@code public static Point Copy(Point p)}<p>
	* Create a new Point with the same coordinates of the given Point<p>
	* @param p The Point to be copied
	* @return A new Point with matching coordinates of the given Point
	*/
  public static Point Copy(Point p)
  {
	return new Point(p.x, p.y);
  }
}
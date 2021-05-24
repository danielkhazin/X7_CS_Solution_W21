//Author: Trevor Lane
//File Name: Vector2F.cs
//Project Name: JavaConsoleGame
//Creation Date: June 21, 2016
//Modified Date: Nov. 23, 2020
//Description:  This class represent a 2D Vector or Screen Coordinate

using System;

/**
 * <h3>Coordinate Vectors</h3>
 * <b>Creation Date:</b> June 21, 2016<br>
 * <b>Modified Date:</b> Nov 23, 2020<p>
 * @author Trevor Lane
 * @version 1.0 
 */
public class Vector2F
{
	/**
	 * The x coordinate of the vector
	 */
	public float x{get;set;}
	/**
	 * The y coordinate of the vector
	 */
	public float y{get;set;}
	
	/**
	 * A default zero vector
	 */
	public static Vector2F ZERO = new Vector2F(0f,0f);
	
	/**
	 * <b><i>Vector2F</b></i><p>
	 * {@code public Vector2F()}<p>
	 * Create a default zero vector with no magnitude or direction
	 */
	public Vector2F()
	{
		this.x = 0f;
		this.y = 0f;
	}
	
	/**
	 * <b><i>Vector2F</b></i><p>
	 * {@code public Vector2F(float x, float y)}<p>
	 * Create a vector with the given coordinates
	 * @param x The x component of the vector
	 * @param y The y component of the vector
	 */
	public Vector2F(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	/**
	 * <b><i>Zero</b></i><p>
	 * {@code public void Zero()}<p>
	 * Convert the vector into a zero vector, with no magnitude or direction<p>
	 */
	public void Zero()
	{
		x = 0f;
		y = 0f;
	}
	
	/**
	 * <b><i>Normalize</b></i><p>
	 * {@code public void Normalize()}<p>
	 * Convert the vector into a unit vector in the same direction<p>
	 */
	public void Normalize()
	{
		double length = Length(this);//Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
		
		if (length != 0.0)
		{
			float scaler = 1.0f / (float) length;
			x = x * scaler;
			y = y * scaler;
		}
	}
	
	/**
	 * <b><i>Normalize</b></i><p>
	 * {@code public static Vector2F Normalize(Vector2F vec)}<p>
	 * Convert the given vector into a unit vector with the same direction<p>
	 * @param vec The vector in screen coordinates to be normalized
	 * @return A new unit vector in the same direction as vec
	 */
	public static Vector2F Normalize(Vector2F v)
	{
		double length = Length(v);//Math.Sqrt(Math.Pow(vec.x, 2) + Math.Pow(vec.y, 2));
		
		if (length != 0.0)
		{
			float scaler = 1.0f / (float) length;
			float x = v.x * scaler;
			float y = v.y * scaler;
			
			return new Vector2F(x,y);
		}
		
		return ZERO;
	}
	
	/**
	 * <b><i>Equals</b></i><p>
	 * {@code public boolean Equals(Vector2F vec)}<p>
	 * Determine if the given vector's coordinates match this one's<p>
	 * @param vec The comparison vector in screen coordinates
	 * @return true if vec's coordinates match this one's exactly, false otherwise
	 */
   /*
	public bool Equals(Vector2F v)
	{
		return this.x == v.y && this.y == v.y;
	}
  */
	
	/**
	 * <b><i>Add</b></i><p>
	 * {@code public static Vector2F Add(Vector2F v1, Vector2F v2)}<p>
	 * Add the respective coordinates of the two given vectors<p>
	 * @param v1 The first vector in screen coordinates
	 * @param v2 The second vector in screen coordinates
	 * @return A new vector with the sum of each coordinates for its respective components
	 */
   /*
	public static Vector2F Add(Vector2F v1, Vector2F v2)
	{
		float x = v1.x + v2.x;
		float y = v1.y + v2.y;
		return new Vector2F(x, y);
	}
  */
	
	/**
	 * <b><i>Subtract</b></i><p>
	 * {@code public static Vector2F Subtract(Vector2F v1, Vector2F v2)}<p>
	 * Subtract v2 from v1<p>
	 * @param v1 The first vector in screen coordinates
	 * @param v2 The second vector in screen coordinates
	 * @return A new vector with the subtraction result of each coordinates for its respective components
	 */
   /*
	public static Vector2F Subtract(Vector2F v1, Vector2F v2)
	{
		float x = v1.x - v2.x;
		float y = v1.y - v2.y;
		return new Vector2F(x, y);
	}
  */
	
	/**
	 * <b><i>Scaler</b></i><p>
	 * {@code public static Vector2F Scaler(Vector2F vec, float scaler)}<p>
	 * Multiply the components of vec by the value, scaler<p>
	 * @param vec The vector to by modified
	 * @param scaler The value to multiply vec by
	 * @return A new vector with the scaler applied to the components of vec
	 */
   /*
	public static Vector2F Scaler(Vector2F vec, float scaler)
	{
		return new Vector2F(vec.x * scaler, vec.y * scaler);
	}
  */
	
	/**
	 * <b><i>Copy</b></i><p>
	 * {@code public static Vector2F Copy(Vector2F vec)}<p>
	 * Create a new Vector with the same coordinates of the given vector<p>
	 * @param vec The vector to be copied
	 * @return A new vector with matching coordinates of the given vector
	 */
	public static Vector2F Copy(Vector2F v)
	{
		return new Vector2F(v.x, v.y);
	}
	
	/**
	 * <b><i>Length</b></i><p>
	 * @param vec The vector to have its length calculated
	 * @return The length (magnitude) of the given vector
	 */
	public static float Length(Vector2F v)
	{
		return (float)(Math.Sqrt(Math.Pow(v.x, 2) + Math.Pow(v.y, 2)));
	}

	/**
	 * <b><i>Length</b></i><p>
	 * {@code public float Length()}<p>
	 * Retrieve the length (magnitude) of the vector
	 * @return The length of the vector as a float
	 */
	public float Length()
	{
		return Length(this);
	}
	
	/**
	 * <b><i>Distance</b></i><p>
	 * {@code public static double Distance(Vector2F v1, Vector2F v2)}<p>
	 * Calculate the distance between two arbitrary vectors<p>
	 * @param v1 The first vector in screen coordinates
	 * @param v2 The second vector in screen coordinates
	 * @return The unsigned distance between v1 and v2
	 */
	public static double Distance(Vector2F v1, Vector2F v2)
	{
		return Length(v2 - v1);
	}
	
	/**
	 * <b><i>DotProduct</b></i><p>
	 * {@code public static double DotProduct(Vector2F v1, Vector2F v2)}<p>
	 * Calculate the dot product of the two vectors<p> 
	 * @param v1 The first vector in screen coordinates
	 * @param v2 The second vector in screen coordinates
	 * @return The result of the Dot Product of the two vectors
	 */
	public static double DotProduct(Vector2F v1, Vector2F v2)
	{
		return (v1.x * v2.x) + (v1.y * v2.y);
	}
	
	/**
	 * <b><i>CrossProduct</b></i><p>
	 * {@code public static double CrossProduct(Vector2F v1, Vector2F v2)}<p>
	 * Calculate the cross product of the two vectors.    This value represents the magnitude
	 * of the normal or perpendicular vector in the z-axis of the two vectors in 3D<p> 
	 * @param v1 The first vector in screen coordinates
	 * @param v2 The second vector in screen coordinates
	 * @return The result of the Cross Product of the two vectors.
	 */
	public static float CrossProduct(Vector2F v1, Vector2F v2)
	{
		return v1.x * v2.y - v1.y * v2.x;
	}
	
  public static Vector2F operator*(Vector2F v, float scaler)
    => new Vector2F(v.x * scaler, v.y * scaler);

  public static Vector2F operator +(Vector2F v1, Vector2F v2)
    => new Vector2F(v1.x + v2.x, v1.y + v2.y);

  public static Vector2F operator -(Vector2F v1, Vector2F v2)
    => new Vector2F(v1.x - v2.x, v1.y - v2.y);

  public static bool operator ==(Vector2F v1, Vector2F v2)
  {
    // Check for null on v1
    if (Object.ReferenceEquals(v1, null))
    {
        if (Object.ReferenceEquals(v2, null))
        {
            // null == null = true.
            return true;
        }

        // Only the v1 is null.
        return false;
    }
    // Equals handles case of null v2.
    return v1.Equals(v2);
  }
  
  public static bool operator !=(Vector2F v1, Vector2F v2)
    => !(v1 == v2);
  
  public override bool Equals(Object o)
    => this.Equals(o as Vector2F);

  public bool Equals(Vector2F v)
  {
    // If parameter is null, return false.
    if (Object.ReferenceEquals(v, null)) return false;

    // Comparing p against itself (Both reference the same object)
    if (Object.ReferenceEquals(this, v)) return true;

    // If run-time types are not exactly the same, return false.
    if (this.GetType() != v.GetType()) return false;

    // Return true if the fields match.
    return (x == v.x) && (y == v.y);
  }

  public override int GetHashCode()
  {
      return (int)(x * 0x00010000 + y);
  }

  /**
	 * <b><i>ToString</b></i><p>
	 * {@code public String ToString()}<p>
	 * Retrieve String representation of the Vector
	 * @return A formatted String representation of the Vector components
	 */
	public override string ToString()
	{
		return "(" + this.x + ", " + this.y + ")";
	}
}

using OpenTK.Mathematics;

namespace LearningOpenTK.Common;

public class MathUtils
{
    
    public const float Pi = 3.14159265358979323846f;

    public const float HalfPi = 3.14159265358979323846f/2f;

    public const float Pi2 = Pi*2;

    public static float Abs(float value)
    {
        return Math.Abs(value);
    }

    public static float Rad(float angle) 
    {
        return angle * Pi / 180;
    }

    public static float Deg(float angle) 
    {
        return angle * 180/Pi;
    }

    public static float Sqrt(float value)
    {
        return (float)Math.Sqrt(value);
    }

    public static float Cos(float value)
    {
        return (float)Math.Cos(value);
    }

    public static float Sin(float value)
    {
        return (float)Math.Sin(value);
    }

    public static float Acos(float value)
    {
        return (float)Math.Acos(value);
    }

    public static float Atan2(float y,float x)
    {
        return (float)Math.Atan2(y,x);
    }

    public static float Asin(float a)
    {
        return (float)Math.Asin(a);
    }
    
    public static double CosFromSin(double sin, double angle) 
    {
        var cos = Sqrt((float) (1.0 - sin * sin));
        var a = angle + HalfPi;
        var b = a - (int)(a / Pi2) * Pi2;
        if (b < 0.0)
            b = Pi2 + b;
        if (b >= Pi)
            return -cos;
        return cos;
    }
    
    public static float Lerp(float from, float to, float t)
    {
        return (1 - t) * from + t * to;
    }
    
    public static float Clamp(float val, float min, float max) 
    {
        return Math.Max(min, Math.Min(max, val));
    }
    
    public static float ShortestAngle(float a,float b)
    {
        return (b-a + 180) % 360 - 180;
    }

    public static bool EqualsApproximately(float a, float b,float factor)
    {
        var diff = Math.Abs(b - a);
        var tolerance = factor/100 * b;
        return diff < tolerance;
    }
    
    public static float CatmullRom(float previous, float current, float next, float nextNext,float f) 
    {
        return 0.5F * (2.0F * current + (next - previous) * f + (2.0F * previous - 5.0F * current + 4.0F * next - nextNext) * f * f + (3.0F * current - previous - 3.0F * next + nextNext) * f * f * f);
    }
    
    public static float WrapDegrees(float angle)
    {
        var f = angle % 360.0F;
        if (f >= 180.0F) {
            f -= 360.0F;
        }
        if (f < -180.0F) {
            f += 360.0F;
        } 
        return f;
    }
}
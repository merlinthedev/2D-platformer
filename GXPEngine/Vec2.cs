using GXPEngine; // Allows using Mathf functions
using System;

public struct Vec2 {
    public float x;
    public float y;


    public Vec2(float x = 0, float y = 0) {
        this.x = x;
        this.y = y;
    }

    // TODO: Implement Length, Normalize, Normalized, SetXY methods (see Assignment 1)


    public void Normalize() {
        float l = Length();

        if (l > 0) {
            this /= Length();
        }
    }

    public float Length() {
        return Mathf.Sqrt((this.x * this.x) + (this.y * this.y));
    }

    public Vec2 Normalized() {
        Vec2 v = new Vec2(x, y);
        v.Normalize();
        return v;
    }

    public void SetXY(float x, float y) {
        this.x = x;
        this.y = y;
    }


    public static float Deg2Rad(float deg) {

        return deg * Mathf.PI / 180;

    }

    public static float Rad2Deg(float rad) {
        return rad * (180 / Mathf.PI);
    }


    // returns a new vector pointing in the given direction in degrees
    public static Vec2 GetUnitVectorDeg(float angle) {
        Vec2 x = new Vec2(1, 0);
        x.RotateDegrees(angle);
        return x;
    }

    public static Vec2 GetUnitVectorRad(float angle) {
        Vec2 x = new Vec2(1, 0); // look at polar / cartesian conversions...
        x.RotateRadians(angle);
        return x;
    }

    //returns a new unit vector pointing in a random direction (all angles should have the same probability(density)).
    public static Vec2 RandomUnitVector() {
        Vec2 x = GetUnitVectorDeg(Utils.Random(0, Mathf.PI * 2));
        return x;
    }

    public float GetAngleRadians() // no args
    {
        return Mathf.Atan2(y, x); // don't forget to unit test!!!
    }

    public float GetAngleDegrees() {
        return Rad2Deg(GetAngleRadians());
    }

    public float Angle {
        get => Mathf.Atan2(y, x);
        set {
            SetXY(Length(), 0); // setAgnle(0)
            RotateRadians(value);
        }
    }

    public void SetAngleRadians(float radians) {
        Angle = radians;
    }

    public void SetAngleDegrees(float degrees) {
        Angle = Deg2Rad(degrees);
    }

    public void RotateRadians(float radians) {
        this.SetXY(x * Mathf.Cos(radians) - y * Mathf.Sin(radians), x * Mathf.Sin(radians) + y * Mathf.Cos(radians));
    }

    public void RotateDegrees(float degrees) {
        RotateRadians(Deg2Rad(degrees));
    }

    public void RotateAroundRadians(float radians, Vec2 p) {
        this -= p;
        RotateRadians(radians);
        this += p;
    }

    public void RotateAroundDegrees(float degrees, Vec2 p) {
        RotateAroundRadians(Deg2Rad(degrees), p);
    }




    // TODO: Implement subtract, scale operators

    public static Vec2 operator +(Vec2 left, Vec2 right) {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator +(Vec2 left, float right) {
        return new Vec2(left.x + right, left.y + right);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right) {
        return new Vec2(left.x - right.x, left.y - right.y);
    }

    public static Vec2 operator -(Vec2 left, float right) {
        return new Vec2(left.x - right, left.y - right);
    }

    public static Vec2 operator /(Vec2 left, Vec2 right) {
        return new Vec2(left.x / right.x, left.y / right.y);
    }

    public static Vec2 operator /(Vec2 left, float right) {
        return new Vec2(left.x / right, left.y / right);
    }

    public static Vec2 operator *(Vec2 left, Vec2 right) {
        return new Vec2(left.x * right.x, left.y * right.y);
    }

    public static Vec2 operator *(Vec2 left, float right) {
        return new Vec2(left.x * right, left.y * right);
    }

    public override string ToString() {
        return String.Format("({0},{1})", x, y);
    }
}


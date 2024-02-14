using static System.Console;
using System;
using static System.Math;

public class vec : IEquatable<vec> {
    public double x,y,z;

    // Constructors
    public vec(){ x=y=z=0; }
    public vec(double x, double y, double z){
        this.x=x; this.y=y; this.z=z;
    }

    // Operators
    public static vec operator*(vec v, double c){return new vec(c*v.x,c*v.y,c*v.z);}
    public static vec operator*(double c, vec v){return v*c;}
    public static vec operator/(vec v, double c){return new vec(v.x/c,v.y/c,v.z/c);}
    public static vec operator+(vec u, vec v){
        return new vec(v.x+u.x,v.y+u.y,v.z+u.z);
    }
    public static vec operator-(vec u){return new vec(-u.x,-u.y,-u.z);}
    public static vec operator-(vec u, vec v){
        return new vec(u.x-v.x, u.y-v.y, u.z-v.z);
    }

    // Methods:
    public void print(string s){Write(s);WriteLine($"{x} {y} {z}");}
    public void print(){this.print("");}
    public double dot(vec rhs) {
        return this.x*rhs.x + this.y*rhs.y + this.z*rhs.z;
    }
    public static double dot(vec u, vec v) {
        return u.x*v.x +u.y*v.y +u.z*v.z;  
    }

    public bool approx(vec rhs){
        return approx(this.x, rhs.x) &&
               approx(this.y, rhs.y) &&
               approx(this.z, rhs.z);
    }
    static bool approx(double a, double b, double acc=1e-9, double eps=1e-9){
        double d = Abs(a-b);
        return (d <= acc) || (d <= Max(Abs(a),Abs(b))*eps);
    }
    static bool approx(vec u, vec v) => u.approx(v);

    public bool Equals(vec other) {
        return this.x==other.x &&
               this.y==other.y &&
               this.z==other.z;
    }
    
    public override string ToString() => $"{this.x}, {this.y}, {this.z}";
}

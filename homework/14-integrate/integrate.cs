using System;
using static System.Math;



public struct integrand {
    public double result, error;
    public static implicit operator double(integrand I) {return I.result;}
    public integrand(double I, double E) {result=I; error=E;}
    public static integrand operator+(integrand lhs, integrand rhs) {
        return new integrand(lhs.result+rhs.result, lhs.error+rhs.error);
    }
}

public class integrate {
    const double EPS = 2.2204460492503131e-16;
    public static integrand adaptive (
        Func<double,double> f,
        double a, 
        double b,
        double delta=0.001, 
        double eps=0.001, 
        double f2=Double.NaN, 
        double f3=Double.NaN, 
        double h_min = EPS // Escape Hatch for diverging integrals
    ) {
        double h=b-a;
        if (h.IsInfinity) { // One of the limits are at infinity.
            
        }
        // Console.Error.WriteLine($"h = {h:G4}, a={a:G4}, b={b:G4}, h_min={h_min:G4}");
        if(Double.IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6); } // first call, no points to reuse
        double f1=f(a+h/6), f4=f(a+5*h/6);
        double Q = (2*f1+f2+f3+2*f4)/6*(h); // higher order rule
        double q = (  f1+f2+f3+  f4)/4*(h); // lower order rule
        double err = Math.Abs(Q-q);
        if ((Abs(h)<h_min) || (err <= delta+eps*Math.Abs(Q))) return (new integrand(Q,err));
        else { 
            return adaptive(f, a, (a+b)/2, delta/Math.Sqrt(2), eps, f1, f2, h_min)+
                   adaptive(f, (a+b)/2, b, delta/Math.Sqrt(2), eps, f3, f4, h_min);
        }
    }
    public static integrand adaptive (
        transformation I_t,
        double delta=0.001, 
        double eps=0.001, 
        double h_min = EPS // Escape Hatch for diverging integrals
    ) {
        return adaptive(I_t.fg, I_t.t_a, I_t.t_b, delta:delta, eps: eps, h_min: h_min);
    }
}

public class transformation {
    const double EPS = 2.2204460492503131e-16;
    public Func<double, double> fg;
    public double t_a, t_b;
    public transformation (
        Func<double,double> f,
        double a, double b
    ) { fg = f; t_a=a; t_b = b;}
    
    public static transformation clenshaw_curtis(
        Func<double,double> f,
        double a, double b
    ) {
        double x_mid  = (a+b)/2;        
        double x_side = (b-a)/2;        
        Func<double, double> fg = (double theta) => f(x_mid + x_side*Cos(theta) )*Sin(theta)*x_side;
        return new transformation(fg, 0, PI);
    }
    // Infinite interval transformation
    static public transformation infinf (Func<double,double> f, bool edge_evaluated = false) {
        if (edge_evaluated) { 
            // some integral solvers evaluate at the edge and must therefore be shifted
            double limit_shift = EPS;
        } else {
            double limit_shift = 0;
        }
        return new transformation(
            (double t) => f( t/(1-t*t) ) * (1+t*t)/Pow(1-t*t, 2),
            -1+limit_shift, 1-limit_shift
        );
    }
    static public transformation ainf (Func<double,double> f, double a, bool edge_evaluated = false) {
        if (edge_evaluated) { 
            // some integral solvers evaluate at the edge and must therefore be shifted
            double limit_shift = EPS;
        } else {
            double limit_shift = 0;
        }
        return new transformation(
            (double t) => f(a+t/(1-t))/Pow(1-t, 2),
            0+limit_shift, 1
        );
    }
    static public transformation infb (Func<double,double> f, double a, bool edge_evaluated = false) {
        if (edge_evaluated) { 
            // some integral solvers evaluate at the edge and must therefore be shifted
            double limit_shift = EPS;
        } else {
            double limit_shift = 0;
        }
        return new transformation(
            (double t) => f(a+t/(1+t))/Pow(1-t, 2),
            0+limit_shift, 1
        );
    }
}

using System;
using static System.Console;
using static matrix;

public static class spline {
    public static double linterp(double[] x, double[] y, double x_new) {
        if (x.Length != y.Length) {
            throw new ArgumentException("The x and y must contain the same amount of points.");
        }
        int i = binsearch(x, x_new);
        double dx = x[i+1] - x[i];
        if (dx<0) throw new ArgumentException("x must be a sorted list."); // Unlikely to happen as the binary search would not terminate?
        double dy = y[i+1] - y[i];
        return y[i] + dy/dx*(x_new-x[i]);
        
    }
    public static double linteg(double[] x, double[] y, double x_end) {
        if (x.Length != y.Length) {
            throw new ArgumentException("The x and y must contain the same amount of points.");
        }
        int i = 0;
        double dx;
        double dy;
        double ans = 0;
        // I think a bin search is wasted cycles as we're already iterating through the
        // list.
        int last_segment = binsearch(x, x_end);
        for (i = 0; i<last_segment; i++) {
            dx = x[i+1] - x[i];
            if (dx<0) throw new ArgumentException("x must be a sorted list."); // Unlikely to happen as the binary search would not terminate?
            dy = y[i+1] - y[i];
            ans += dx * (y[i] + dy/2.0); //
        }
        dx = x[i+1] - x[i];
        if (dx<0) throw new ArgumentException("x must be a sorted list."); // Unlikely to happen as the binary search would not terminate?
        dy = y[i+1] - y[i];
        double x_prime = x_end-x[i];
        return ans + x_prime * (y[i] + dy/dx*x_prime/2.0);
        
    }
    public static int binsearch(double[] x, double x_new) {
        int range_start = 0; int range_end = x.Length-1;
        while (range_end-range_start>1) {
            int range_mid_point = (range_end+range_start)/2;
            if (x[range_mid_point] <= x_new) range_start = range_mid_point;
            else if (x[range_mid_point] > x_new) range_end = range_mid_point; 
            else throw new ArgumentException($"Unable to compare new point to point {range_mid_point} in array. Is either nan?\n x[{range_mid_point}]: {x[range_mid_point]}, new point: {x_new}.");
        }
        return range_start;
    }
}

class qspline {
    vector x, y, b, c;
    public qspline(vector xs, vector ys) {
        x = xs.copy(); y = ys.copy();
        b = new vector(3);
        c = new vector(3);        
    }
    public double evaluate(double z){/* evaluate the spline */
        return 0; 
    }
	public double derivative(double z){/* evaluate the derivative */
        return 0;
    }
	public double integral(double z){/* evaluate the integral */
        return 0;
    }
}

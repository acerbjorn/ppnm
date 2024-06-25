using System;
using static System.Math;
using System.Collections.Generic;

public class rk12 {
    public static (vector, vector) rkstep12(
        Func<double, vector, vector> f,
        double x,
        vector y,
        double h
    ) {
        vector k0 = f(x,y);              /* embedded lower order formula (Euler) */
        vector k1 = f(x+h/2,y+k0*(h/2)); /* higher order formula (midpoint) */
        vector yh = y+k1*h;              /* y(x+h) estimate */
        vector y_err = (k1-k0)*h;        /* error estimate */
        return (yh,y_err);
    }
    public static (List<double>, List<vector>) driver(
    	Func<double,vector,vector> F,/* the f from dy/dx=f(x,y) */
    	(double,double) interval,    /* (start-point,end-point) */
    	vector ystart,               /* y(start-point) */
    	double h=0.125,              /* initial step-size */
    	double acc=0.01,             /* absolute accuracy goal */
    	double eps=0.01             /* relative accuracy goal */
    ) {
    var (a,b)=interval; double x=a; vector y=ystart.copy();
    var xlist=new List<double>(); xlist.Add(x);
    var ylist=new List<vector>(); ylist.Add(y);
    do{
            if(x>=b) return (xlist,ylist); /* job done */
            if(x+h>b) h=b-x;               /* last step should end at b */
            var (yh,δy) = rkstep12(F,x,y,h);
            double tol = (acc+eps*yh.norm()) * Sqrt(h/(b-a));
            double err = δy.norm();
            if(err<=tol){ // accept step
        		x+=h; y=yh;
        		xlist.Add(x);
        		ylist.Add(y);
    		}
        	h *= Min( Pow(tol/err,0.25)*0.95 , 2); // readjust stepsize
        } while(true);
    }//driver

    // public static Func<double,vector> make_linear_interpolant(List<double> x,List<vector> y)
    // {
    // 	Func<double,vector> interpolant = delegate(double z){
    // 		int i=binsearch(x,z);
    // 		double Δx=x[i+1]-x[i];
    // 		vector Δy=y[i+1]-y[i];
    // 		return y[i]+Δy/Δx*(z-x[i]);
    // 	};
    // 	return interpolant;
    // }

    public static Func<double,vector> make_ode_ivp_interpolant(
        Func<double, vector, vector> f,
        (double,double) interval,
        vector y,
        double h=0.125,
        double acc=0.01,
        double eps=0.01
    ) {
    	var (xlist,ylist) = driver(
            f,
            interval,
            y,
            h:h, 
            acc:acc,
            eps:eps
        );
    	return interpolant.linear(xlist,ylist);    
    }
}

public static class interpolant {
    public static Func<double,vector> linear(List<double> x,List<vector> y)
    {
    	Func<double,vector> f = delegate(double z){
    		int i=binsearch(x,z);
    		double Δx=x[i+1]-x[i];
    		vector Δy=y[i+1]-y[i];
    		return y[i]+Δy/Δx*(z-x[i]);
    	};
    	return f;
    }
    public static int binsearch(List<double> x, double x_new) {
        int range_start = 0; int range_end = x.Count-1;
        while (range_end-range_start>1) {
            int range_mid_point = (range_end+range_start)/2;
            if (x[range_mid_point] <= x_new) range_start = range_mid_point;
            else if (x[range_mid_point] > x_new) range_end = range_mid_point; 
            else throw new ArgumentException($"Unable to compare new point to point {range_mid_point} in array. Is either nan?\n x[{range_mid_point}]: {x[range_mid_point]}, new point: {x_new}.");
        }
        return range_start;
    }
}

public class euler {
    public static vector step(
        Func<double, vector, vector> f,
        double x,
        vector y,
        double h
    ) {
        vector k0 = f(x,y);              /* embedded lower order formula (Euler) */
        vector yh = y+k0*h;              /* y(x+h) estimate */
        return yh;
    }
    public static (List<double>, List<vector>) driver(
    	Func<double,vector,vector> F,/* the f from dy/dx=f(x,y) */
    	(double,double) interval,    /* (start-point,end-point) */
    	vector ystart,               /* y(start-point) */
    	double h=0.125              /* initial step-size */
    ) {
        var (a,b)=interval; double x=a; vector y=ystart.copy();
        var xlist=new List<double>(); xlist.Add(x);
        var ylist=new List<vector>(); ylist.Add(y);
        do{
            if(x>=b) return (xlist,ylist); /* job done */
            if(x+h>b) h=b-x;               /* last step should end at b */
            var yh = step(F,x,y,h);
    		x+=h; y=yh;
    		xlist.Add(x);
    		ylist.Add(y);
        } while(true);
    }//driver
    public static Func<double,vector> make_ode_ivp_interpolant(
        Func<double, vector, vector> f,
        (double,double) interval,
        vector y,
        double h=0.125
    ) {
    	var (xlist,ylist) = driver(
            f,
            interval,
            y,
            h:h 
        );
    	return interpolant.linear(xlist,ylist);    
    }
}

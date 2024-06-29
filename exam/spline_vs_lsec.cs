using System;
using static System.Math;
using System.Collections.Generic;

class spline_vs_lsec {
    static void Main() {
        Console.WriteLine($"Error Rate\tdx\tlsec\tspline");
        vector err_rates = vector.linspace(0,0.5,50);
        foreach (double err_rate in err_rates) {
            var (lsec_err, spline_err) = test_case(wavelet, 4, err_rate, 0.1);
            Console.WriteLine($"{err_rate}\t{lsec_err}\t{spline_err}");
        }
    }
    static (vector, vector) remove_errs(vector x, vector y) {
        List<double> xpl = new List<double>();
        List<double> ypl = new List<double>();
        for (int i=0; i<y.size; i++) {
            if (0!=y[i]) {
                xpl.Add(x[i]); ypl.Add(y[i]);
            } 
        }
        vector xp = new vector(xpl.ToArray());
        vector yp = new vector(ypl.ToArray());
        return (xp, yp);
    }
    static double test_spline(vector x, vector y, vector y_err) {
        var (xp, yp) = remove_errs(x, y_err);
        qspline spline = new qspline(xp,yp);
        double squared_err = 0;
        for (int i = 0; i<x.size; i++) {
            squared_err += Pow(y[i]-spline.evaluate(x[i]),2);
        }
        return Sqrt(squared_err);
    }
    static double test_lsec(vector x, vector y, vector y_err) {
        lsec ec = new lsec(y_err, (double yi) => yi==0);
        double squared_err = 0;
        for (int i = 0; i<x.size; i++) {
            squared_err += Pow(y[i]-ec.x[i],2);
        }
        return Sqrt(squared_err);
    }
    static (double, double) test_case(
        Func<double, vector, double> f, 
        int parameter_counts,
        double error_rate, 
        double dx,
        int iterations = 50
    ) {
        double lsec_err = 0, spline_err = 0;
        for (int i = 0; i<iterations; i++) {
            vector p = vector.rnd(parameter_counts,-1,1);
            vector x = vector.linspace(-10,10,spacing:dx);
            vector y = x.map((double xi) => f(xi,p));
            vector y_err = deteriorate(y, error_rate);
            lsec_err += test_lsec(x,y,y_err)/iterations;
            spline_err += test_spline(x,y,y_err)/iterations;
        }
        return (lsec_err, spline_err);
    }
    // static void test_case_random_sampling(
    //     Func<double, vector, double> f, 
    //     int parameter_counts,
    //     int iterations = 1000
    // ) {
    //     double lsec_err = 0, spline_err = 0;
    //     for (int i = 0; i<iterations; i++) {
    //         vector p = vector.rnd(parameter_counts,-1,1);
    //         vector x = vector.linspace(-10,10,spacing:dx);
    //         vector y = x.map((double xi) => f(xi,p));
    //         vector y_err = deteriorate(y, error_rate);
    //         lsec_err += test_lsec(x,y,y_err)/iterations;
    //     }
    // }
    public static vector deteriorate(
        vector original, 
        double error_rate, 
        double error_substitute = 0,
        Random rng=null
    ) {
        if (rng==null) {
            rng = new Random();
        }
        vector copy = original.copy();
        for (int i = 0; i<copy.size; i++) {
            if (rng.NextDouble()<error_rate) copy[i] = error_substitute;
        } 
        return copy;
    }
    static public double wavelet(double x, vector p) {
        return Cos(10*(p[0]*x+p[1]))*Exp(-x*x+10*p[3])*5*p[2];
    }
}

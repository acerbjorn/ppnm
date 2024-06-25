using System;
using static qspline;
using static System.Math;
using static System.Console;
using static matrix;

class qspline_sin {
    static int Main(string[] args) {
        double start = 0;
        double end = 10;
        double input_dx = 1;
        double output_dx = 1.0/8.0;
        // WriteLine($"{output_dx}");

        vector xhat = vector.linspace(start, end+1, spacing:input_dx);
        vector yhat = xhat.map(Sin);
        // vector.linspace(start, end, spacing:output_dx).print();        
        vector x_prime = vector.linspace(start, end, spacing:output_dx);
        vector x = xhat; 
        vector y = yhat;
        qspline f_p = new qspline(x,y); 
        WriteLine("#x\tsin(x)");
        foreach (double x_i in x) {
            WriteLine($"{x_i}\t{Sin(x_i):N4}");
        }
        Write("\n\n");

        
        WriteLine("#x\tsin(x)\tf_prime(x)\t");
        foreach (double x_i in x_prime) {
            WriteLine($"{x_i:N4}\t{Sin(x_i)}\t{f_p.evaluate(x_i):N4}"); 
        }
        return 0;
    }
}

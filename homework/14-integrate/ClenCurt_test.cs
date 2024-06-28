using System;
using static System.Math;

class Clenshaw_Curtis_test {
    public static void Main() {
        Console.WriteLine($"Evaluating f(x)=1/sqrt(x)\t        result\t\t\titerations");
        Console.WriteLine($"\texpected:                       2");
        f_iter f = new f_iter((double x) => 1/Sqrt(x));
        double original = integrate.adaptive(f.eval, 0, 1);
        int orig_iter = f.iter; f.iter = 0;
        Console.WriteLine($"\twithout transformation:         {original}\t{orig_iter}");
        double clencurt = integrate.adaptive(transformation.clenshaw_curtis(f.eval, 0, 1));
        int clcu_iter = f.iter;
        Console.WriteLine($"\tClenshaw-Curtis transformation: {clencurt}\t{clcu_iter}");
        
        Console.WriteLine($"Evaluating g(x)=ln(x)/sqrt(x)");
        Console.WriteLine($"\texpected:                       -4");
        f_iter g = new f_iter((double x) => Log(x)/Sqrt(x));
        original = integrate.adaptive(g.eval, 0, 1);
        orig_iter = g.iter; g.iter = 0;
        Console.WriteLine($"\twithout transformation:         {original}\t{orig_iter}");
        clencurt = integrate.adaptive(transformation.clenshaw_curtis(g.eval, 0, 1));
        clcu_iter = g.iter;
        Console.WriteLine($"\tClenshaw-Curtis transformation: {clencurt}\t{clcu_iter}");
    }
}

public class f_iter {
    public Func<double, double> eval;
    public int iter;
    public f_iter(Func<double,double> f) {
        iter = 0;
        eval = (double x) => {iter++; return f(x);};
    }
}

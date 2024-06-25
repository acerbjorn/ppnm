using static monte_carlo;
using static matrix;
using System;

class unit_circle {
    static void Main(string[] args) {
        int nterms=(int)1e7;
        bool verbose = false;
        string sampling_method = "pseudo";
        foreach(string arg in args){
            var words = arg.Split(":");
            if (words[0]=="-nterms") nterms = (int)double.Parse(words[1]);
            if (words[0]=="-v") verbose = true; 
            if (words[0]=="-method") sampling_method = words[1]; 
        }

        // Parameters
        Func<vector,double> f = (vector x) => {
            if (x.norm()<=1.0) return 1;
            else return 0;
        };
        double[] starts = {-1.0,-1.0};
        double[] ends = {1.0,1.0};

        
        vector a = new vector(starts), b = new vector(ends);
        double result = 0, sigma = 0;
        switch (sampling_method) {
            case "halton":
                if (verbose) Console.Write($"");
                (result, sigma) = monte_carlo.halton_integ(f,a,b,nterms);
                break;
            default:
                (result, sigma) = monte_carlo.pseudo_random_integ(f,a,b,nterms);
                break;
        }
        if (verbose) Console.Write($"Area of unit-circle: ");
        Console.WriteLine($"{result}\t{sigma}");    
    }
}

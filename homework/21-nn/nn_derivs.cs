using System;
using static ann; 
using static System.Math;

class nn_approximation {
    static void Main() {
        int seed = 6; // preseeding
        Random rng = new Random(seed);
        vector x_obs = vector.linspace(-1,1,100);
        // Console.Error.Write($"x observations: {x_obs}");
        vector y_obs = x_obs.map(g);
        ann nn = new ann(5, rng: rng);
        nn.train(x_obs, y_obs);
        vector xs = vector.linspace(-1,1,200);
        Console.WriteLine("x\tg(x)\tF_p(x)\tAnti-derivative(nn)\tFirst-derivative(nn)\tSecond-derivative(nn)");

        foreach (double x in xs) {
            Console.Write($"{x:F3}\t{g(x):F3}");
            Console.Write($"\t{nn.response(x):F3}");
            Console.Write($"\t{nn.anti_deriv_response(x):F3}");
            Console.Write($"\t{nn.first_deriv_response(x):F3}");
            Console.Write($"\t{nn.second_deriv_response(x):F3}");
            Console.Write("\n");
        }
    }
    static double g(double x) {
        return Cos(5*x-1)*Exp(-x*x);
    }
}

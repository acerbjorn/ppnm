using System;
using static ann; 
using static System.Math;

class nn_approximation {
    static void Main() {
        int seed = 1; // preseeding
        int nn_count = 4, n_offset = 1, step_size = 2;
        vector x_obs = vector.linspace(-1,1,100);
        // Console.Error.Write($"x observations: {x_obs}");
        vector y_obs = x_obs.map(g);
        ann[] nn_list = new ann[nn_count];
        
        for (int i = 0; i<nn_count; i++ ) {
            var rng = new Random(seed); // preseeded random to have consistent runs for preinitialized weights
            Console.Error.WriteLine($"\nTraining a {i*step_size+n_offset}-neuron network");
            nn_list[i] = new ann(i*step_size+n_offset, rng:rng);
            nn_list[i].train(x_obs, y_obs);
        }
        vector xs = vector.linspace(-2,2,250);
        Console.Write("x\tg(x)");
        for (int i = 0; i<nn_count; i++ ) {
            Console.Write($"\tNeurons:{i*step_size+n_offset}");
        } Console.Write("\n");

        foreach (double x in xs) {
            Console.Write($"{x:F3}\t{g(x):F3}");
            for (int i = 0; i<nn_count; i++ ) {
                Console.Write($"\t{nn_list[i].response(x):F3}");
            } Console.Write("\n");
        }
    }
    static double g(double x) {
        return Cos(5*x-1)*Exp(-x*x);
    }
}

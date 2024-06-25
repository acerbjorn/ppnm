using System;
using static System.Math;
using static matrix;
using static EVD;
using static hamiltonian;

class hydrogen_atom {
    static void Main(string[] args) {
        double r_max = 10.0;
        double r_min = 1e-1;
        double dr = 1e-1;
        int n_return = 1;
        bool return_wavefunction = false;
        foreach(string arg in args) {
            string[] words = arg.Split(":");
            if (words[0] == "-dr") dr = double.Parse(words[1]);
            if (words[0] == "-r_max") r_max = double.Parse(words[1]);
            if (words[0] == "-r_min") r_min = double.Parse(words[1]);
            if (words[0] == "-n") n_return = (int)double.Parse(words[1]);
            if (words[0] == "-w") return_wavefunction = true;
        }
        matrix H; vector r;
        r = vector.linspace(r_min, r_max, spacing:dr);
        H = construct_hamiltonian(r,  (double x) => -1/x);
        // H.print("H:", "\t");
        // r.print("r:");
        EVD eigs = new EVD(H);

        // Assert that Eigenvalues are sorted?
        // for (int i = 0; i<eigs.w.size; i++) {
        //     Console.Error.WriteLine($"eigs.w[{i}]={eigs.w[i]}");
        // }

        // Print energies and eigenfunctions
        // norm
        if (return_wavefunction) System.Console.Write($"r");
        for (int i=0; i<n_return; i++) {
            System.Console.Write($"\t{eigs.w[i].ToString("G5")}");
        }
        System.Console.Write("\n");
        if (return_wavefunction) {
            int n_points = (int)(r_max/dr)-1;
            for (int i=0; i<n_points; i++) {
                System.Console.Write($"{r[i]}");
                for (int j=0; j<n_return; j++) {
                    System.Console.Write($"\t{(eigs.V[i,j]/Sqrt(dr)).ToString("G5")}");
                }
                System.Console.Write("\n");
            }
        }
    }
}

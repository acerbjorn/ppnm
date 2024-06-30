using System;
using static System.Math;
using static matrix;
using static EVD;
using static hamiltonian;

class hydrogen_atom {
    static void Main(string[] args) {
        double r_max = 10.0;
        double dr = 1e-1;
        int n_return = 1;
        bool return_wavefunction = false;
        foreach(string arg in args) {
            // Console.Error.Write(arg);
            string[] words = arg.Split(":");
            if (words[0] == "-dr") dr = double.Parse(words[1]);
            if (words[0] == "-r_max") r_max = double.Parse(words[1]);
            if (words[0] == "-n") n_return = (int)double.Parse(words[1]);
            if (words[0] == "-w") return_wavefunction = true;
        }
        matrix H; vector r;
        r = vector.linspace(dr, r_max, spacing:dr);
        // Console.Error.Write(r);
        H = construct_hamiltonian(r,  (double x) => -1/x);
        // Console.Error.Write("H constructed.");
        // H.print("H:", "\t");
        // r.print("r:");
        EVD eigs = new EVD(H);
        // Console.Error.Write("EVD done");

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
            int n_points = eigs.V.size1;
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

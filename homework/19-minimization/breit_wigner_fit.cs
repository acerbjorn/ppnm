using System;
using static System.Math;
using static genlist<double>;
using static min;


class breit_wigner_fit {
    static void Main() {
        // Dmitris standard-in reader
        var energy_list = new genlist<double>();
        var signal_list = new genlist<double>();
        var error_list  = new genlist<double>();
        var separators = new char[] {' ','\t'};
        var options = StringSplitOptions.RemoveEmptyEntries;
        do{
                string line=Console.In.ReadLine();
                if(line==null)break; // finish when stdin ends
                if(line[0] == '#') continue; // ignore commented out lines
                string[] words=line.Split(separators, options);
                energy_list.add(double.Parse(words[0]));
                signal_list.add(double.Parse(words[1]));
                error_list.add(double.Parse(words[2]));
        } while(true);

        
        vector energy = new vector(energy_list.to_array());
        vector signal = new vector(signal_list.to_array());
        vector error = new vector(error_list.to_array());

        // energy.print("energy: ");
        // signal.print("signal: ");
        // error.print("error: ");
        
        vector p0 = new vector(new double[]{
            127, 2, 10 // starting guess of mass, Gamma, A
        });
        // Console.WriteLine($"p0 = {p0},\nF(p0) = {breit_wigner(125,p0[0],p0[1],p0[2])}\nD(p0) = {D(p0, energy, signal, error)}");
        vector p = min.newton(
            (vector guess) => D(guess, energy, signal, error),
            p0
        );
        Console.WriteLine($"m = {p[0]};");
        Console.WriteLine($"Gamma = {p[1]};");
        Console.WriteLine($"A = {p[2]};");
    }
    public static double breit_wigner(double E, double m, double Gamma, double A) {
        return A/( Pow(E-m, 2) + Pow(Gamma, 2)/4 );
    }
    static double D(vector guess, vector energy, vector signal, vector error) {
        // Console.WriteLine($"guess: {guess}");
        double sum = 0;
        double F;
        int n = energy.size;
        for (int i = 0; i<n; i++) {
            F = breit_wigner(energy[i], guess[0], guess[1], guess[2]);
            sum += Pow( ( F-signal[i] )/error[i] ,2);
        }
        // Console.WriteLine($"sum: {sum}\n");
        return sum;
    }
}

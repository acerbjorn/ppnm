using System;
using static System.Math;

class split_test{
    static int Main(string[] args) {
        int sample_rate = 1000;
        double error_rate = 0.05;
        bool chunked = true;
        foreach(string arg in args) {
            string[] words = arg.Split(":");
            if (words[0] == "-sample_rate") sample_rate = (int)double.Parse(words[1]);
            if (words[0] == "-error_rate") error_rate = double.Parse(words[1]);
            if (words[0] == "-no_chunk") chunked = false;
        }
        vector time = vector.linspace(0,1,sample_rate);
        vector y = time.map(A440);
        vector y_err = deteriorate(y, error_rate);
        lsec y_recon;
        if (chunked) {
            signal_splitter yl = new signal_splitter(y_err, (double yi) => yi == 0);
            y_recon = new lsec(yl,(double yi) => yi == 0);
        } else {
            y_recon = new lsec(y_err,(double yi) => yi == 0);
        }
        double error = 0;
        for (int i = 0; i<y.size; i++) {
            error += Pow(y_recon.x[i]-y[i],2);
        }
        if (vector.approx(y_recon.x, y,5e-2,5e-2)) {
            return 0;
        } else {
            Console.Error.Write($"Reconstruction error = {Sqrt(error)}\n");
            return 1;
        }
    }
    static double A440(double x) {
        return Cos(440*x/PI);
    }
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
}

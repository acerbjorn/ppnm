using static matrix;
using System;
using static System.Math;

public class monte_carlo {
    static int[] primes =  	{2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271};
    public static (double, double) pseudo_random_integ(
        Func<vector,double> f,
        vector a, 
        vector b, 
        int N
    ) {
        int dims = a.size; 
        double sum = 0; double sum_squared = 0; double fx = 0;
        vector lengths = b-a;
        double volume = 1;
        for (int i = 0; i<dims; i++) volume *= lengths[i];
        vector random_point = new vector(dims);
        var rnd = new Random(); 
        for (int measurement=0; measurement<N; measurement++) {
            for (int i=0; i<dims; i++) {
                random_point[i] = a[i] + rnd.NextDouble()*lengths[i];
            }
            fx = f(random_point); sum += fx; sum_squared += fx*fx;
        }
        double mean = sum/N; 
        double sigma = Sqrt( (sum_squared/N) - mean*mean );
        var result = (mean*volume,sigma*volume/Sqrt(N));
        return result;
    }

    public static (double, double) halton_integ(
        Func<vector,double> f,
        vector a, 
        vector b, 
        int N
    ) {
        int dims = a.size; 
        double sum1 = 0; double sum2 = 0; 
        vector lengths = b-a;
        double volume = 1;
        for (int i = 0; i<dims; i++) volume *= lengths[i];
        vector random_point1 = new vector(dims);
        vector random_point2 = new vector(dims);
        for (int n=0; n<N; n++) {
            for (int i=0; i<dims; i++) {
                random_point1[i] = a[i] + corput(n,primes[i])*lengths[i];
                random_point2[i] = a[i] + corput(n,primes[i+dims])*lengths[i];
            }
            sum1 += f(random_point1);
            sum2 += f(random_point2);
        }
        double mean = sum1/N; 
        double sigma = (sum1-sum2)/N;
        var result = (mean*volume,sigma*volume);
        return result;
    }

    public static (double, double) stratified_integ(
        Func<vector,double> f, 
        vector a, 
        vector b, 
        int N,
        int n_min=500
    ) {
        if (N <= n_min) {
            return pseudo_random_integ(f,a,b,N);
        }
        int dims = a.size;
        matrix sums_le = new vector(dims), sums_le_squared = new vector(dims);
        matrix sums_g = new vector(dims), sums_g_squared = new vector(dims);
        double fx = 0, fx_squared = 0;
        vector lengths = b-a;
        vector midpoints = (b+a)/2;
        double volume = 1;
        for (int i = 0; i<dims; i++) volume *= lengths[i];
        vector random_point = new vector(dims);
        var rnd = new Random(); 
        for (int measurement=0; measurement<n_min; measurement++) {
            for (int i=0; i<dims; i++) {
                random_point[i] = a[i] + rnd.NextDouble()*lengths[i];
            }
            fx = f(random_point); fx_squared = fx*fx;
            for (int i=0; i<dims; i++) {
                if (midpoints[i] >= random_point[i]) 
            }
        }
        double mean = sum/N; 
        double sigma = Sqrt( (sum_squared/N) - mean*mean );
        var result = (mean*volume,sigma*volume/Sqrt(N));
        return result;
    }    
    
    static double corput(int n, int b) {
        double q=0, bk=(double)1/b ;
        while (n>0){ q += (n % b)*bk ; n /= b ; bk /= b ; }
        return q; 
    }
    
}


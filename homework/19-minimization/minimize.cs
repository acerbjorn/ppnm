using System;
using static System.Math;
using static matrix;
using static QRGS;

public class min {
    public static vector newton(
    	Func<vector,double> f, /* objective function */
    	vector x_start,        /* starting point */
    	double acc=1e-3,       /* accuracy goal, on exit |∇φ| should be < acc */
        double lambda_min = 1/1024,
        int max_iter = 2000,
        bool verbose = false,
        double dx_exponent = -20
    ){
        int iterations = 0;
        vector x = x_start.copy();
        vector grad;
    	do { /* Newton's iterations */
            hessian H = hessian.central_difference_upper_triangular(f,x,dx_exponent);
            grad = H.gradient;
    		if(grad.norm() < acc) break; /* job done */
    		var QRH = new QRGS(H);   /* QR decomposition */
    		var dx = QRH.solve(-grad); /* Newton's step */
            if (dx.norm() < acc) break; // If the step length is below our accuracy we're not stepping far?
    		double lambda=1,fx=f(x);
    		do { /* linesearch */
    			if( f(x+lambda*dx) < fx ) break; /* good step: accept */
    			lambda/=2;
    		} while(lambda > lambda_min);
    		x+=lambda*dx;
            iterations++;
    	} while(iterations < max_iter);
        if (iterations>=max_iter) {
            Console.Error.WriteLine($"Minimization terminated after reaching max_iter = {max_iter}. \nSolution did not converge.");
            Console.Error.WriteLine($"|∇f| = {grad.norm()}\nx_start\t= {x_start}\nx_end\t= {x}");
        }
    	return x;
    }
    
    // ------------DOWNHILL SIMPLEX---------
    public static vector downhill_simplex(
    	Func<vector,double> f,     /* objective function */
    	vector x_start,            /* starting point */
        double start_size = 1e-1,  // starting size
    	double acc=1e-15,           /* accuracy goal, on exit |∇φ| should be < acc */
        int max_iter = 2000,        // maximum iterations
        bool verbose = false,
        bool loop_verbose = false
    ) {
        int iterations = 0;
        simplex min_simplex = new simplex(f, x_start, start_size, verbose: verbose);
        double current_size = 0;
        do {
            iterations++;
            if (loop_verbose) Console.Error.Write($"it{iterations}");
            min_simplex.downhill_move(verbose:loop_verbose);
            current_size = min_simplex.calculate_size();
            if (loop_verbose) {
                Console.Error.Write($"\n");
                min_simplex.print_state();
            }
            if (current_size < acc) break;
        } while (iterations != max_iter);
        if (iterations==max_iter) Console.Error.WriteLine($"Minimization terminated after reaching max_iter = {max_iter}. \nSolution did not converge in time.\nsimplex size = {current_size}");
        if (verbose) Console.Error.WriteLine($"Minimization terminated after {iterations} iterations. \nFinal simplex centroid at {min_simplex.calculate_centroid()}, with size {current_size}\nFinal State:\n");
        if (verbose) min_simplex.print_state();
        return min_simplex.calculate_centroid();
    }
}

public class simplex {
    int n;
    vector[] points;
    double[] fp;
    Func<vector,double> f;
    
    public simplex(
        Func<vector,double> f, 
        vector start, 
        double dr, 
        bool verbose = false
    ): this(f, classic_simplex_shape(start, dr), verbose) {}
    
    public simplex(
        Func<vector,double> f, 
        vector start, 
        double dr, 
        Random rng, 
        bool verbose = false
    ): this(f, random_simplex_shape(start, dr, rng), verbose) {}
    
    public simplex(
        Func<vector,double> f, 
        vector[] starting_points, 
        bool verbose = false
    ) {
        // Base constructor
        this.f = f;
        n = starting_points.Length-1;
        
        points = starting_points;
        fp = new double[n+1];
        
        for (int i = 0; i < n+1; i++) {
            fp[i] = f(points[i]);
        }
        if (verbose) print_state();// Base constructor    
    }
    static vector[] classic_simplex_shape(vector start, double dr) {        
        int n = start.size;
        vector[] points = new vector[n+1];
        points[0] = start.copy();
        
        for (int i = 1; i < n+1; i++) {
            points[i] = start + dr*vector.basis(n,i-1);
        }
        return points;
    }    
    static vector[] random_simplex_shape(vector start, double dr, Random rng) {        
        int n = start.size;
        vector[] points = new vector[n+1];
        
        for (int i = 1; i < n+1; i++) {
            vector displacement = vector.rnd(n,min:-1,max:1, rng:rng);
            points[i] = start + displacement;
        }
        return points;
    }    
    public vector calculate_centroid() {
        vector new_centroid = new vector(n);
        for (int i=0; i<n+1; i++) {
            new_centroid += points[i]/(n+1);
        }
        return new_centroid;
    }
    public vector calculate_centroid(int i_exclude) {
        vector new_centroid = new vector(n);
        for (int i=0; i<n+1; i++) {
            if (i!=i_exclude) new_centroid += points[i]/n;
        }
        return new_centroid;
    }
    (int, int) find_extreme_points() {
        int i_hip = 0, i_lop = 0;
        for (int i = 1; i < n+1; i++) {
            if (fp[i] < fp[i_lop]) i_lop = i;
            else if (fp[i] > fp[i_hip]) i_hip = i; 
        }
        return (i_hip, i_lop);
    }
    public void downhill_move(bool verbose=false) {
        var (i_hi, i_lo) = find_extreme_points(); 
        vector centroid = calculate_centroid(i_hi);
        if (fp[i_hi] < fp[i_lo]) {
            Console.Error.WriteLine($"weird behaviour, printing state");
            print_state();
        }
        vector centroid_displacement = centroid - points[i_hi];
        vector reflected_point = centroid + centroid_displacement;
        double reflected_fp = f(reflected_point);
        if (reflected_fp < fp[i_hi]) {
            if (verbose) Console.Error.WriteLine($"f(p_{i_hi})>f(p_re)={reflected_fp}");
            if (reflected_fp < fp[i_lo]) {
                vector expanded_point = reflected_point + centroid_displacement;
                double expanded_fp = f(expanded_point);
                if (expanded_fp < reflected_fp) {
                    if (verbose) Console.Error.WriteLine($"f(p_re)>f(p_ex)={expanded_fp}");
                    // accept expansion
                    if (verbose) Console.Error.WriteLine($"ex{i_hi}");
                    points[i_hi] = expanded_point; fp[i_hi] = expanded_fp;
                    return;
                }
            };
            // accept reflection
            if (verbose) Console.Error.WriteLine($"re{i_hi}");
            points[i_hi] = reflected_point; fp[i_hi] = reflected_fp;
        } else {
            // either contraction or reduction
            vector contracted_point = centroid - 0.5*centroid_displacement;
            double contracted_fp = f(contracted_point);
            if (contracted_fp < fp[i_hi]) {
                // accept contraction
                if (verbose) Console.Error.WriteLine($"co{i_hi}");
                points[i_hi] = contracted_point; fp[i_hi] = contracted_fp;
            } else {
                // nothing else works
                if (verbose) Console.Error.WriteLine($"rd{i_lo}");
                do_reduction(i_lo);
            };
        };
    }
    void do_reduction(int i_lo) {
        for (int i=0; i<n+1; i++) {
            if (i != i_lo) {
                points[i] = (points[i] + points[i_lo])/2;
                fp[i] = f(points[i]);
            }
        }
    }
    public double calculate_size(vector centroid=null) {
        // mean distance from centroid.
        double size = 0;
        if (centroid==null) centroid = calculate_centroid();
        for (int i=0; i < n+1; i++) size += (points[i]-centroid).norm()/(n+1.0);
        return size;
    }
    public void print_state() {
        Console.Error.WriteLine("Simplex points:");
        for (int i=0; i<n+1; i++) Console.Error.Write($"\tf{points[i]} = {fp[i]}\n");
        Console.Error.WriteLine($"p_ce = {calculate_centroid()}");
        Console.Error.WriteLine($"size = {calculate_size()}");
    }
}

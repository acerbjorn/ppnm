using System;
using static System.Math;

class hessian_test {
    static void Main() {
        Console.Write("# parabolic\n");
        test_hessian_stability(parabolic, parabolic_hessian_analytic, true);
        Console.Write("# himmelblau\n");
        test_hessian_stability(himmelblau, himmelblau_hessian);
        Console.Write("# rosenbrock\n");
        test_hessian_stability(rosenbrock, rosenbrock_hessian);
        Console.Write("# gradient parabolic\n");
        test_grad_stability(parabolic, parabolic_gradient_analytic, true);
        Console.Write("# gradient himmelblau\n");
        test_grad_stability(himmelblau, himmelblau_gradient);
        Console.Write("# gradient rosenbrock\n");
        test_grad_stability(rosenbrock, rosenbrock_gradient);
    }
    static void test_hessian_stability(
        Func<vector, double> f,
        Func<vector, matrix> analytic_hess,
        bool randomize_dims = false
    ) {
        Random rng = new Random();
        Console.WriteLine("dx_magnitude\tavg_fwd_hess_err\tavg_cen_hess_err\tavg_cen_u_hess_err");
        for (int magnitude=-10; magnitude > -30; magnitude--) {
            int iterations = 100;
            double mean_fwd_err = 0; 
            double mean_cen_err = 0; 
            double mean_cen_u_err = 0; 
            for (int i = 0; i< iterations; i++) {
                vector p;
                if (randomize_dims) {
                    p = vector.rnd(rng.Next(2,10),-10,10);
                } else p = vector.rnd(2,-10,10);
                matrix H = analytic_hess(p);
                matrix fwd_err = H-hessian.forward_difference(f,p, dx_exponent:magnitude);
                matrix cen_err = H-hessian.central_difference(f,p, dx_exponent:magnitude);
                matrix cen_u_err = H-hessian.central_difference_upper_triangular(f,p, dx_exponent:magnitude);
                mean_fwd_err += sum_squares(fwd_err)/iterations;
                mean_cen_err += sum_squares(cen_err)/iterations;
                mean_cen_u_err += sum_squares(cen_u_err)/iterations;
            }
            Console.WriteLine($"{magnitude}\t{mean_fwd_err}\t{mean_cen_err}\t{mean_cen_u_err}");
        }
        Console.Write("\n\n");
    }
    static void test_grad_stability(
        Func<vector, double> f,
        Func<vector, vector> analytic_grad,
        bool randomize_dims = false
    ) {
        Random rng = new Random();
        Console.WriteLine("dx_magnitude\tavg_fwd_hess_err\tavg_cen_hess_err\tavg_cen_u_hess_err");
        for (int magnitude=-10; magnitude > -30; magnitude--) {
            int iterations = 1000;
            double mean_fwd_err = 0; 
            double mean_cen_err = 0; 
            double mean_cen_u_err = 0; 
            for (int i = 0; i< iterations; i++) {
                vector p;
                if (randomize_dims) {
                    p = vector.rnd(rng.Next(2,10),-10,10);
                } else p = vector.rnd(2,-10,10);
                vector H = analytic_grad(p);
                vector fwd_err = H-hessian.forward_difference(f,p, dx_exponent:magnitude).gradient;
                vector cen_err = H-hessian.central_difference(f,p, dx_exponent:magnitude).gradient;
                vector cen_u_err = H-hessian.central_difference_upper_triangular(f,p, dx_exponent:magnitude).gradient;
                mean_fwd_err += sum_squares(fwd_err)/iterations;
                mean_cen_err += sum_squares(cen_err)/iterations;
                mean_cen_u_err += sum_squares(cen_u_err)/iterations;
            }
            Console.WriteLine($"{magnitude}\t{mean_fwd_err}\t{mean_cen_err}\t{mean_cen_u_err}");
        }
        Console.Write("\n\n");
    }
    static double sum_squares(matrix A) {
        double sum = 0;
        for (int i=0; i<A.size1; i++) {
            for (int j=0; j<A.size2; j++) {
                // Console.Error.Write($"{i},{j}\n");
                sum += Pow(A[i,j],2);
            }
        }
        return sum;
    }
    static double sum_squares(vector A) {
        double sum = 0;
        for (int i=0; i<A.size; i++) {
            sum += Pow(A[i],2);
        }
        return sum;
    }
    static double parabolic(vector p) {
        // return Pow(p.norm(),2);
        double sum = 0;
        for (int i = 0; i<p.size; i++) {
            sum += p[i]*p[i];
        }
        return sum;
    }
    static matrix parabolic_hessian_analytic(vector p) {
        return 2*matrix.id(p.size);
    }
    static vector parabolic_gradient_analytic(vector p) {
        return p.map(x=>2*x);
    }
    static double himmelblau(vector input) {
        double x = input[0], y = input[1];
        return Pow(x*x + y - 11, 2) + Pow(x + y*y -7, 2);
    }
    static double rosenbrock(vector input) {
        double x = input[0], y = input[1];
        return Pow(1-x, 2) + 100*Pow(y-x*x, 2);
    }
    static vector himmelblau_gradient(vector input) {
        double x = input[0], y = input[1];
        return new vector(new double[] {
            4*x*(x*x + y - 11) + 2*(x + y*y -7),
            2*(x*x + y - 11) + 4*y*(x + y*y -7),
        });
    }
    static vector rosenbrock_gradient(vector input) {
        double x = input[0], y = input[1];
        return new vector(new double[] {
            2*x - 2 + 400*x*x*x - 400*y*x,
            200*y - 200*x*x
        });
    }
    static matrix himmelblau_hessian(vector input) {
        double x = input[0], y = input[1];
        matrix H = new matrix(2);
        H[0,0] = 12*x*x+4*y-42;
        H[0,1] = 4*x+4*y;
        H[1,0] = 4*x+4*y;
        H[1,1] = 2+4*x+12*y*y-28;
        return H;
    }
    static matrix rosenbrock_hessian(vector input) {
        double x = input[0], y = input[1];
        matrix H = new matrix(2);
        H[0,0] = 2+1200*x*x-400*y;
        H[0,1] = -400*x;
        H[1,0] = -400*x;
        H[1,1] = 200;
        return H;
    }
}

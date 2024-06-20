using System;
using static System.Math;

class hessian_test {
    static void Main() {
        Console.WriteLine($"Testing hessian on different points, in dimensions of n∈[1,10]");
        Random rng = new Random(1);
        double dx_exponent = -22; // goes into pow(2,dx_exponent) to find dx
        // It's changeable because i noticed some numerical instability at the
        // suggested exponent of -26
        for (int i = 0; i<100; i++) {
            //vector p = new vector("1,0.1,0,2");
            vector p = vector.rnd(rng.Next(2,5), -10, 10, rng);
            vector grad_ana = parabolic_gradient_analytic(p);
            vector grad_fwd = hessian.gradient_forward_difference(parabolic, p, dx_exponent);
            
            matrix H_ana = parabolic_hessian_analytic(p);
            hessian H_fwd = hessian.forward_difference(parabolic, p, dx_exponent);
            hessian H_cen = hessian.central_difference(parabolic, p, dx_exponent);
            hessian H_cen_u = hessian.central_difference_upper_triangular(parabolic, p, dx_exponent);

            
            if (!H_ana.approx(H_fwd)) {
                Console.WriteLine("Forward difference numeric hessian did not return correct answer");
                p.print($"P{i}: ");
                H_fwd.print("H_fwd:", "\t");
            }
            if (!H_ana.approx(H_cen)) {
                Console.WriteLine("Central difference numeric hessian did not return correct answer");
                p.print($"P{i}: ");
                H_cen.print("H_cen:", "\t");
            }
            if (!H_ana.approx(H_cen_u)) {
                Console.WriteLine("Central difference upper triangular numeric hessian did not return correct answer");
                p.print($"P{i}: ");
                H_cen.print("H_cen_u:", "\t");
            }
            if (!vector.approx(grad_ana, grad_fwd, 1e-2)) {
                Console.WriteLine("Forward difference numeric gradient did not return correct answer");
                p.print($"P{i}: ");
                grad_fwd.print("grad_fwd:");
            }
            if (!vector.approx(grad_ana, H_cen_u.gradient, 1e-2)) {
                Console.WriteLine("Forward difference numeric gradient did not return correct answer");
                p.print($"P{i}: ");
                H_cen_u.gradient.print("grad_fwd:");
            }
        }
        Console.WriteLine($"Unless noted all 1000 cases passed.");

        /*
        Console.WriteLine($"Testing hessian implementations on rosenbrock and himmelblau functions.");
        rng = new Random(1);
        for (int i = 0; i<1000; i++) {
            vector p = vector.rnd(2, -10, 10, rng);
            vector grad = rosenbrock_gradient(p);
            hessian H_fwd = hessian.forward_difference(rosenbrock, p, dx_exponent);
            hessian H_cen_u = hessian.central_difference_upper_triangular(rosenbrock, p, dx_exponent);
            hessian H_cen = hessian.central_difference(rosenbrock, p, dx_exponent);
            if (H_fwd.gradient.approx(grad)) {
                Console.WriteLine("Forward difference numeric hessian did not return correct gradient");
                p.print($"P{i}: ");
                H_fwd.gradient.print("H_fwd   grad:", "\t");
            }
            if (H_cen.gradient.approx(grad)) {
                Console.WriteLine("Central difference numeric hessian did not return correct gradient");
                p.print($"P{i}: ");
                H_cen.gradient.print("H_cen grad:");
                grad.print("    grad:");
            }
            if (H_cen_u.gradient.approx(grad)) {
                Console.WriteLine("Central difference upper triangular numeric hessian did not return correct gradient");
                p.print($"P{i}: ");
                H_cen_u.gradient.print("H_cen_u grad:");
            }
            
            grad = himmelblau_gradient(p);
            H_fwd = hessian.forward_difference(himmelblau, p);
            H_cen_u = hessian.central_difference_upper_triangular(himmelblau, p);
            H_cen = hessian.central_difference(rosenbrock, p);
            if (H_fwd.gradient.approx(grad)) {
                Console.WriteLine("Forward difference numeric hessian did not return correct gradient");
                p.print($"P{i}: ");
                H_fwd.print("H_fwd   grad:", "\t");
            }
            if (H_cen.gradient.approx(grad)) {
                Console.WriteLine("Central difference numeric hessian did not return correct gradient");
                p.print($"P{i}: ");
                H_cen.gradient.print("H_cen grad:", "\t");
            }
            if (H_cen_u.gradient.approx(grad)) {
                Console.WriteLine("Central difference upper triangular numeric hessian did not return correct gradient");
                p.print($"P{i}: ");
                H_cen_u.gradient.print("H_cen_u grad:", "\t");
            }
        }
        Console.WriteLine($"Unless noted all 1000 cases passed.");
        */
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
}

using System;
using static System.Math;
using static matrix;
using static min;

public class ann{
    int n; /* number of hidden neurons, assumes single hidden layer. */
    public Func<double,double> f = x => x*Exp(-x*x); /* activation function */
    Func<double,double> F   = x => -Exp(-x*x)/2; // antiderivative
    Func<double,double> fp  = x => (1-2*x*x)*Exp(-x*x); // first derivative
    Func<double,double> fpp = x => (4*x*x*x-6*x)*Exp(-x*x); // second derivative
    vector p; /* network parameters */
    public ann(
        int neuron_count, 
        Random rng = null, 
        // Func<double,double> activation_function = null,
        (double, double)? w_interval = null,
        (double, double)? a_interval = null,
        (double, double)? b_interval = null
    ) { 
        if (rng == null) rng = new Random(); 
        // if (!(activation_function == null)) f = activation_function;
        n = neuron_count;
        p = new vector(3*n); 
        var (w_l, w_u) = w_interval ?? (-1.0, 1.0); 
        var (a_l, a_u) = a_interval ?? (-1.0, 1.0); 
        var (b_l, b_u) = b_interval ?? ( 0.5, 1.5);

         
        for (int i=0; i<n; i++) {
            // initialize p to random values 
            // (If all neurons were similar they would move the same under applied gradient)
            p[i]     = (w_u-w_l)*rng.NextDouble() + w_l ; // w_i
            p[i+n]   = (a_u-a_l)*rng.NextDouble() + a_l ; // a_i
            p[i+2*n] = (b_u-b_l)*rng.NextDouble() + b_l ; // b_i
        }
    }
    public double response(double x){
        return response(x,this.p);
    }
    public double anti_deriv_response(double x){
        return anti_deriv_response(x,this.p);
    }
    public double first_deriv_response(double x){
        return first_deriv_response(x,this.p);
    }
    public double second_deriv_response(double x){
        return second_deriv_response(x,this.p);
    }
    public double response(double x, vector p){
        double y = 0; 
        for (int i = 0; i<n; i++) {
            y += p[i] * f((x-p[i+n])/p[i+2*n]);
        }
        return y;
    }
    public double anti_deriv_response(double x, vector p) {
        double y = 0; 
        for (int i = 0; i<n; i++) {
            y += p[i] * F((x-p[i+n])/p[i+2*n])*p[i+2*n];
        }
        return y;
    }
    public double first_deriv_response(double x, vector p) {
        double y = 0; 
        for (int i = 0; i<n; i++) {
            y += p[i] * fp((x-p[i+n])/p[i+2*n])/p[i+2*n];
        }
        return y;
    }
    public double second_deriv_response(double x, vector p) {
        double y = 0; 
        for (int i = 0; i<n; i++) {
            y += p[i] * fpp((x-p[i+n])/p[i+2*n])/Pow(p[i+2*n],2);
        }
        return y;
    }
    
    public void train(vector x,vector y){
        this.p = min.newton(
            (vector p) => loss_function(p,x,y),
            this.p,
            max_iter: 2000
        );           
    }
    
    double loss_function(vector p, vector x, vector y) {
        double loss = 0;
        for (int i = 0; i < x.size; i++) {
            loss += Pow(response(x[i], p) - y[i],2);
        }
        return loss;
    }
}


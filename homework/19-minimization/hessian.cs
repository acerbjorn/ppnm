using System;
using static System.Math;

public class hessian: matrix {
    public vector gradient; 
    public hessian(int n): base(n) {
        gradient = new vector(n);
    }
    public static hessian forward_difference(
        Func<vector,double> f,
        vector x,
        double dx_exponent = -26
    ){
    	hessian H=new hessian(x.size);
    	H.gradient = gradient_forward_difference(f,x, dx_exponent);
    	for(int j=0;j<x.size;j++){
    		double dx=Max(Abs(x[j]),1)*Pow(2, dx_exponent); /* for numerical gradient */
    		x[j]+=dx;
            // Console.Error.WriteLine($"x+dx    : {x}");
            // Console.Error.WriteLine($"dx      : {dx}");
            // Console.Error.WriteLine($"∇f(x+dx): {gradient_forward_difference(f,x)}");
            // Console.Error.WriteLine($"∇f(x)   : {H.gradient}");
    		vector H_j =gradient_forward_difference(f,x, dx_exponent)-H.gradient;
            // Console.Error.WriteLine($"H_j     : {H_j}");
    		for(int i=0;i<x.size;i++) H[i,j]=H_j[i]/dx;
    		x[j]-=dx;
            // Console.Error.WriteLine($"x       : {x}\n");
    	}
        H.symmetrize();
    	//return H; // Numeric calculation does not guarantee symmetric H?
    	return H; 
    }

    public static hessian central_difference(
        Func<vector,double> f,
        vector x,
        double dx_exponent = -26
    ) {
        int n = x.size;
    	hessian H=new hessian(n);
        
        vector x_pp = x.copy();
        vector x_pm = x.copy();
        vector x_mp = x.copy();
        vector x_mm = x.copy();
        
        for (int j = 0; j < n; j++) {       
    		double dxj=Max(Abs(x[j]),1)*Pow(2, dx_exponent);
            x_pp[j] += dxj;
            x_pm[j] += dxj;
            x_mp[j] -= dxj;
            x_mm[j] -= dxj;
            for (int k = 0; k < n; k++) {
        		double dxk=Max(Abs(x[k]),1)*Pow(2, dx_exponent);                
                x_pp[k] += dxk;
                x_pm[k] -= dxk;
                x_mp[k] += dxk;
                x_mm[k] -= dxk;
                
                double f_pp = f(x_pp);
                double f_pm = f(x_pm);
                double f_mp = f(x_mp);
                double f_mm = f(x_mm);

                // This gives us a slightly wider dx in our gradient, but saves n
                // f evaluations.
                if (H[j,k] != 0) Console.Error.Write($"H[{j},{k}]={H[j,k]:F3} (already set)");
                if (j==k) H.gradient[j] = (f_pp-f_mm)/(4*dxj);
                H[j,k] = (f_pp-f_pm-f_mp+f_mm)/(4*dxj*dxk);
                
                x_pp[k] -= dxk;
                x_pm[k] += dxk;
                x_mp[k] -= dxk;
                x_mm[k] += dxk;
            }
            x_pp[j] -= dxj;
            x_pm[j] -= dxj;
            x_mp[j] += dxj;
            x_mm[j] += dxj;
        }
        
        return H; // Should symmetrize
    }

    public static hessian central_difference_upper_triangular(
        Func<vector,double> f,
        vector x,
        double dx_exponent = -26
    ){
        int n = x.size;
    	hessian H=new hessian(n);
        
        vector x_pp = x.copy();
        vector x_pm = x.copy();
        vector x_mp = x.copy();
        vector x_mm = x.copy();
        double f_pp;
        double f_pm;
        double f_mp;
        double f_mm;
        
        for (int j = 0; j < n; j++) {       
    		double dxj=Max(Abs(x[j]),1)*Pow(2, dx_exponent);
            x_pp[j] += dxj;
            x_pm[j] += dxj;
            x_mp[j] -= dxj;
            x_mm[j] -= dxj;
            
            for (int k = 0; k <= j; k++) {
        		double dxk=Max(Abs(x[k]),1)*Pow(2, dx_exponent);                
                x_pp[k] += dxk;
                x_pm[k] -= dxk;
                x_mp[k] += dxk;
                x_mm[k] -= dxk;
                
                f_pp = f(x_pp);
                f_pm = f(x_pm);
                f_mp = f(x_mp);
                f_mm = f(x_mm);

                if (j==k) {
                    // This gives us a slightly wider dx in our gradient, but saves n
                    // f evaluations.
                    H.gradient[j] = (f_pp-f_mm)/(4*dxj);
                    H[j,j] = (f_pp-f_pm-f_mp+f_mm)/(4*dxj*dxk);
                } else {
                    H[j,k] = (f_pp-f_pm-f_mp+f_mm)/(4*dxj*dxk);
                    H[k,j] = (f_pp-f_pm-f_mp+f_mm)/(4*dxj*dxk);
                }
                
                x_pp[k] -= dxk;
                x_pm[k] += dxk;
                x_mp[k] -= dxk;
                x_mm[k] += dxk;
            }
            x_pp[j] -= dxj;
            x_pm[j] -= dxj;
            x_mp[j] += dxj;
            x_mm[j] += dxj;
        }
        
        return H;
    }
    public static vector gradient_forward_difference(
        Func<vector,double> f,
        vector x,
        double dx_exponent = -26
    ){
    	vector grad = new vector(x.size);
    	double fx_0 = f(x); /* no need to recalculate at each step */
        // Console.Error.WriteLine($"\tf(x) : {fx_0}");
    	for(int i=0;i<x.size;i++){
    		double dx=Max(Abs(x[i]),1)*Pow(2, dx_exponent);
            // Console.Error.WriteLine($"\tdx : {dx}");
    		x[i]+=dx;
            // Console.Error.WriteLine($"\tx+dx : {x}");
    		grad[i]=(f(x)-fx_0)/dx;
            // Console.Error.WriteLine($"\t∇f_i : {grad[i]}");
    		x[i]-=dx;
            // Console.Error.WriteLine($"\tx    : {x}:\n");
    	}
    	return grad;
    }
    public void symmetrize() {
        for (int i=0; i < size1; i++) {
            for (int j=0; j<i; j++) {
                double mean_ji = (this[i,j] + this[j,i])/2;
                this[i,j] = mean_ji;
                this[j,i] = mean_ji;
            }
        }
    }
}

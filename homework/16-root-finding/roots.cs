using System;
using static QRGS; 
using static matrix;
using static System.Math;



public class root_finder {
    public static vector newton(
    	Func<vector,vector> f,   /* the function to find the root of */
    	vector start,            /* the start point */
    	double acc=1e-2,         /* accuracy goal: on exit ‖f(x)‖ should be <acc */
    	vector deltax=null,      /* optional δx-vector for calculation of jacobian */
        double lambda_min = 1/64 /* minimum step length */ 
	){
        vector x=start.copy();
        vector fx=f(x),z,fz;
        do{ /* Newton's iterations */
        	if(fx.norm() < acc) break; /* job done */
        	matrix J=jacobian(f,x,fx,deltax);
        	var QRofJ = new QRGS(J);
        	vector Dx = QRofJ.solve(-fx); /* Newton's step */
        	double lambda=1;
        	do{ /* linesearch */
        		z=x+lambda*Dx;
        		fz=f(z);
        		if( fz.norm() < (1-lambda/2)*fx.norm() ) break;
        		if( lambda < lambda_min ) break;
        		lambda /= 2;
        		}while(true);
        	x=z; fx=fz;
        	}while(true);
        return x;
    }
    public static matrix jacobian(
        Func<vector,vector> f,
        vector x,
        vector fx=null,
        vector dx=null
    ){
        // For x_i = 0 we need to set it explicitly
	    if(dx == null) dx = x.map(xi => Max(Abs(xi),1)*Pow(2,-26)); 
    	if(fx == null) fx = f(x);
    	matrix J=new matrix(x.size);
    	for(int k=0;k < x.size;k++){
    		x[k]+=dx[k];
    		vector df=f(x)-fx; // forward difference approximation
    		for(int i=0;i < x.size;i++) J[i,k]=df[i]/dx[k];
    		x[k]-=dx[k]; 
    		}
    	return J;
    }    
}

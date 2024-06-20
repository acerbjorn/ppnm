set terminal svg background "white"
set out "hessian_stability_para.svg"
set title "Hessian stability for f(x)=x²"
set xlabel "log(dx)"
set ylabel "Sum of squares error"
set logscale y
set format y "10^{%T}"

plot \
	'Out_hessian_stability.txt' index 0 using 1:2 with lines title "Forward difference",\
	'Out_hessian_stability.txt' index 0 using 1:3 with lines title "Central difference",\
	'Out_hessian_stability.txt' index 0 using 1:4 with lines title "Upper triangular central difference"

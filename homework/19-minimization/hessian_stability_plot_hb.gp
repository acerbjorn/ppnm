set terminal svg background "white"
set out "hessian_stability_hb.svg"
set title "Hessian stability for Himmelblau's function"
set xlabel "log(dx)"
set ylabel "Sum of squares error"
set logscale y
set format y "10^{%T}"

plot \
	'Out_hessian_stability.txt' index 1 using 1:2 with lines title "Forward difference",\
	'Out_hessian_stability.txt' index 1 using 1:3 with lines title "Central difference",\
	'Out_hessian_stability.txt' index 1 using 1:4 with lines title "Upper triangular central difference"

set terminal svg background "white"
set out "wavelet_derivs.svg"
set macros
g(x) = 3*x*x-7*x-10
dgdx(x) = 6*x -7
d2gdx2(x) = 6
G(x) = x*x*x-7/2*x*x-10*x  

POS = "at graph 0.08,0.9 font ',15'"

set multiplot layout 2,2 rowsfirst

set label 1 'g(x)' @POS
plot \
	'wavelet_derivs_out.txt' using 1:3 with lines dashtype 2 title "Neural network", \
	g(x) with lines title "Original"

set label 1 'G(x)' @POS
plot \
	'wavelet_derivs_out.txt' using 1:4 with lines dashtype 2 title "Neural network", \
	G(x) with lines title "Original"

set label 1 'dg/dx' @POS
plot \
	'wavelet_derivs_out.txt' using 1:5 with lines dashtype 2 title "Neural network", \
	dgdx(x) with lines title "Original"

set label 1 'd^2g/dx^2' @POS
plot \
	'wavelet_derivs_out.txt' using 1:6 with lines dashtype 2 title "Neural network", \
	d2gdx2(x) with lines title "Original" 

unset multiplot


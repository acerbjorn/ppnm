set terminal svg background "white"
set out "wavelet_derivs.svg"

plot \
	'wavelet_derivs_out.txt' using 1:2 with lines title columnheader,\
	for [col=3:6] 'wavelet_derivs_out.txt' using 1:col with lines dashtype 2 title columnheader


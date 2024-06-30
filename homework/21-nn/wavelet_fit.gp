set terminal svg background "white"
set out "wavelet_fit.svg"
set arrow from -1, graph 0 to -1, graph 1 nohead
set arrow from 1, graph 0 to 1, graph 1 nohead

plot \
	'wavelet_fit_out.txt' using 1:2 with lines title columnheader,\
	for [col=3:6] 'wavelet_fit_out.txt' using 1:col with lines dashtype 2 title columnheader

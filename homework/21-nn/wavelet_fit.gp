set terminal svg background "white"
set out "wavelet_fit.svg"
set arrow from -1, graph 0 to -1, graph 1 nohead
set arrow from 1, graph 0 to 1, graph 1 nohead

plot for [col=2:6] 'wavelet_fit_out.txt' using 1:col with lines title columnheader

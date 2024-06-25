set terminal svg background "white"
set out "wavelet_derivs.svg"

plot for [col=2:6] 'wavelet_derivs_out.txt' using 1:col with lines title columnheader

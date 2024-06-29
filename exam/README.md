# Least Squares Error Concealment
This is my exam project. I would estimate the work here to be equivalent to a
part A) and a part B)

# A) Implement the least square error concealment algorithm
This happens in ls_error_conceal.cs, and some tests are done in lsec_test.cs.
These produce the plots in figure sinus1.svg, wavelet1.svg and wavelet2.svg.
They show that the technique works in general, but that for very quickly varying
points placed far apart the technique introduces some error.

![Test of error concealment on cos(x)\*sin(2*x)](sinus1.svg)
![Test of error concealment on a wavelet](wavelet1.svg)
![Test of error concealment on another wavelet](wavelet2.svg)

# B) Testing error introduced by algorithm and comparison to error concealment using splines 
The error introduced made me curious how well another interpolation algorithm
(splines) would compare. I've tried to estimate the average squared difference
error introduced by the least squares and compared it to just papering over
the errors with quadratic splines. The results can be seen in comp_wavelet.svg.
Generally the average error is lower and as the error rate (chance to replace
signal with 0) increased to unreasonable levels, the least squares method is a
lot less prone to wild swings.

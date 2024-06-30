set terminal svg background "white"
set out "wavefunction.svg"

# radial waveforms taken from 
# https://home.uni-leipzig.de/~physik/sites/mona/wp-content/uploads/sites/3/2017/12/Hydrogen_Atom.pdf
R10(x) = 2*x*exp(-x)
R20(x) = 2.*x*(1./2.)**(3./2.)*(1.-x/2.)*exp(-x/2.)
R30(x) = 2.*x*(1./3.)**(3./2.)*(1.-2./3.*x+2./27.*x*x)*exp(-x/3.)


plot \
	for [col=2:4] "wavefunction.txt" using 1:col with line dashtype 2 title gprintf("E=%5g", columnheader(col)),\
	R10(x), \
	-R20(x), \
	-R30(x) \

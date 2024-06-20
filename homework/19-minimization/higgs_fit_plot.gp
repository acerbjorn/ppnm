set terminal svg background "white"
set out "higgs_fit.svg"

breit_wigner(x) = A/((x-m)**2 + Gamma**2/4)

plot \
	"higgs_data.txt" with errorbars,\
	breit_wigner(x)

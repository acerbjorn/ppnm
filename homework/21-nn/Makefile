.PHONEY: default
default: wavelet_fit.svg wavelet_derivs.svg

scenic: default
	imv *.svg

include ../preamble.mk

wavelet_fit_out.txt: nn_approximation.exe
	mono $< > $@

wavelet_fit.svg: wavelet_fit.gp wavelet_fit_out.txt
	gnuplot $<
	
wavelet_derivs_out.txt: nn_derivs.exe
	mono $< > $@

wavelet_derivs.svg: wavelet_derivs.gp wavelet_derivs_out.txt
	gnuplot $<
	
nn_approximation.exe: nn_approximation.cs nn.dll matrix.dll; $(MKEXE)
nn_derivs.exe: nn_derivs.cs nn.dll matrix.dll; $(MKEXE)

nn.dll: nn.cs matrix.dll minimize.dll; $(MKLIB)
	
.PHONEY: clean
clean: 
	rm matrix.dll out.%.txt

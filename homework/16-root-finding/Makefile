.PHONEY: default
default: Out.txt Out.r_min.txt

include ../preamble.mk

Out.txt: test_function_minimization.exe
	mono $< > $@

Out.r_min.txt: wavefunction.exe
	mono $< > $@

wavefunction.exe: wavefunction.cs roots.dll matrix.dll rk.dll
	$(MKEXE)

test_function_minimization.exe: test_function_minimization.cs roots.dll matrix.dll
	$(MKEXE)

roots.dll: roots.cs matrix.dll qrgs.dll; $(MKLIB)

qrgs.dll: ../lineq/qrgs.cs matrix.dll; $(MKLIB)

rk.dll: ../13-ode/rk.cs matrix.dll; $(MKLIB)

	
.PHONEY: clean
clean: 
	$(RM) matrix.dll rk.dll qrgs.dll roots.dll test_function_minimization.exe wavefunction.exe Out.txt Out.r_min.txt

.PHONEY: default
default: out.unit_circle.txt

include ../preamble.mk

scenic: default
	imv *.svg

out.%.txt: %.exe Makefile
	mono $< -nterms:1e6 -v -method:halton > $@

%.exe: %.cs integ_mc.dll matrix.dll
	$(MKEXE)

integ_mc.dll: integ_mc.cs matrix.dll; $(MKLIB)

.PHONEY: clean
clean: 
	$(RM) matrix.dll integ_mc.dll out.%.txt %.exe

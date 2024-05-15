.PHONEY: default
default: testaftest.exe
	mono $<

testaftest.exe: testaftest.cs AUnit.dll
	mcs -reference:AUnit.dll $<

AUnit.dll : AUnit.cs
	mcs -target:library -out:AUnit.dll AUnit.cs

.PHONY: clean
clean: 
	rm AUnit.dll

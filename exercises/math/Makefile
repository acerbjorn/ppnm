Out.txt: math.exe
	mono math.exe > Out.txt

math.exe : math.cs sfuns.dll
	mcs -reference:sfuns.dll -target:exe math.cs

sfuns.dll : sfuns.cs
	mcs -target:library sfuns.cs

clean: 
	rm -f Out.txt math.exe sfuns.dll



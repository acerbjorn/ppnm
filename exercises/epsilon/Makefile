Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs
	mcs main.cs

.PHONY:clean
clean : 
	rm -f main.exe Out.txt

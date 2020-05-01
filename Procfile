worker : cd $HOME/heroku_output && ./SitoLotto
web: cd $HOME/heroku_output && SitoLotto.dll
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SitoLotto.dll
web: ASPNETCORE_URLS=http://*:$PORT dotnet SitoLotto.dll
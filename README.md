# Pokedex-Ultra
Pokedex-Ultra provides a lightweight API for getting information about pokemons
## Compiling and Running
You need to have either [Docker](https://www.docker.com/) or [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) installed to build and run the project
### Docker:
```cmd
 docker build -t pokedex-ultra -f Pokedex-Ultra\Dockerfile Pokedex-Ultra

 docker run -it -p 8080:80 pokedex-ultra
```
### .NET CLI
```cmd
dotnet run --project  Pokedex-Ultra/Pokedex-Ultra.csproj
```

## Usage
There are two avaliable endpoints:
```
/pokemon/{pokemonName}

​/pokemon​/translated​/{pokemonName}
```

To explore avaliable endpoints you may use a Swagger page: `/swagger/index.html`
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /webapp

# copy csproj file and restore
COPY . .
RUN dotnet restore MercadoLibre.Mutant.Dna.Api

# Copy everything else and build
COPY . ./
RUN dotnet publish MercadoLibre.Mutant.Dna.Api -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /webapp
COPY --from=build /webapp/out .
ENTRYPOINT ["dotnet", "MercadoLibre.Mutant.Dna.Api.dll"]

# docker build --no-cache -f Masiv.AuthorizationManager.Api\Dockerfile -t authorization-manager-img .
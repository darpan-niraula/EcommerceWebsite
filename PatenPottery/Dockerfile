# Use the official .NET SDK image to build the app
# sdk used to build and compile .NET application
# includes compiler, CLI tools, and libraries
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
# build-env here

# Set the working directory in the container
WORKDIR /app

# Copy the .csproj and restore dependencies
# .csproj contains the meatadata and
# dependencies of the application which is restored later
# using dotnet restore
COPY *.csproj ./
RUN dotnet restore

# Copy the entire project and build the app
COPY . ./
# publish is crucial for preparing .NET application for deployment.
RUN dotnet publish -c Release -o out
# RUN command terminal
# uses dotnet CLI to publish/ compile the application
# and preprare for deployment
# -c Release means prep for Release (other option includes Debug mode)
# -o output directory for the published files (inside directory out here)

# Application is already compiled and build
# now running the application with aspnet instead of sdk
# is lighter and only contains runtime needed to run the already built application
# Use the official ASP.NET Core runtime image for the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory in the container
# getting inside the same directory as above
WORKDIR /app

# Copies the "published" output from the "build-env"
# to current directory
# i.e. from /app/out to .
# dot means current directory (i.e. /app)
COPY --from=build-env /app/out .

# Expose the port that the application will run on
# inside the Docker container
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "PatenPottery.dll"]

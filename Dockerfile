# Stage 1: Build
# (Yahaan '8.0' ki jagah apna .NET version likhein agar alag hai)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Poora project copy karein
COPY . .

# ----------------- SABSE ZAROORI -----------------
# Apne project ka sahi path (raasta) yahaan daalein.
# Agar aapka project "HRMS.API" folder mein hai, toh "HRMS.API/HRMS.API.csproj" likhein.
RUN dotnet publish "HRMS.csproj" -c Release -o /app/publish
# -------------------------------------------------

# Stage 2: Final Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# ----------------- SABSE ZAROORI (Part 2) -----------------
# Apni .dll file ka sahi naam yahaan daalein.
# Yeh "dotnet publish" command waali file (.csproj) ka naam hi hota hai.
ENTRYPOINT ["dotnet", "HRMS.dll"]
# ------------------------------------------------- 

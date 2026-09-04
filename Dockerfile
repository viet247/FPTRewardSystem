# Stage 1: Build & Publish ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy file .csproj và restore các thư viện NuGet
COPY ["FPTRewardSystem.API.csproj", "./"]
RUN dotnet restore "FPTRewardSystem.API.csproj"

# Copy toàn bộ mã nguồn và Publish dự án
COPY . .
RUN dotnet publish "FPTRewardSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime (Môi trường chạy siêu nhẹ)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Mở cổng 8080 để Render routing lưu lượng truy cập
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "FPTRewardSystem.API.dll"]
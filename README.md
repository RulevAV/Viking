# Viking
# docker compose up
# Scaffold-DbContext "Host=localhost;Port=5439;Database=Viking;Username=Viking;Password=Viking;" Npgsql.EntityFrameworkCore.PostgreSQL -Context conViking -Project Viking -Force -Debug
# Scaffold-DbContext "Host=localhost;Port=5439;Database=Viking.Sports;Username=Viking;Password=Viking;" Npgsql.EntityFrameworkCore.PostgreSQL -Context conViking_Sports -Project Viking.Models.Sports -Force -Debug


# dotnet ef dbcontext scaffold "Host=localhost;Port=5439;Database=Viking;Username=Viking;Password=Viking;" Npgsql.EntityFrameworkCore.PostgreSQL -c conViking -p Viking.Models.Identity -f -d --для rider
# dotnet ef dbcontext scaffold "Host=localhost;Port=5439;Database=Viking.Sports;Username=Viking;Password=Viking;" Npgsql.EntityFrameworkCore.PostgreSQL -c conViking_Sports -p Viking.Models.Sports -f -d --для rider
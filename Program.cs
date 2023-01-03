using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using server_red;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRedRepository, RedRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//OracleConnection oraCon = InitConnection();

app.Run();

static OracleConnection InitConnection()
{
    bool isConnected = false;
    while (!isConnected)
    {
        isConnected = true;
        Console.WriteLine("User Id=");
        string? input = Console.In.ReadLine();
        Console.WriteLine("Password=");
        string user = input != null ? input : "";
        input = Console.In.ReadLine();
        string pwd = input != null ? input : "";
        string db = "85.143.223.149:1521";

        string conStringUser = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";";
        using (OracleConnection con = new OracleConnection(conStringUser))
        {
            try
            {
                con.Open();
                return con;
            }
            catch (Exception e)
            {
                isConnected = false;
                Console.WriteLine(e.Message);
            }
        }
    }
    throw new Exception();
}

void DestroyConnection()
{
    oraCon.Close();
}
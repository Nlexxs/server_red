using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

TestConnection();

app.Run();


static void TestConnection()
{
    string user = "red";
    string pwd = "";
    string db = "85.143.223.149:1521";
    string conStringUser = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";";

    using (OracleConnection con = new OracleConnection(conStringUser))
    {
        using (OracleCommand cmd = con.CreateCommand())
        {
            try
            {
                con.Open();
                Console.WriteLine("Successfully connected to Oracle Database as " + user);
                Console.WriteLine();

                //Retrieve sample data
                cmd.CommandText = "SELECT * FROM TEST_TABLE";
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0));
                }


                reader.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
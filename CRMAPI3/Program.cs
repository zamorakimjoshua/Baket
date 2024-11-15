
using Amazon.S3;
using CRMAPI3.Class;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAWSService<IAmazonS3>();


builder.Services.AddSingleton<IRoomS3, RoomS3>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

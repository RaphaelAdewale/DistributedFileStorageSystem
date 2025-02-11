using DistributedFileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


// add services to the container
var services = new ServiceCollection()
    .AddDfsEfc<string>(options =>
    {
        // add database provider 
        options.Database.ContextConfigurator = (db) => db.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DfsDatabase;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");

        // add path construction algorithm 
        var rnd = new Random();
        options.FileStorage.PathBuilder = (fileId) => Path.GetFullPath($@"_dfs\fake_disk_{rnd.Next(1, 3)}\{DateTime.Now:yyyy\\MM\\dd}\{fileId}");
    })
    .BuildServiceProvider();


var dfs = services.GetRequiredService<IDistributedFileStorage<string>>();

// upload
using var uploadFile = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("test text"));
var fileId = await dfs.Add(uploadFile, $"example.txt", "my metadata");

// get info
var fileInfo = await dfs.GetInfo(fileId);
Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(fileInfo));

// download 
using var downloadFile = File.Create("example.txt");
await foreach (var chunk in dfs.GetContent(fileId))
    await downloadFile.WriteAsync(chunk);
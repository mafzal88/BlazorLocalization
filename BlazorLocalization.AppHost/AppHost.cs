var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ClientUI>("clientui");

builder.Build().Run();

// See https://aka.ms/new-console-template for more information
using Assignments09_2.Domain.Interfaces;
using Assignments09_2.Domain.Services;
using Assignments09_2.Infrastructure.Database;
using Assignments09_2.Infrastructure.Repositories;

Console.WriteLine("Hello, World!");

var consoleService = new ConsoleService();
var context = new chinookContext();
var repository = new ChinookRepository(context);
var operations = new MusicShopServiceOperations(consoleService, repository);
IMusicShopService musicShop = new MusicShopService(consoleService, repository, operations);
musicShop.ManageMusicShop();
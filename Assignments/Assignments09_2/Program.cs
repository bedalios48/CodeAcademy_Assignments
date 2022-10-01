// See https://aka.ms/new-console-template for more information
using Assignments09_2.Domain.Interfaces;
using Assignments09_2.Domain.Services;
using Assignments09_2.Infrastructure.Database;
using Assignments09_2.Infrastructure.Repositories;

Console.WriteLine("Hello, World!");

IMusicShopService musicShop = new MusicShopService(new ConsoleService(), new ChinookRepository(new chinookContext()));
musicShop.ManageMusicShop();
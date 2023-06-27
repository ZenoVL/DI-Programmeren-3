using demo.bl;
using demo.dal;
using demo.dal.inMemory;
using KdG.DI.container;

var services = new DICollection();
        
services.AddSingleton<IProductRepository, ProductRepositoryInMemory>();
services.AddSingleton<ICategorieRepository, CategorieRepository>();
        
services.AddSingleton<IProductManager, ProductManager>();
services.AddSingleton<ICategorieManager,CategorieManager>();

services.startDi(typeof(Program));
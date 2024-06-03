﻿using Core.Models;
using Category = Core.Models.Category;

namespace DataBase.Data {
    public static class InitializeDatabaseSeeder {
        // Заполняем таблицу категорий
        public static List<Category> Categories = new List<Category> {
            new Category { Id = 1, Name = "Categ 1", Description = "Categ 1 Description" },
            new Category { Id = 2, Name = "Categ 2", Description = "Categ2 Description" },
            new Category { Id = 3, Name = "Categ 3", Description = "Categ3 Description" }
        };


        // Заполняем таблицу продуктов
        public static List<Product> Products = new List<Product> {
            new Product {
                Id = 1, Name = "Prod 1", Description = "Prod 1 Description", CategoryId = Categories[0].Id, Cost = 10
            },
            new Product {
                Id = 2, Name = "Prod 2", Description = "Prod 2 Description", CategoryId = Categories[1].Id, Cost = 20
            },
            new Product {
                Id = 3, Name = "Prod 3", Description = "Prod 3 Description", CategoryId = Categories[2].Id, Cost = 30
            }
        };


        // Заполняем таблицу складов
        public static List<Storage> Storages = new List<Storage> {
            new Storage { Id = 1, Name = "Storage 1", Description = "Storage 1 Description" },
            new Storage { Id = 2, Name = "Storage 2", Description = "Storage 2 Description" },
            new Storage { Id = 3, Name = "Storage 3", Description = "Storage 3 Description" }
        };

        // Заполняем таблицу связей продуктов и складов
        public static List<ProductsStorage> ProductsStorages = new List<ProductsStorage> {
            new ProductsStorage { ProductId = Products[0].Id, StorageId = Storages[0].Id },
            new ProductsStorage { ProductId = Products[1].Id, StorageId = Storages[1].Id },
            new ProductsStorage { ProductId = Products[2].Id, StorageId = Storages[2].Id }
        };
    }
}
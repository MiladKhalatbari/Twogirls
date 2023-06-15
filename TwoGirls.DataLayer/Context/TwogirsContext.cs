using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Numerics;
using System.Reflection.Emit;
using TwoGirls.DataLayer.Entities;


namespace TwoGirls.DataLayer.Context
{
    public class TwogirsContext : DbContext
    {
        public TwogirsContext(DbContextOptions<TwogirsContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<ImagePath> ImagePaths { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserDiscountCodes>  UserDiscountCodes { get; set; }
        public DbSet<ProductType> productTypes  { get; set; }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<ImagePath>())
            {
                if (entry.State == EntityState.Added && entry.Entity.ProductId != null)
                {
                    var product = Products.Find(entry.Entity.ProductId);

                    product.ImagePaths.Add(entry.Entity);
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.Entity<User>().HasQueryFilter(x => x.IsDelete == false);
            Builder.Entity<Role>().HasQueryFilter(x => x.IsDelete == false);
            Builder.Entity<Product>().HasQueryFilter(x => x.IsDelete == false);

            Random random = new Random();
            Builder.Entity<CategoryToProduct>().HasKey(t => new { t.CategoryId, t.ProductId });
            Builder.Entity<Favorite>().HasKey(t => new { t.UserId, t.ProductId });
            Builder.Entity<Product>().HasMany(p => p.ImagePaths).WithOne(ip => ip.Product).HasForeignKey(ip => ip.ProductId).OnDelete(DeleteBehavior.Restrict);
            Builder.Entity<Order>().HasOne(o => o.Cart).WithMany().HasForeignKey(o => o.CartId).OnDelete(DeleteBehavior.NoAction); 
            Builder.Entity<Order>().HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.NoAction);
            Builder.Entity<Order>().Property(o => o.AddressId).IsRequired(false);
            base.OnModelCreating(Builder);

            #region Add Product Type
            Builder.Entity<ProductType>().HasData(
               new ProductType()
               {
                   Id = 1,
                   Title = "Sunglasses"
               }, new TransactionType()
               {
                   Id = 2,
                   Title = "Eyeglasses"
               });
            #endregion

            #region Add Transaction Type
            Builder.Entity<TransactionType>().HasData(
                new TransactionType()
                {
                    Id = 1,
                    Title = "Deposit"
                }, new TransactionType()
                {
                    Id = 2,
                    Title = "Withdraw"
                });
            #endregion

            #region Add Role
            Builder.Entity<Role>().HasData(new Role()
            {
                RoleId = 1,
                RoleTitle = "Owner"
            });
            Builder.Entity<Role>().HasData(new Role()
            {
                RoleId = 2,
                RoleTitle = "Admin"
            });
            Builder.Entity<Role>().HasData(new Role()
            {
                RoleId = 3,
                RoleTitle = "Staff"
            });
            Builder.Entity<Role>().HasData(new Role()
            {
                RoleId = 4,
                RoleTitle = "User"
            });
            #endregion

            #region Add Permission
            Builder.Entity<Permission>().HasData(new Permission()
            {
                Id = 1,
                Title = "Admin Panel",
            }, new Permission()
            {
                Id = 2,
                Title = "Manage Users",
                ParentId = 1
            }, new Permission()
            {
                Id = 3,
                Title = "Manage Permissions",
                ParentId = 1
            }, new Permission()
            {
                Id = 4,
                Title = "Manage Products",
                ParentId = 1
            }, new Permission()
            {
                Id = 5,
                Title = "Manage Orders",
                ParentId = 1
            }, new Permission()
            {
                Id = 6,
                Title = "Add or Edit",
                ParentId = 2
            }, new Permission()
            {
                Id = 7,
                Title = "Delete",
                ParentId = 2
            }, new Permission()
            {
                Id = 8,
                Title = "Recover",
                ParentId = 2
            }, new Permission()
            {
                Id = 9,
                Title = "Watch the list",
                ParentId = 2
            }, new Permission()
            {
                Id = 10,
                Title = "Add or Edit",
                ParentId = 3
            }, new Permission()
            {
                Id = 11,
                Title = "Delete",
                ParentId = 3
            }, new Permission()
            {
                Id = 12,
                Title = "Recover",
                ParentId = 3
            }, new Permission()
            {
                Id = 13,
                Title = "Watch the list",
                ParentId = 3
            }, new Permission()
            {
                Id = 14,
                Title = "Add",
                ParentId = 4
            }, new Permission()
            {
                Id = 15,
                Title = "Edit",
                ParentId = 4
            }, new Permission()
            {
                Id = 16,
                Title = "Delete",
                ParentId = 4
            }, new Permission()
            {
                Id = 17,
                Title = "Recover",
                ParentId = 4
            }, new Permission()
            {
                Id = 18,
                Title = "Watch the list",
                ParentId = 4
            }, new Permission()
            {
                Id = 19,
                Title = "Send",
                ParentId = 5
            }, new Permission()
            {
                Id = 20,
                Title = "Unsend",
                ParentId = 5
            }, new Permission()
            {
                Id = 21,
                Title = "Watch the list",
                ParentId = 5
            });
            #endregion

            #region add picture
            Builder.Entity<ImagePath>().HasData(
                  new ImagePath() { Id = 1, ProductId = 1, Url = "/image/sunglasses/v1.webp", Name = "v1.webp" },
                  new ImagePath() { Id = 2, ProductId = 1, Url = "/image/sunglasses/v2.webp", Name = "v2.webp" },
                  new ImagePath() { Id = 3, ProductId = 2, Url = "/image/sunglasses/a1.webp", Name = "a1.webp" },
                  new ImagePath() { Id = 4, ProductId = 2, Url = "/image/sunglasses/a2.webp", Name = "a2.webp" },
                  new ImagePath() { Id = 5, ProductId = 3, Url = "/image/sunglasses/b1.webp", Name = "b1.webp" },
                  new ImagePath() { Id = 6, ProductId = 3, Url = "/image/sunglasses/b2.webp", Name = "b2.webp" },
                  new ImagePath() { Id = 7, ProductId = 4, Url = "/image/sunglasses/c1.webp", Name = "c1.webp" },
                  new ImagePath() { Id = 8, ProductId = 4, Url = "/image/sunglasses/c2.webp", Name = "c2.webp" },
                  new ImagePath() { Id = 9, ProductId = 5, Url = "/image/sunglasses/e1.webp", Name = "e1.webp" },
                  new ImagePath() { Id = 10, ProductId = 5, Url = "/image/sunglasses/e2.webp", Name = "e2.webp" },
                  new ImagePath() { Id = 11, ProductId = 6, Url = "/image/sunglasses/f1.webp", Name = "f1.webp" },
                  new ImagePath() { Id = 12, ProductId = 6, Url = "/image/sunglasses/f2.webp", Name = "f2.webp" },
                  new ImagePath() { Id = 13, ProductId = 7, Url = "/image/sunglasses/g1.webp", Name = "g1.webp" },
                  new ImagePath() { Id = 14, ProductId = 7, Url = "/image/sunglasses/g2.webp", Name = "g2.webp" },
                  new ImagePath() { Id = 15, ProductId = 8, Url = "/image/sunglasses/s1.webp", Name = "s1.webp" },
                  new ImagePath() { Id = 16, ProductId = 8, Url = "/image/sunglasses/s2.webp", Name = "s2.webp" },
                  new ImagePath() { Id = 17, ProductId = 9, Url = "/image/sunglasses/h1.webp", Name = "h1.webp" },
                  new ImagePath() { Id = 18, ProductId = 9, Url = "/image/sunglasses/h2.webp", Name = "h2.webp" },
                  new ImagePath() { Id = 19, ProductId = 10, Url = "/image/sunglasses/w1.webp", Name = "w1.webp" },
                  new ImagePath() { Id = 20, ProductId = 10, Url = "/image/sunglasses/w2.webp", Name = "w2.webp" },
                  new ImagePath() { Id = 21, ProductId = 11, Url = "/image/sunglasses/r1.webp", Name = "r1.webp" },
                  new ImagePath() { Id = 22, ProductId = 11, Url = "/image/sunglasses/r2.webp", Name = "r2.webp" },
                  new ImagePath() { Id = 23, ProductId = 12, Url = "/image/sunglasses/q1.webp", Name = "q1.webp" },
                  new ImagePath() { Id = 24, ProductId = 12, Url = "/image/sunglasses/q2.webp", Name = "q2.webp" }
                  );
            #endregion

            #region Add User
            Builder.Entity<User>().HasData(
                new User()
                {
                    FirstName = "Milad",
                    LastName = "Khalatbari",
                    Password = "$HASH$V1$jhUAa6x2XLTQSttEBvsxYw==$Ib1JCvml1e9BLlaqTxYe+ugz3qHe/aig6ks9sNge0lk=",
                    Id = 1,
                    Email = "aqamiladam@gmail.com",
                    PhoneNumber = "+420730834642",
                    IsActive = true,
                    RegisterDate = DateTime.Now,
                    ImagePath = "/image/user-avatar/Milad_profile.jpg",
                    ActiveCode = "50975f6e0555441db20afedb3fd1e02f",
                    RoleId = 1,
                }, new User()
                {
                    FirstName = "saman",
                    LastName = "afrasiabi",
                    Password = "$HASH$V1$jhUAa6x2XLTQSttEBvsxYw==$Ib1JCvml1e9BLlaqTxYe+ugz3qHe/aig6ks9sNge0lk=",
                    Id = 2,
                    Email = "samanafra@gmail.com",
                    PhoneNumber = "+0985858585",
                    IsActive = true,
                    RegisterDate = DateTime.Now,
                    ImagePath = "/image/user-avatar/saman_profile.jpg",
                    ActiveCode = "90e2b14b48cc40deafcd275d96c50b94",
                    RoleId = 1,
                });
            #endregion

            #region Add UserRole

            #endregion

            #region add product
            Builder.Entity<Product>().HasData(
                new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 1,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1
                }, new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 2,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1


                }, new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 3,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1


                }, new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 4,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1


                }, new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 5,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1


                }, new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 6,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1


                }, new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 7,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1


                }, new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 8,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1


                }, new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 9,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1


                }, new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 10,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1


                }, new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 11,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1


                }, new Product
                {
                    Title = "Reyban Genwux 941",
                    Id = 12,
                    ItemNumber = random.Next(19865, 88888),
                    DiscountedPrice = random.Next(1000, 1500),
                    Description = "",
                    ReleaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    PurchasePrice = random.Next(1000, 1501),
                    SalesPrice = random.Next(1000, 1501),
                    QuantityInStock = random.Next(10, 51),
                    PurchaseDate = DateTime.Now.AddMonths(random.Next(-2, 1)),
                    ProductTypeId = 1
                }) ;
            #endregion

            #region add Category

            Builder.Entity<Category>().HasData(

            new Category
            {
                Id = 1,
                Name = "Men's",
            }, new Category
            {
                Id = 2,
                Name = "Women's",
            }, new Category
            {
                Id = 3,
                Name = "Featured Categories",
            }, new Category
            {
                Id = 4,
                Name = "New Collection",
                ParentId = 1

            }, new Category
            {
                Id = 5,
                Name = "Most Visited",
                ParentId = 1
            }, new Category
            {
                Id = 6,
                Name = "Best Sellers",
                ParentId = 1
            }, new Category
            {
                Id = 7,
                Name = "Discounted",
                ParentId = 1
            }, new Category
            {
                Id = 8,
                Name = "New Collection",
                ParentId = 2
            }, new Category
            {
                Id = 9,
                Name = "Most Visited",
                ParentId = 2
            }, new Category
            {
                Id = 10,
                Name = "Best Sellers",
                ParentId = 2
            }, new Category
            {
                Id = 11,
                Name = "Discounted",
                ParentId = 2
            }, new Category
            {
                Id = 12,
                Name = "Fashion and Trends",
                ParentId = 3
            }, new Category
            {
                Id = 13,
                Name = "Designer Outlet",
                ParentId = 3
            }, new Category
            {
                Id = 14,
                Name = "Sports Glasses",
                ParentId = 3
            }, new Category
            {
                Id = 15,
                Name = "Glasses for Couples",
                ParentId = 3
            });

            #endregion

            #region add category to product

            Builder.Entity<CategoryToProduct>().HasData(
                new CategoryToProduct { ProductId = 1, CategoryId = 1 },
                new CategoryToProduct { ProductId = 1, CategoryId = 2 },
                new CategoryToProduct { ProductId = 1, CategoryId = 3 },
                new CategoryToProduct { ProductId = 1, CategoryId = 4 },
                new CategoryToProduct { ProductId = 1, CategoryId = 5 },
                new CategoryToProduct { ProductId = 1, CategoryId = 7 },
                new CategoryToProduct { ProductId = 2, CategoryId = 8 },
                new CategoryToProduct { ProductId = 2, CategoryId = 9 },
                new CategoryToProduct { ProductId = 2, CategoryId = 10 },
                new CategoryToProduct { ProductId = 2, CategoryId = 11 },
                new CategoryToProduct { ProductId = 2, CategoryId = 12 },
                new CategoryToProduct { ProductId = 3, CategoryId = 1 },
                new CategoryToProduct { ProductId = 3, CategoryId = 2 },
                new CategoryToProduct { ProductId = 3, CategoryId = 4 },
                new CategoryToProduct { ProductId = 3, CategoryId = 5 },
                new CategoryToProduct { ProductId = 4, CategoryId = 6 },
                new CategoryToProduct { ProductId = 4, CategoryId = 7 },
                new CategoryToProduct { ProductId = 5, CategoryId = 8 },
                new CategoryToProduct { ProductId = 5, CategoryId = 9 },
                new CategoryToProduct { ProductId = 5, CategoryId = 3 },
                new CategoryToProduct { ProductId = 6, CategoryId = 10 },
                new CategoryToProduct { ProductId = 6, CategoryId = 1 },
                new CategoryToProduct { ProductId = 6, CategoryId = 11 },
                new CategoryToProduct { ProductId = 7, CategoryId = 2 },
                new CategoryToProduct { ProductId = 7, CategoryId = 3 },
                new CategoryToProduct { ProductId = 7, CategoryId = 8 },
                new CategoryToProduct { ProductId = 8, CategoryId = 7 },
                new CategoryToProduct { ProductId = 8, CategoryId = 5 },
                new CategoryToProduct { ProductId = 9, CategoryId = 4 },
                new CategoryToProduct { ProductId = 9, CategoryId = 1 },
                new CategoryToProduct { ProductId = 9, CategoryId = 6 },
                new CategoryToProduct { ProductId = 10, CategoryId = 3 },
                new CategoryToProduct { ProductId = 10, CategoryId = 7 },
                new CategoryToProduct { ProductId = 11, CategoryId = 6 },
                new CategoryToProduct { ProductId = 11, CategoryId = 8 },
                new CategoryToProduct { ProductId = 12, CategoryId = 1 },
                new CategoryToProduct { ProductId = 12, CategoryId = 2 },
                new CategoryToProduct { ProductId = 12, CategoryId = 4 }
                );
            #endregion

            #region Add Review
            Builder.Entity<Review>().HasData(
                new Review()
                {
                    Id = 1,
                    UserId = 1,
                    Comment = "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !",
                    ProductId = 1,
                    Rate = 1,
                    Date = DateTime.Now.AddDays(random.Next(-50, 1))

                }, new Review()
                {
                    Id = 2,
                    UserId = 1,
                    Comment = "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !",
                    ProductId = 2,
                    Rate = 2,
                    Date = DateTime.Now.AddDays(random.Next(-50, 1))

                }, new Review()
                {
                    Id = 3,
                    UserId = 1,
                    Comment = "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !",
                    ProductId = 5,
                    Rate = 3,
                    Date = DateTime.Now.AddDays(random.Next(-50, 1))

                }, new Review()
                {
                    Id = 4,
                    UserId = 1,
                    Comment = "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !",
                    ProductId = 6,
                    Rate = 2,
                    Date = DateTime.Now.AddDays(random.Next(-50, 1))

                }, new Review()
                {
                    Id = 5,
                    UserId = 2,
                    Comment = "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !",
                    ProductId = 2,
                    Rate = 5,
                    Date = DateTime.Now.AddDays(random.Next(-50, 1))

                }, new Review()
                {
                    Id = 6,
                    UserId = 2,
                    Comment = "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !",
                    ProductId = 1,
                    Rate = 3,
                    Date = DateTime.Now.AddDays(random.Next(-50, 1))

                });
            #endregion

            #region Add Card
            Builder.Entity<Cart>().HasData(
                new Cart { Id = 1, UserId = 1, IsClose = false }
                );
            #endregion

            #region Add Card Item
            Builder.Entity<CartItem>().HasData(
                new CartItem
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 1,
                    CartId = 1

                }, new CartItem
                {
                    Id = 2,
                    ProductId = 2,
                    Quantity = 2
                    ,
                    CartId = 1
                },
                new CartItem
                {
                    Id = 3,
                    ProductId = 3,
                    Quantity = 3,
                    CartId = 1
                });
            #endregion
        }

    }
}
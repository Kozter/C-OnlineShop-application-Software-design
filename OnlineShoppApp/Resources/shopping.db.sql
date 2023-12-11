BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
	"MigrationId"	TEXT NOT NULL,
	"ProductVersion"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY("MigrationId")
);
CREATE TABLE IF NOT EXISTS "Customers" (
	"Id"	INTEGER NOT NULL,
	"Name"	TEXT NOT NULL,
	"Address"	TEXT NOT NULL,
	"Telephone"	TEXT,
	"Email"	TEXT,
	CONSTRAINT "PK_Customers" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Products" (
	"Id"	INTEGER NOT NULL,
	"Name"	TEXT NOT NULL,
	"Category"	TEXT NOT NULL,
	"Description"	TEXT NOT NULL,
	"Discount"	TEXT NOT NULL,
	"Price"	TEXT NOT NULL,
	CONSTRAINT "PK_Products" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Orders" (
	"Id"	INTEGER NOT NULL,
	"CustomerId"	INTEGER NOT NULL,
	"OrderDate"	TEXT NOT NULL,
	"Status"	TEXT,
	"PaymentMethod"	TEXT NOT NULL,
	CONSTRAINT "PK_Orders" PRIMARY KEY("Id" AUTOINCREMENT),
	CONSTRAINT "FK_Orders_Customers_CustomerId" FOREIGN KEY("CustomerId") REFERENCES "Customers"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "OrderLines" (
	"Id"	INTEGER NOT NULL,
	"OrderId"	INTEGER NOT NULL,
	"ProductId"	INTEGER NOT NULL,
	"Quantity"	INTEGER NOT NULL,
	"OrderAmount"	TEXT NOT NULL,
	CONSTRAINT "PK_OrderLines" PRIMARY KEY("Id" AUTOINCREMENT),
	CONSTRAINT "FK_OrderLines_Orders_OrderId" FOREIGN KEY("OrderId") REFERENCES "Orders"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_OrderLines_Products_ProductId" FOREIGN KEY("ProductId") REFERENCES "Products"("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Payments" (
	"Id"	INTEGER NOT NULL,
	"CustomerId"	INTEGER NOT NULL,
	"OrderId"	INTEGER NOT NULL,
	"Amount"	TEXT NOT NULL,
	"PaymentDate"	TEXT NOT NULL,
	"PaymentType"	TEXT,
	CONSTRAINT "PK_Payments" PRIMARY KEY("Id" AUTOINCREMENT),
	CONSTRAINT "FK_Payments_Customers_CustomerId" FOREIGN KEY("CustomerId") REFERENCES "Customers"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Payments_Orders_OrderId" FOREIGN KEY("OrderId") REFERENCES "Orders"("Id") ON DELETE CASCADE
);
INSERT INTO "__EFMigrationsHistory" ("MigrationId","ProductVersion") VALUES ('20231117173559_InitialCreate','7.0.13');
INSERT INTO "Customers" ("Id","Name","Address","Telephone","Email") VALUES (1,'Ann','862 Sycamore Rd','1457824125','ann@mail.com');
INSERT INTO "Customers" ("Id","Name","Address","Telephone","Email") VALUES (2,'Bob','123 Elm St','1452369874','bob@example.com');
INSERT INTO "Customers" ("Id","Name","Address","Telephone","Email") VALUES (3,'Carol','456 Maple Ave','1478523698','carol@sample.net');
INSERT INTO "Customers" ("Id","Name","Address","Telephone","Email") VALUES (4,'David','789 Oak Rd','1425369874','david@mail.com');
INSERT INTO "Customers" ("Id","Name","Address","Telephone","Email") VALUES (5,'Emma','321 Pine St','1457896321','emma@inbox.com');
INSERT INTO "Customers" ("Id","Name","Address","Telephone","Email") VALUES (6,'Frank','654 Cedar Blvd','1432567894','frank@post.com');
INSERT INTO "Customers" ("Id","Name","Address","Telephone","Email") VALUES (7,'Grace','987 Birch Lane','1456978231','grace@myemail.com');
INSERT INTO "Customers" ("Id","Name","Address","Telephone","Email") VALUES (8,'Henry','159 Willow Path','1485963721','henry@webservice.org');
INSERT INTO "Customers" ("Id","Name","Address","Telephone","Email") VALUES (9,'Isla','753 Spruce Dr','1423789561','isla@internet.com');
INSERT INTO "Customers" ("Id","Name","Address","Telephone","Email") VALUES (10,'Jason','468 Palm Way','1475829632','jason@site.com');
INSERT INTO "Products" ("Id","Name","Category","Description","Discount","Price") VALUES (1,'Trailblazer Boots','Footwear','Rugged all-terrain hiking boots.','10','120.00');
INSERT INTO "Products" ("Id","Name","Category","Description","Discount","Price") VALUES (2,'Summit Tent','Outdoor','4-person, weather-resistant tent.','15','250.00');
INSERT INTO "Products" ("Id","Name","Category","Description","Discount","Price") VALUES (3,'Zen Yoga Mat','Fitness','Eco-friendly, non-slip yoga mat.','5','35.00');
INSERT INTO "Products" ("Id","Name","Category","Description","Discount","Price") VALUES (4,'Electric Blender','Appliances','High-speed blender for smoothies.','20','99.99');
INSERT INTO "Products" ("Id","Name","Category","Description","Discount","Price") VALUES (5,'Gourmet Coffee','Groceries','Organic fair-trade coffee beans.','0','15.99');
INSERT INTO "Products" ("Id","Name","Category","Description","Discount","Price") VALUES (6,'Smart Thermostat','Electronics','Wifi-enabled, energy-saving device.','10','199.00');
INSERT INTO "Products" ("Id","Name","Category","Description","Discount","Price") VALUES (7,'Classic Sunglasses','Accessories','UV protection with a retro style.','25','50.00');
INSERT INTO "Products" ("Id","Name","Category","Description","Discount","Price") VALUES (8,'Steel Water Bottle','Gear','Insulated stainless-steel bottle.','0','25.00');
INSERT INTO "Products" ("Id","Name","Category","Description","Discount","Price") VALUES (9,'Bluetooth Headset','Electronics','Noise-cancelling wireless headset.','5','89.99');
INSERT INTO "Products" ("Id","Name","Category","Description","Discount","Price") VALUES (10,'Leather Wallet','Accessories','Handcrafted leather billfold.','15','45.00');
INSERT INTO "Orders" ("Id","CustomerId","OrderDate","Status","PaymentMethod") VALUES (1,1,'2023-11-18 10:53:24.0005039','Shipped','Credit');
INSERT INTO "OrderLines" ("Id","OrderId","ProductId","Quantity","OrderAmount") VALUES (1,1,1,3,'4.5');
CREATE INDEX IF NOT EXISTS "IX_OrderLines_OrderId" ON "OrderLines" (
	"OrderId"
);
CREATE INDEX IF NOT EXISTS "IX_OrderLines_ProductId" ON "OrderLines" (
	"ProductId"
);
CREATE INDEX IF NOT EXISTS "IX_Orders_CustomerId" ON "Orders" (
	"CustomerId"
);
CREATE INDEX IF NOT EXISTS "IX_Payments_CustomerId" ON "Payments" (
	"CustomerId"
);
CREATE INDEX IF NOT EXISTS "IX_Payments_OrderId" ON "Payments" (
	"OrderId"
);
COMMIT;

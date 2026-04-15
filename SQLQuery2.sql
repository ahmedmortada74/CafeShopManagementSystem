CREATE TABLE users
(
id INT PRIMARY KEY IDENTITY(1,1),
username VARCHAR (MAX) NULL,
password VARCHAR (MAX) NULL,
profile_image VARCHAR (MAX) NULL,
role VARCHAR (MAX) NULL,
status VARCHAR (MAX) NULL,
date_reg DATE NULL,
)


CREATE TABLE products
(
id INT PRIMARY KEY IDENTITY(1,1),
prod_id VARCHAR (MAX) NULL,
prod_name VARCHAR (MAX) NULL,
prod_type VARCHAR (MAX) NULL,
prod_stock INT  NULL,
prod_price FLOAT NULL,
prod_status VARCHAR (MAX) NULL,
prod_image VARCHAR (MAX) NULL,
date_insert  DATE NULL,
date_update  DATE NULL,
date_delete  DATE NULL,
)

Select* from customers

CREATE TABLE orders
(
id INT PRIMARY KEY IDENTITY(1,1),
customer_id INT  NULL,
prod_id VARCHAR (MAX) NULL,
prod_name VARCHAR (MAX) NULL,
prod_type VARCHAR (MAX) NULL,
prod_price FLOAT NULL,
order_date  DATE NULL,
delete_order  DATE NULL,

)

Alter TABLE customers
ADD users VARCHAR (MAX) NULL;


CREATE TABLE customers
(
id INT PRIMARY KEY IDENTITY(1,1),
customer_id INT  NULL,
total_price FLOAT NULL,
date date null,
change  float NULL,
amount  float NULL
)
truncate table orders
SELECT MAX( customer_id) FROM  customers
SELECT * FROM customers
SELECT * FROM users
SELECT * FROM products
SELECT * FROM orders
SELECT MaX(id) FROM orders
INSERT INTO users (username, password,profile_image,role,status,date_reg) VALUES ('admin','admin123','','Admin','Active','2025-02-15');
EXEC sp_rename 'orders.customer', 'customer_id', 'COLUMN';

SELECT ISNULL(SUM(total_price), 0) FROM Customers 
                                  WHERE CAST(date AS DATE) = '2025-03-05'
SELECT SUM(total_price) FROM Customers

SELECT * FROM Customers
INSERT INTO Products 
( prod_id, prod_name, prod_type, prod_stock, prod_price, prod_status, prod_image, date_insert, date_update, date_delete)
VALUES
( 'PRD-001', 'Orange Juice', 'Drinks', 100, 25, 'Available', 'E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\PRD-001.jpg', '2025-01-10', NULL, NULL),

( 'PRD-002', 'Mango Juice', 'Drinks', 80, 30, 'Available', 'E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\PRD-002.jpg', '2025-01-12', NULL, NULL),

( 'PRD-003', 'Cappuccino', 'Drinks', 60, 35, 'Available', 'E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\PRD-003.jpg', '2025-01-15', NULL, NULL),

( 'PRD-004', 'Latte', 'Drinks', 50, 40, 'Available', 'E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\PRD-004.jpg', '2025-01-18', NULL, NULL),

( 'PRD-005', 'Espresso', 'Drinks', 70, 20, 'Available', 'E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\PRD-005.jpg', '2025-01-20', NULL, NULL),

( 'PRD-006', 'Chicken Sandwich', 'Meal', 40, 55, 'Available', 'E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\PRD-006.jpg', '2025-01-22', NULL, NULL),

( 'PRD-007', 'Beef Burger', 'Meal', 30, 75, 'Available', 'E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\PRD-007.jpg', '2025-01-25', NULL, NULL),

( 'PRD-008', 'French Fries', 'Snacks', 90, 25, 'Available', 'E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\PRD-008.jpg', '2025-01-28', NULL, NULL),

( 'PRD-009', 'Pizza Margherita', 'Meal', 20, 90, 'Available', 'E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\PRD-009.jpg', '2025-02-01', NULL, NULL),

( 'PRD-010', 'Chocolate Cake', 'Dessert', 35, 45, 'Available', 'E:\projects\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\PRD-010.jpg', '2025-02-05', NULL, NULL);
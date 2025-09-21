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
Select* from orders

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

Select TABLE orders
ADD Qty INT NOT NULL DEFAULT 1;


CREATE TABLE customers
(
id INT PRIMARY KEY IDENTITY(1,1),
customer_id INT  NULL,
total_price FLOAT NULL,
date date null,
change  float NULL,
amount  float NULL
)

SELECT MAX( customer_id) FROM  customers
SELECT * FROM customers
SELECT * FROM users
SELECT * FROM products
SELECT * FROM orders
SELECT MaX(id) FROM orders
INSERT INTO users (username, password,profile_image,role,status,date_reg) VALUES ('admin','admin123','','Admin','Active','2025-02-15');
EXEC sp_rename 'orders.customer', 'customer_id', 'COLUMN';
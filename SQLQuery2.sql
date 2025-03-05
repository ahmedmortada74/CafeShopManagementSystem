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
SELECT * FROM users
INSERT INTO users (username, password,profile_image,role,status,date_reg) VALUES ('admin','admin123','','Admin','Active','2025-02-15');

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
SELECT MAX( customer_id) FROM  customers

SELECT * FROM customers
EXEC sp_rename 'orders.customer', 'customer_id', 'COLUMN';
SELECT MaX(id) FROM orders
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
alter table customers
add change float null



CREATE TABLE customers
(
id INT PRIMARY KEY IDENTITY(1,1),
customer_id INT  NULL,
total_price FLOAT NULL,
date date null,

)

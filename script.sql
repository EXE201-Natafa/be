DROP DATABASE IF EXISTS natafa_db;
CREATE DATABASE natafa_db;
USE natafa_db;

CREATE TABLE `user` (
    user_id INT AUTO_INCREMENT PRIMARY KEY, 
    email VARCHAR(255) NOT NULL UNIQUE,
    `password` VARCHAR(255) NOT NULL,
    full_name NVARCHAR(255) NOT NULL,
    birthday DATE,
    phone_number NVARCHAR(15), 
    confirmed_email BOOLEAN NOT NULL DEFAULT 0,
    image NVARCHAR(255),
    `role` ENUM('Admin', 'Staff', 'Customer') DEFAULT 'Customer' NOT NULL,
    `status` BOOLEAN NOT NULL DEFAULT 1
);

CREATE TABLE refresh_token (
    refresh_token_id INT AUTO_INCREMENT PRIMARY KEY,
    token NVARCHAR(255) NOT NULL,
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES `user`(user_id) ON DELETE CASCADE
);

CREATE TABLE category (
    category_id INT AUTO_INCREMENT PRIMARY KEY,
    category_name NVARCHAR(255) NOT NULL,
    image NVARCHAR(255),
    parent_category_id INT,
    FOREIGN KEY (parent_category_id) REFERENCES category(category_id) ON DELETE CASCADE
);

CREATE TABLE product (
    product_id INT AUTO_INCREMENT PRIMARY KEY,
    product_name NVARCHAR(255) NOT NULL,
    summary TEXT,
    material NVARCHAR(255) NOT NULL,
    created_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `status` BOOL NOT NULL DEFAULT 1,  
    category_id INT NOT NULL,
    FOREIGN KEY (category_id) REFERENCES category(category_id) ON DELETE CASCADE
);

CREATE TABLE product_detail (
    product_detail_id INT AUTO_INCREMENT PRIMARY KEY,
    size NVARCHAR(10) NOT NULL,
    weight DECIMAL(10, 2) NOT NULL, 
    height DECIMAL(10, 2) NOT NULL,
    width DECIMAL(10, 2) NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    quantity INT NOT NULL,
    discount DECIMAL(4,2) NOT NULL DEFAULT 0,
    product_id INT NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(product_id) ON DELETE CASCADE
);

CREATE TABLE product_image (
    product_image_id INT AUTO_INCREMENT PRIMARY KEY,
    url NVARCHAR(255) NOT NULL,
    product_id INT NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(product_id) ON DELETE CASCADE
);

CREATE TABLE wishlist (
    wishlist_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    product_id INT NOT NULL,
    created_date DATETIME  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES `user`(user_id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES product(product_id) ON DELETE CASCADE
);

CREATE TABLE voucher (
    voucher_id INT AUTO_INCREMENT PRIMARY KEY,
    voucher_name VARCHAR(255) NOT NULL,
    voucher_code VARCHAR(255) NOT NULL,
    `description` TEXT NOT NULL,
    discount_amount DECIMAL(10, 2) NOT NULL,
    start_date DATETIME NOT NULL,
    end_date DATETIME NOT NULL,
    minimum_purchase DECIMAL(10, 2) NOT NULL DEFAULT 0,
    usage_limit INT NOT NULL,
    `status` BOOL NOT NULL DEFAULT 1
);

CREATE TABLE user_voucher (
    user_voucher_id INT AUTO_INCREMENT PRIMARY KEY,
    redeemed_date DATETIME,
    `status` BOOL NOT NULL DEFAULT 0,
    user_id INT NOT NULL,
    voucher_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES `user`(user_id),
    FOREIGN KEY (voucher_id) REFERENCES voucher(voucher_id)
);

CREATE TABLE payment_method (
    payment_method_id INT AUTO_INCREMENT PRIMARY KEY,
    payment_method_name VARCHAR(255) NOT NULL   
);

CREATE TABLE shipping_price_table (
    shipping_price_table_id INT AUTO_INCREMENT PRIMARY KEY,
    from_weight DECIMAL(10,2)  NOT NULL,
    to_weight DECIMAL(10,2),
    in_region DECIMAL(10,2) NOT NULL,
    out_region DECIMAL(10,2) NOT NULL,
    pir DECIMAL(10,2),
    por DECIMAL(10,2)
);

CREATE TABLE `order` (
    order_id INT AUTO_INCREMENT PRIMARY KEY,
    order_code VARCHAR(100), 
    total_amount DECIMAL(10,2) NOT NULL,
    address NVARCHAR(150) NOT NULL, 
    phone_number NVARCHAR(10) NOT NULL,
    shipping_price DECIMAL(10,2) NOT NULL, 
    user_id INT NOT NULL,
    voucher_id INT,
    payment_method_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES `user`(user_id),
    FOREIGN KEY (voucher_id) REFERENCES voucher(voucher_id),
    FOREIGN KEY (payment_method_id) REFERENCES payment_method(payment_method_id)
);

CREATE TABLE order_detail (
    order_detail_id INT AUTO_INCREMENT PRIMARY KEY,
    price DECIMAL(10,2) NOT NULL,
    quantity INT NOT NULL,    
    product_detail_id INT NOT NULL,  
    order_id INT NOT NULL,  
    FOREIGN KEY (product_detail_id) REFERENCES product_detail(product_detail_id) ON DELETE CASCADE,
    FOREIGN KEY (order_id) REFERENCES `order`(order_id) ON DELETE CASCADE
);

CREATE TABLE order_tracking (
    tracking_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    `status` ENUM('Pending', 'Confirmed', 'Shipping', 'Completed', 'Canceled', 'Returned', 'Denied') NOT NULL,
    updated_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (order_id) REFERENCES `order`(order_id) ON DELETE CASCADE
);

CREATE TABLE feedback (
    feedback_id INT AUTO_INCREMENT PRIMARY KEY,
    rating FLOAT NOT NULL,
    `comment` TEXT,
    created_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `status` BOOL NOT NULL DEFAULT 1,
    user_id INT NOT NULL,
    product_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES `user`(user_id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES product(product_id) ON DELETE CASCADE
);

CREATE TABLE feedback_image (
    feedback_image_id INT AUTO_INCREMENT PRIMARY KEY,
    url NVARCHAR(255) NOT NULL,    
    feedback_id INT NOT NULL,
    FOREIGN KEY (feedback_id) REFERENCES feedback(feedback_id) ON DELETE CASCADE
);

CREATE TABLE `transaction` (
    transaction_id INT AUTO_INCREMENT PRIMARY KEY,
    amount DECIMAL(10,2) NOT NULL,
    `description` TEXT,
    created_date DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    order_id INT NOT NULL,
    FOREIGN KEY (order_id) REFERENCES `order`(order_id) ON DELETE CASCADE
);

INSERT INTO `user` (email, `password`, full_name, birthday, phone_number, confirmed_email, image, `role`, `status`) 
VALUES 
('admin@example.com', '$2a$11$wtFq3R/acjsYL.k8CXT9nO1alaldf/yWdqOk/x80W9s6fcqPbo6Nu', 'Admin User', '1985-01-01', '0123456789', 1, NULL, 'Admin', 1),
('staff@example.com', '$2a$11$wtFq3R/acjsYL.k8CXT9nO1alaldf/yWdqOk/x80W9s6fcqPbo6Nu', 'Staff User', '1990-06-15', '0987654321', 1, NULL, 'Staff', 1);

INSERT INTO voucher (voucher_name, voucher_code, `description`, discount_amount, start_date, end_date, minimum_purchase, usage_limit, `status`) VALUES
('Summer Sale', 'SUMMER2025', 'Discount for summer season', 20000.00, '2025-01-01 00:00:00', '2025-09-30 23:59:59', 100000.00, 100, 1),
('Black Friday', 'BLACKFRIDAY25', 'Black Friday Special Discount', 25000.00, '2025-11-24 00:00:00', '2025-11-25 23:59:59', 50000.00, 100, 1),
('Christmas Offer', 'XMAS2025', 'Christmas Discount', 15000.00, '2025-12-20 00:00:00', '2025-12-25 23:59:59', 75000.00, 100, 1),
('New Year Sale', 'NEWYEAR2025', 'New Year Celebration Discount', 30000.00, '2025-01-01 00:00:00', '2025-01-05 23:59:59', 120000.00, 100, 1),
('Clearance Sale', 'CLEARANCE10', 'Discount on clearance items', 10000.00, '2025-03-01 00:00:00', '2025-09-15 23:59:59', 0, 100, 1);

INSERT INTO payment_method (payment_method_name) VALUES
('Payment when delivered (COD)'),      -- Cash on Delivery
('Payment by card (VNPAY)'),    -- VNPay Payment Gateway
('Payment by Momo');

INSERT INTO shipping_price_table (from_weight, to_weight, in_region, out_region, pir, por) VALUES
(0, 3, 22000, 22000, null, null),
(3, 5, 25000, 29000, null, null),
(5, null, 25000, 29000, 4000, 4000);

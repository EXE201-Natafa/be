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

CREATE TABLE shipping_address (
    shipping_address_id INT AUTO_INCREMENT PRIMARY KEY,
    full_name NVARCHAR(255) NOT NULL,
    address VARCHAR(150) NOT NULL, 
    phone_number VARCHAR(15) NOT NULL,
    is_default BOOLEAN NOT NULL,
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES `user`(user_id)
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
    full_name NVARCHAR(255) NOT NULL,
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
('admin@example.com', '$2a$11$wtFq3R/acjsYL.k8CXT9nO1alaldf/yWdqOk/x80W9s6fcqPbo6Nu', 'Admin User', '1985-01-01', '0123456789', 1, 'https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 'Admin', 1),
('staff@example.com', '$2a$11$wtFq3R/acjsYL.k8CXT9nO1alaldf/yWdqOk/x80W9s6fcqPbo6Nu', 'Staff User', '1990-06-15', '0987654321', 1, 'https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 'Staff', 1),
('customer1@example.com', '$2a$11$wtFq3R/acjsYL.k8CXT9nO1alaldf/yWdqOk/x80W9s6fcqPbo6Nu', 'Customer One', '1995-07-01', '0123456789', 1, 'https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 'Customer', 1),
('customer2@example.com', '$2a$11$wtFq3R/acjsYL.k8CXT9nO1alaldf/yWdqOk/x80W9s6fcqPbo6Nu', 'Customer Two', '1998-03-15', '0987654321', 1, 'https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 'Customer', 1);


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

-- Main categories
INSERT INTO category (category_name, image) VALUES
('Anime', 'https://www.monsterlab.vn/wp-content/uploads/2024/07/anime-cover.png'),
('Superheroes', 'https://cellphones.com.vn/sforum/wp-content/uploads/2023/01/top-phim-sieu-nhan-hay-nhat-1-1.jpg'),
('Gaming', 'https://i0.wp.com/newdigitalage.co/wp-content/uploads/2022/06/iStock-1334436084-jpg.webp?fit=1024%2C683&ssl=1'),
('Movies & TV', 'https://dnm.nflximg.net/api/v6/BvVbc2Wxr2w6QuoANoSpJKEIWjQ/AAAAQYSYKbqZFoEIeGc-wrY2M-TY013cQsmYTg9S6KujIEFdY0afNVR3a53-z1b_WepEdcrSH7ogRkZfDdm0WaKkhtTSd4guhpBFXjkgwUPHegVn48146BjSqcQvHlMI6wEtLJ9SfOghf28FxVFQMuZGvhabdlQ.jpg?r=61b'),
('Original Designs', 'https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg');

-- Subcategories
INSERT INTO category (category_name, image, parent_category_id) VALUES
('Naruto', 'https://www.siliconera.com/wp-content/uploads/2023/07/naruto-20th-anniversary-episodes-07032023.png', 1),
('Dragon Ball', 'https://image.made-in-china.com/2f0j00DMTikNBWbvRg/New-Arrived-27-Gk-Fight-Super-Saiyan-Son-Goku-Dragon-Ball-Wholesale-Japanese-Anime-Action-Figure-Toy-Model.webp', 1),
('One Piece', 'https://static3.cbrimages.com/wordpress/wp-content/uploads/2020/03/One-Piece-Cast.jpg', 1),
('Avengers', 'https://cityofgood.sg/wp-content/uploads/2020/10/Avenger0.jpg', 2),
('Batman', 'https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 2),
('Overwatch', 'https://www.denofgeek.com/wp-content/uploads/2023/04/Overwatch-2.jpg?fit=1920%2C1080', 3),
('Star Wars', 'https://cdn-images.vtv.vn/zoom/640_400/66349b6076cb4dee98746cf1/2024/11/09/hb-disneyplus-skywalkersaga-mobile-19267-e964ed2c-1--57131370325514295168852-14870489541703807402253.jpeg', 4),
('Harry Potter', 'https://m.yodycdn.com/blog/harry-potter-co-may-phan-yody-vn-3.jpg', 4),
('Gaoranger','https://picture.dzogame.vn/Img/eng1442989122_pp_066.jpg',3);

INSERT INTO product (product_name, summary, material, category_id) VALUES
-- Products in Anime
('Naruto Uzumaki Figure', 'High-quality model of Naruto Uzumaki', 'PVC', 6),
('Dragon Ball Goku Figure', 'Detailed Goku figure from Dragon Ball', 'Resin', 7),
('One Piece Luffy Figure', 'Amazing Luffy figure from One Piece', 'PVC', 8),
('Sasuke Uchiha Figure', 'Detailed Sasuke Uchiha figure from Naruto', 'PVC', 6),
('Vegeta Figure', 'Vegeta figure from Dragon Ball with epic pose', 'Resin', 7),
('Zoro Figure', 'One Piece figure of Roronoa Zoro', 'PVC', 8),
-- Products in Superheroes
('Iron Man Mk85 Figure', 'Premium Iron Man figure', 'Metal & PVC', 9),
('Batman Dark Knight Figure', 'Detailed Batman figure from DC', 'Resin', 10),
('Thor Ragnarok Figure', 'High-quality Thor figure with Stormbreaker', 'Metal & PVC', 9),
('Joker Clown Prince of Crime Figure', 'Detailed Joker figure from DC', 'Resin', 10),
-- Products in Gaming
('Tracer Overwatch Figure', 'Tracer figure from Overwatch', 'PVC', 11),
('Genji Overwatch Figure', 'Genji figure with a sleek design', 'PVC', 11),
('Doom Slayer Figure', 'Iconic Doom Slayer model from DOOM series', 'PVC', 11),
-- Products in Movies & TV
('Star Wars Stormtrooper', 'Stormtrooper figure from Star Wars', 'PVC', 12),
('Harry Potter Wand Figure', 'Harry Potter wand replica', 'Wood', 13),
('Darth Vader Figure', 'Detailed figure of Darth Vader from Star Wars', 'PVC', 12),
('Hermione Granger Wand Figure', 'Hermione Granger wand replica from Harry Potter', 'Wood', 13),
-- Products in Original Designs
('Cyber Samurai', 'Custom cyberpunk samurai figure', 'Metal', 5),
('Mecha Warrior', 'Original mecha warrior model', 'Metal & PVC', 5),
('Fantasy Dragon', 'Handcrafted fantasy dragon figure', 'Resin', 5);

INSERT INTO product_detail (size, weight, height, width, price, quantity, discount, product_id) VALUES
-- Product Details for Naruto Uzumaki Figure
('15cm', 0.5, 15.0, 10.0, 350000, 30, 10.00, 1),
('20cm', 0.8, 20.0, 12.0, 400000, 20, 15.00, 1),
('25cm', 1.0, 25.0, 15.0, 450000, 10, 20.00, 1),
-- Product Details for Dragon Ball Goku Figure
('15cm', 0.6, 15.0, 11.0, 380000, 25, 10.00, 2),
('20cm', 0.9, 20.0, 14.0, 450000, 15, 15.00, 2),
('25cm', 1.2, 25.0, 18.0, 520000, 8, 20.00, 2),
-- Product Details for One Piece Luffy Figure
('15cm', 0.5, 15.0, 9.0, 370000, 35, 10.00, 3),
('20cm', 0.8, 20.0, 12.0, 450000, 18, 15.00, 3),
('25cm', 1.1, 25.0, 14.0, 500000, 12, 20.00, 3),
-- Product Details for Sasuke Uchiha Figure
('15cm', 0.4, 15.0, 10.0, 340000, 28, 10.00, 4),
('20cm', 0.7, 20.0, 12.0, 420000, 18, 15.00, 4),
('25cm', 1.0, 25.0, 15.0, 490000, 10, 20.00, 4),
-- Product Details for Vegeta Figure
('15cm', 0.5, 15.0, 11.0, 380000, 30, 10.00, 5),
('20cm', 0.9, 20.0, 14.0, 470000, 18, 15.00, 5),
('25cm', 1.2, 25.0, 18.0, 550000, 10, 20.00, 5),
-- Product Details for Zoro Figure
('15cm', 0.6, 15.0, 12.0, 360000, 30, 10.00, 6),
('20cm', 0.8, 20.0, 14.0, 450000, 18, 15.00, 6),
('25cm', 1.1, 25.0, 17.0, 520000, 10, 20.00, 6),
-- Product Details for Iron Man Mk85 Figure
('15cm', 0.6, 15.0, 10.0, 550000, 25, 10.00, 7),
('20cm', 1.0, 20.0, 14.0, 650000, 15, 15.00, 7),
('25cm', 1.5, 25.0, 18.0, 750000, 8, 20.00, 7),
-- Product Details for Batman Dark Knight Figure
('15cm', 0.7, 15.0, 10.0, 520000, 30, 10.00, 8),
('20cm', 1.1, 20.0, 13.0, 620000, 18, 15.00, 8),
('25cm', 1.4, 25.0, 16.0, 720000, 10, 20.00, 8),
-- Product Details for Thor Ragnarok Figure
('15cm', 0.5, 15.0, 10.0, 570000, 20, 10.00, 9),
('20cm', 0.9, 20.0, 13.0, 670000, 15, 15.00, 9),
('25cm', 1.2, 25.0, 17.0, 770000, 8, 20.00, 9),
-- Product Details for Joker Clown Prince of Crime Figure
('15cm', 0.6, 15.0, 9.0, 510000, 30, 10.00, 10),
('20cm', 1.0, 20.0, 12.0, 610000, 18, 15.00, 10),
('25cm', 1.3, 25.0, 16.0, 710000, 10, 20.00, 10),
-- Product Details for Tracer Overwatch Figure
('15cm', 0.4, 15.0, 10.0, 299000.00, 30, 10.00, 11),
('20cm', 0.6, 20.0, 12.0, 399000.00, 20, 15.00, 11),
('25cm', 0.9, 25.0, 15.0, 499000.00, 10, 20.00, 11),
-- Product Details for Genji Overwatch Figure
('15cm', 0.4, 15.0, 10.0, 299000.00, 30, 10.00, 12),
('20cm', 0.6, 20.0, 12.0, 399000.00, 20, 15.00, 12),
('25cm', 0.9, 25.0, 15.0, 499000.00, 10, 20.00, 12),
-- Product Details for Doom Slayer Figure
('15cm', 0.5, 15.0, 10.0, 349000.00, 25, 10.00, 13),
('20cm', 0.8, 20.0, 12.0, 459000.00, 15, 15.00, 13),
('25cm', 1.0, 25.0, 15.0, 569000.00, 5, 20.00, 13),
-- Product Details for Star Wars Stormtrooper Figure
('15cm', 0.5, 15.0, 10.0, 299000.00, 30, 10.00, 14),
('20cm', 0.7, 20.0, 12.0, 399000.00, 20, 15.00, 14),
('25cm', 1.0, 25.0, 15.0, 499000.00, 10, 20.00, 14),
-- Product Details for Harry Potter Wand Figure
('30cm', 0.3, 30.0, 3.0, 199000.00, 50, 5.00, 15),
('35cm', 0.4, 35.0, 3.5, 249000.00, 30, 10.00, 15),
('40cm', 0.5, 40.0, 4.0, 299000.00, 20, 15.00, 15),
-- Product Details for Darth Vader Figure
('15cm', 0.6, 15.0, 10.0, 349000.00, 25, 10.00, 16),
('20cm', 0.8, 20.0, 12.0, 449000.00, 15, 15.00, 16),
('25cm', 1.2, 25.0, 15.0, 549000.00, 5, 20.00, 16),
-- Product Details for Hermione Granger Wand Figure
('30cm', 0.3, 30.0, 3.0, 199000.00, 50, 5.00, 17),
('35cm', 0.4, 35.0, 3.5, 249000.00, 30, 10.00, 17),
('40cm', 0.5, 40.0, 4.0, 299000.00, 20, 15.00, 17),
-- Product Details for Cyber Samurai Figure
('15cm', 0.5, 15.0, 10.0, 399000.00, 20, 10.00, 18),
('20cm', 0.8, 20.0, 12.0, 499000.00, 10, 15.00, 18),
('25cm', 1.2, 25.0, 15.0, 599000.00, 5, 20.00, 18),
-- Product Details for Mecha Warrior Figure
('15cm', 0.6, 15.0, 10.0, 449000.00, 20, 10.00, 19),
('20cm', 0.9, 20.0, 12.0, 549000.00, 10, 15.00, 19),
('25cm', 1.5, 25.0, 15.0, 649000.00, 5, 20.00, 19),
-- Product Details for Fantasy Dragon Figure
('15cm', 0.7, 15.0, 10.0, 499000.00, 20, 10.00, 20),
('20cm', 1.0, 20.0, 12.0, 599000.00, 10, 15.00, 20),
('25cm', 1.6, 25.0, 15.0, 699000.00, 5, 20.00, 20);

INSERT INTO product_image (url, product_id) VALUES
-- Product Images for Naruto Uzumaki
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 1),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 1),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 1),
-- Product Images for Dragon Ball Goku
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 2),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 2),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 2),
-- Product Images for One Piece Luffy
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 3),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 3),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 3),
-- Product Images for Sasuke Uchiha
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 4),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 4),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 4),
-- Product Images for Vegeta
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 5),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 5),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 5),
-- Product Images for Zoro
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 6),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 6),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 6),
-- Product Images for Iron Man Mk85
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 7),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 7),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 7),
-- Product Images for Batman Dark Knight
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 8),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 8),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 8),
-- Product Images for Thor Ragnarok
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 9),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 9),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 9),
-- Product Images for Joker Clown Prince of Crime
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 10),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 10),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 10),
-- Product Images for Tracer Overwatch
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 11),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 11),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 11),
-- Product Images for Genji Overwatch
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 12),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 12),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 12),
-- Product Images for Doom Slayer
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 13),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 13),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 13),
-- Product Images for Star Wars Stormtrooper
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 14),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 14),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 14),
-- Product Images for Harry Potter Wand
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 15),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 15),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 15),
-- Product Images for Darth Vader
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 16),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 16),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 16),
-- Product Images for Hermione Granger Wand
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 17),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 17),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 17),
-- Product Images for Cyber Samurai
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 18),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 18),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 18),
-- Product Images for Mecha Warrior
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 19),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 19),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 19),
-- Product Images for Fantasy Dragon
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 20),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 20),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 20);


INSERT INTO wishlist (user_id, product_id) VALUES
(3, 1),
(3, 2),
(3, 3),
(3, 4),
(4, 5),
(4, 1),
(4, 6),
(4, 9);

INSERT INTO user_voucher (redeemed_date, status, user_id, voucher_id) VALUES
('2025-06-01 12:00:00', 1, 3, 1),
(NULL, 0, 4, 2);


-- Thêm 3 đơn hàng mới
INSERT INTO `order` (order_code, total_amount, full_name, address, phone_number, shipping_price, user_id, voucher_id, payment_method_id) VALUES
('ORD001', 1091000.00, 'customer1', '789 Manga Road, Kyoto', '0311222333', 50000.00, 3, NULL, 1),
('ORD002', 828300.00, 'customer2', '321 Hero Blvd, Metropolis', '0499888777', 40000.00, 4, NULL, 2),
('ORD003', 1068350.00, 'customer1', '159 Legend St, New York', '0311999888', 45000.00, 3, NULL, 1);

-- Order_detail cho 3 đơn hàng mới
INSERT INTO order_detail (price, quantity, product_detail_id, order_id) VALUES
(342000, 2, 5, 1),    -- Vegeta 15cm
(357000, 1, 10, 1),   -- Sasuke 20cm
(269100, 1, 14, 2),   -- Stormtrooper 15cm
(519200, 1, 19, 2),   -- Mecha Warrior 25cm
(509150, 1, 22, 3),   -- Fantasy Dragon 20cm
(559200, 1, 23, 3);   -- Fantasy Dragon 25cm

-- order_tracking cho 3 đơn mới với đủ trạng thái
INSERT INTO order_tracking (order_id, status, updated_date) VALUES
(1, 'Pending', '2025-06-04 08:00:00'),
(1, 'Confirmed', '2025-06-04 11:00:00'),
(1, 'Shipping', '2025-06-05 14:00:00'),
(1, 'Completed', '2025-06-07 16:00:00'),

(2, 'Pending', '2025-06-05 09:00:00'),
(2, 'Confirmed', '2025-06-05 12:00:00'),
(2, 'Shipping', '2025-06-06 15:00:00'),
(2, 'Returned', '2025-06-08 10:00:00'),

(3, 'Pending', '2025-06-06 08:00:00'),
(3, 'Confirmed', '2025-06-06 10:00:00'),
(3, 'Canceled', '2025-06-07 09:00:00');


INSERT INTO feedback (rating, comment, user_id, product_id) VALUES
(5.0, 'Amazing detail and quality!', 3, 1),
(4.0, 'Good quality but slightly expensive.', 4, 2),
(4.5, 'Very well made, looks great on my shelf!', 3, 5),
(3.5, 'Decent figure, but packaging was slightly damaged.', 4, 7),
(5.0, 'Exceeded my expectations, highly recommend!', 3, 11),
(4.0, 'Nice craftsmanship, but a bit pricey.', 4, 14);

INSERT INTO feedback_image (url, feedback_id) VALUES
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 1),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 1),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 2),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 2),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 3),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 3),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 4),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 4),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 5),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 5),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 6),
('https://res.cloudinary.com/dg9k8inku/image/authenticated/s--m7uersrt--/v1747794571/wqeav9v0avz3ee2nsibq.jpg', 6);


INSERT INTO `transaction` (amount, description, order_id) VALUES
(1091000.00, 'Payment for Vegeta 15cm x2 and Sasuke 20cm x1', 1),
(828300.00, 'Payment for Stormtrooper 15cm and Mecha Warrior 25cm', 2),
(1068350.00, 'Payment for Fantasy Dragon 20cm and 25cm', 3);



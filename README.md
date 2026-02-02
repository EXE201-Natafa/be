# Natafa - Backend API

Natafa là một ứng dụng thương mại điện tử hiện đại với backend được xây dựng bằng **ASP.NET Core 8.0**. Hệ thống cung cấp đầy đủ các chức năng quản lý sản phẩm, đơn hàng, thanh toán, và quản trị người dùng.

## 📋 Mục đích dự án

Cung cấp một nền tảng API RESTful toàn diện cho:
- Quản lý sản phẩm và danh mục
- Xử lý đơn hàng và giỏ hàng
- Thanh toán qua VNPay
- Quản lý người dùng và xác thực
- Hệ thống ghi chú và đánh giá
- Quản lý voucher và khuyến mãi
- Giao hàng và địa chỉ

## 🏗️ Kiến trúc dự án

```
Natafa/
├── Natafa.API/           # Layer API chính
│   ├── Controllers/      # Xử lý HTTP requests
│   ├── Services/         # Business Logic
│   ├── Models/          # DTOs và View Models
│   ├── Mapper/          # AutoMapper Profiles
│   └── Extensions/      # Cấu hình DI, Auth, Swagger
├── Natafa.Domain/       # Domain Entities
│   └── Entities/        # Models cơ sở dữ liệu
├── Natafa.Repository/   # Data Access Layer
│   ├── Implements/      # Repository implementations
│   └── Interfaces/      # Repository interfaces
```

## 🛠️ Công nghệ sử dụng

### Backend
- **Framework**: ASP.NET Core 8.0
- **Database**: MySQL 8.0
- **ORM**: Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **API Documentation**: Swagger/OpenAPI
- **AutoMapper**: Object mapping
- **BCrypt**: Password hashing

### External Services
- **Payment**: VNPay
- **File Storage**: Cloudinary
- **Email**: Gmail SMTP
- **Excel**: EPPlus

### Tools
- **Docker**: Containerization
- **Docker Compose**: Multi-container orchestration

## 🚀 Bắt đầu nhanh

### Yêu cầu
- .NET SDK 8.0 trở lên
- Docker & Docker Compose
- MySQL (hoặc sử dụng Docker)

### Cài đặt và chạy

#### Cách 1: Sử dụng Docker Compose (Khuyến nghị)

```bash
# Clone repository
git clone <repository-url>
cd <project-folder>

# Khởi động services
docker-compose up -d

# Backend sẽ chạy tại http://localhost:5000
# Swagger UI: http://localhost:5000/swagger
```

#### Cách 2: Chạy local

```bash
# Restore dependencies
dotnet restore

# Cập nhật database
dotnet ef database update

# Chạy ứng dụng
dotnet run --project src/Natafa.API/Natafa.Api.csproj

# Backend sẽ chạy tại https://localhost:7105
```

## 📁 Cấu trúc thư mục chi tiết

### Controllers
- `AuthenticationController` - Đăng nhập, đăng ký, xác thực
- `UserController` - Quản lý hồ sơ người dùng
- `ProductController` - Quản lý sản phẩm
- `CategoryController` - Quản lý danh mục
- `CartController` - Quản lý giỏ hàng
- `OrderController` - Quản lý đơn hàng
- `PaymentController` - Xử lý thanh toán
- `VoucherController` - Quản lý voucher/khuyến mãi
- `FeedbackController` - Quản lý ghi chú/đánh giá
- `ShippingAddressController` - Quản lý địa chỉ giao hàng
- `DashboardController` - Dashboard thống kê
- `ExcelController` - Export/Import Excel
- `TransactionController` - Lịch sử giao dịch
- `WishListController` - Danh sách yêu thích

### Services
Chứa các business logic và handler cho các chức năng chính của ứng dụng.

### Models
- **Request Models**: Dữ liệu từ client
- **Response Models**: Dữ liệu trả về client
- **DTOs**: Data Transfer Objects
- **View Models**: Models cho views

## ⚙️ Cấu hình

### Các file cấu hình chính

#### `appsettings.json` - Cấu hình chung
```json
{
  "ConnectionStrings": {
    "DbConnection": "connection-string"
  },
  "AuthenticationConfiguration": {
    "Issuer": "...",
    "Audience": "...",
    "AccessTokenSecret": "...",
    "AccessTokenExpiration": 150
  },
  "MailConfiguration": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "FromEmail": "..."
  },
  "VnPayConfig": {
    "TmnCode": "...",
    "HashSecret": "..."
  }
}
```

#### `docker-compose.yml`
- MySQL Database Service (Port: 3307)
- Backend Service (Port: 5000)
- Tự động khởi tạo database từ `script.sql`

## 📦 Cài đặt Dependencies

Các dependencies chính trong `Natafa.Api.csproj`:
- `AutoMapper` v14.0.0 - Object mapping
- `BCrypt.Net-Next` v4.0.3 - Password hashing
- `CloudinaryDotNet` v1.27.5 - Image storage
- `EPPlus` v8.0.4 - Excel processing
- `MailKit` v4.12.0 - Email sending
- `Microsoft.AspNetCore.Authentication.JwtBearer` v8.0.2 - JWT auth
- `Swashbuckle.AspNetCore` v6.6.2 - Swagger documentation

## 🔐 Xác thực & Phân quyền

- JWT Token-based authentication
- Role-based access control (Admin, Staff, Customer)
- Email verification
- Refresh token mechanism
- Password hashing với BCrypt

## 💾 Cơ sở dữ liệu

Được khởi tạo tự động từ `script.sql` khi sử dụng Docker. Bao gồm các bảng:
- `user` - Người dùng
- `refresh_token` - Làm mới token
- `shipping_address` - Địa chỉ giao hàng
- `category` - Danh mục sản phẩm
- `product` - Sản phẩm
- `cart` - Giỏ hàng
- `order` - Đơn hàng
- Và nhiều bảng khác

## 📧 Email Configuration

Hệ thống sử dụng Gmail SMTP để gửi email:
- Cấu hình trong `appsettings.json`
- Hỗ trợ email xác thực, thông báo đơn hàng, etc.

## 💳 Thanh toán (VNPay)

Tích hợp cổng thanh toán VNPay:
- VNPay configuration trong `appsettings.json`
- `PaymentController` xử lý thanh toán
- `VnPayLibrary` class cung cấp utility functions

## 📤 Upload tệp (Cloudinary)

Tích hợp Cloudinary để lưu trữ hình ảnh:
- Configuration trong `appsettings.json`
- Hỗ trợ upload hình ảnh sản phẩm, avatar user

## 📊 Excel Export/Import

Cung cấp chức năng xuất/nhập dữ liệu qua Excel:
- `ExcelController` xử lý các yêu cầu Excel
- EPPlus library cho xử lý file Excel

## 🧪 API Testing

Postman collection có sẵn trong `postman/Natafa.Api.postman_collection.json`

## 📚 API Documentation

Truy cập Swagger UI khi ứng dụng chạy:
- Local: `https://localhost:7105/swagger`
- Docker: `http://localhost:5000/swagger`

## 🔄 CORS Configuration

Được cấu hình cho frontend tại `http://localhost:5174`

## 👥 Các vai trò (Roles)

- **Admin** - Quản trị toàn bộ hệ thống
- **Staff** - Nhân viên hỗ trợ/quản lý
- **Customer** - Khách hàng mua hàng

## 📝 Logging & Debugging

- Development environment: Full logging và debugging
- Production environment: Cấu hình riêng trong `appsettings.Production.json`

## 🚢 Deployment

### Docker Deployment
```bash
docker-compose -f docker-compose.yml up -d
```

### Azure Deployment
Cơ sở dữ liệu được cấu hình cho Azure MySQL

## 📄 License

Dự án này thuộc về Natafa

## 👨‍💻 Development Team

Natafa Development Team
using FPTRewardSystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FPTRewardSystem.API.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor tiếp nhận DbContextOptions để DI Container cấu hình từ bên ngoài
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        // Định nghĩa các tập thực thể (Entity Sets) tương ứng với các bảng trong Database
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình quan hệ cho Transaction (Vì có 2 liên kết đến cùng bảng Wallet)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SenderWallet)
                .WithMany(w => w.OutgoingTransactions)
                .HasForeignKey(t => t.SenderWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ReceiverWallet)
                .WithMany(w => w.IncomingTransactions)
                .HasForeignKey(t => t.ReceiverWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MerchantProfile>()
                .HasOne(mp => mp.User)
                .WithOne(u => u.MerchantProfile)
                .HasForeignKey<MerchantProfile>(mp => mp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().ToTable("User");


            // Seed data
            // Trong hàm OnModelCreating của AppDbContext.cs

            // 1. Tạo sẵn các ID cố định để dùng chung
            var userId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var userId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var merchantUserId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            var walletId1 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var walletId2 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var merchantWalletId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

            // 2. Nạp dữ liệu cho bảng User
            modelBuilder.Entity<User>().HasData(
                new User { Id = userId1, FullName = "Nguyen Van A", Email = "wva@fpt.com", Role = UserRole.Employee },
                new User { Id = userId2, FullName = "Tran Thi B", Email = "ttb@fpt.com", Role = UserRole.Employee },
                new User { Id = merchantUserId, FullName = "Chu Cua Hang Cafe", Email = "cafe@merchant.com", Role = UserRole.Merchant }
            );

            // 3. Nạp dữ liệu cho bảng Wallet (gắn đúng UserId)
            modelBuilder.Entity<Wallet>().HasData(
                new Wallet { Id = walletId1, Balance = 500, UserId = userId1 },
                new Wallet { Id = walletId2, Balance = 300, UserId = userId2 },
                new Wallet { Id = merchantWalletId, Balance = 0, UserId = merchantUserId }
            );

            // 4. Nạp dữ liệu cho bảng MerchantProfile (chỉ tài khoản Merchant mới có)
            modelBuilder.Entity<MerchantProfile>().HasData(
                new MerchantProfile
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    StoreName = "FPT HighLands Cafe",
                    Address = "Toa nha Alpha, Hoa Lac",
                    UserId = merchantUserId
                }
            );
        }
    }
}

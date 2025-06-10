using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Entities.OTP;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Repositories
{
    public class OTPRepository : IOTPRepository
    {
        private readonly ApplicationDbContext _context;

        public OTPRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OTP otp)
        {
            await _context.OTPs.AddAsync(otp);
        }

        public async Task<OTP?> GetValidOTPAsync(string phoneNumber, int userId, string code)
        {
            return await _context.OTPs
                .Where(o => o.PhoneNumber == phoneNumber &&
                o.UserId==userId &&
                           o.Code == code &&
                           !o.IsUsed &&
                           o.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetOTPAttemptsCountAsync(string phoneNumber, DateTime since)
        {
            return await _context.OTPs
                .Where(o => o.PhoneNumber == phoneNumber &&
                           o.CreatedAt >= since)
                .CountAsync();
        }

        public async Task<bool> HasActiveOTPAsync(string phoneNumber)
        {
            return await _context.OTPs
                .AnyAsync(o => o.PhoneNumber == phoneNumber &&
                             !o.IsUsed &&
                             o.ExpiresAt > DateTime.UtcNow);
        }

        public async Task InvalidateAllOTPsForPhoneAsync(string phoneNumber)
        {
            var activeOtps = await _context.OTPs
                .Where(o => o.PhoneNumber == phoneNumber &&
                            !o.IsUsed &&
                            o.ExpiresAt > DateTime.UtcNow)
                .ToListAsync();

            foreach (var otp in activeOtps)
            {
                otp.IsUsed = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
